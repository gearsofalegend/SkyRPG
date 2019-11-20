//Script for dictating the main behaviors of a dummy avatar used for testing.
//First created by Salim Larochelle on February 24 2017.

//----------------------------------------------
//--- BASIC VARIABLES
private var myTransform : Transform; //This object's transform component (used of optimization)
private var myController : CharacterController; //This object's character controller component
private var myAnimator: Animator; //This object's animator component
private var myAnimatorStateInfo : AnimatorStateInfo;
private var myAnimatorNormalizedTime: float = 0.0f;
public var myWeapon : GameObject; //This is the weapon that is caried

//--- CAMERA VIEW VARIABLES
private var myCameraViewPivot : Transform;

//--- STATE VARIABLES
private var currentState : String = "none";
private var isTurning : boolean = false;

//--- INPUT VARIABLES
//movement control
private var verticalInput : float = 0.0f; //To know if the player is pressing up or down (w or s)
private var horizontalInput : float = 0.0f; //To know if the player is pressing left or right (a or d)
private var rightStickSensitivity : float = 150.0f; //how fast is does the right stick rotate the camera and character.
private var mouseXInput : float = 0.0f; //To know if the player is aiming left or right with the mouse.
private var mouseYInput : float = 0.0f; //To know if the player is aiming up or down with the mouse.
private var mouseSensitivity : float = 3.0f; //how fast is the mouse.
private var cameraXAxisInverted : boolean = false; //are controls on the mouse and right stick inverted when rotating the camera
private var cameraYAxisInverted : boolean = false;
private var myXVelocity : float  = 0.0f; //current X speed of the avatar
private var myZVelocity : float  = 0.0f; //current Z speed of the avatar
private var myYVelocity : float  = 0.0f; //current Y speed of the avatar
//actions
private var runInput : boolean = false; // To know if the player is pressing "run"
private var jumpInput : boolean = false; // To know if the player is pressing "jump"
private var attackInput : boolean = false; // To know if the player is pressing "attack"
//input effect
private var dblTapInput1 : boolean = false; //To make the run also a double tap of the W key
private var dblTapInput2 : boolean = false;
private var defaultDblTapDelay : float = 0.3f; //time required between two input of a double tap in second
private var dblTapDelay : float = 0.3f;

//--- OUTSIDE FACTORS VARIABLES
//Gravity
private var gravity : float = -9.8; // meters per seconds *seconds
private var terminalYVelocity : float = -53; //meters per seconds (53 for average human)
private var myDefaultYVelocity : float  = -1.0f; //default Y speed of the avatar when grounded


//-------------------------------------------------
//--- BASIC FUNCTIONS

function Awake()
{
	//Link components
	myTransform = gameObject.GetComponent(Transform);
	myController = gameObject.GetComponent(CharacterController) as CharacterController;
	myAnimator = gameObject.GetComponent(Animator);

	//Link children
	myCameraViewPivot = transform.Find("CameraViewPivot");
}

function Update () 
{
	//Debug
	//Debug.Log(currentState);
	//Debug.Log(myAnimatorNormalizedTime);
	
	RecordInput();
	OutsideFactors();
	RunStates();
}

function LateUpdate()
{
	//Apply Movement
	myController.Move(myTransform.TransformDirection(Vector3(myXVelocity, myYVelocity, myZVelocity)) * Time.deltaTime);
}

function OnTriggerEnter (impactObject : Collider)
{
	//Get hit
	if(impactObject.name == "EnemyKingSword" && currentState != "isGettingHit")
	{
		StartGetHit();
	}
}

function RecordInput()
{
	//Record movement axis input
	verticalInput = Input.GetAxis("Vertical");
	horizontalInput = Input.GetAxis("Horizontal");

	//Record camera axis input
	//Mouse
	if (cameraXAxisInverted) {mouseXInput = -Input.GetAxis("Mouse X");} else {mouseXInput = Input.GetAxis("Mouse X");}
	if (cameraYAxisInverted) {mouseYInput = -Input.GetAxis("Mouse Y");} else {mouseYInput = Input.GetAxis("Mouse Y");}

	//Action input
	runInput = Input.GetButton("Fire3");
	jumpInput = Input.GetButton("Jump");
	attackInput = Input.GetButtonDown("Fire1");

	//Double tap input (for running)
	if (dblTapDelay == defaultDblTapDelay && !dblTapInput1)
	{dblTapInput1 = Input.GetButtonDown("Vertical");}
	else if ((dblTapInput1 && dblTapDelay > 0) || dblTapInput2)
	{
		if (dblTapDelay > 0) {dblTapDelay -= 1 * Time.deltaTime;}
		if (!dblTapInput2) {dblTapInput2 = Input.GetButtonDown("Vertical");}
		else {dblTapInput2 = Input.GetButton("Vertical");}
	}
	else
	{
		dblTapInput1 = false;
		dblTapInput2 = false;
		dblTapDelay = defaultDblTapDelay;
	}
}

function OutsideFactors()
{
	//Gravity
	if(!myController.isGrounded)
	{
		if(myYVelocity > terminalYVelocity)
		{
			myYVelocity += gravity * Time.deltaTime;
		}
		else if(myYVelocity != terminalYVelocity)
		{
			myYVelocity = terminalYVelocity;
		}
	}
	else
	{
		myYVelocity = myDefaultYVelocity;
	}
}

function RunStates()
{
	//Start new state using priority system
	//Attack
	if(currentState == "isIdling" && attackInput && !myAnimator.IsInTransition(0))
	{StartAttack();}
	//Run
	else if(currentState != "isRunning" && currentState != "isAttacking" && currentState != "isGettingHit" && ((runInput || dblTapInput2) && verticalInput > 0))
	{StartRun();}
	//Walk
	else if(currentState != "isWalking" && currentState != "isRunning" && currentState != "isAttacking" && currentState != "isGettingHit" && (verticalInput != 0 || horizontalInput != 0))
	{StartWalk();}
	//Idle
	else if (currentState == "none")
	{StartIdle();}

	//Start overlapping states
	//Turn
	if(mouseXInput != 0)
	{StartTurn();}


	//Run ongoing states
	if(currentState == "isGettingHit") GetHit();
	else if(currentState == "isAttacking") Attack();
	else if(currentState == "isRunning") Run();
	else if(currentState == "isWalking")Walk();
	else if(currentState == "isIdling")Idle();

	//Run ongoing overlapping states
	if(isTurning) Turn();
	OperateCameraView();
	
	//Animator State Info
	myAnimatorStateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
	myAnimatorNormalizedTime = myAnimatorStateInfo.normalizedTime;
}

function ResetStates()
{
	StopRun();
	StopWalk();
	StopIdle();
	StopAttack();
	StopGetHit();
}



//--- STATE FUNCTIONS

//---GET HIT
function StartGetHit()
{
	ResetStates();
	currentState = "isGettingHit";
	myAnimatorNormalizedTime = 0;
	myAnimator.SetBool("isGettingHit", true);
	//myHealt -= 10;
}

function GetHit()
{
	if(myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("GetHit"))
	{
		StopGetHit();
	}
}

function StopGetHit()
{
	currentState = "none";
	myAnimator.SetBool("isGettingHit", false);
}

//---ATTACK
function StartAttack()
{
	ResetStates();
	currentState = "isAttacking";
	myAnimatorNormalizedTime = 0;
	myAnimator.SetBool("isAttacking", true);
	
	myWeapon.SendMessage("ActivateWeaponCollider");
}

function Attack()
{
	if(myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Attack"))
	{
		StopAttack();
	}
}

function StopAttack()
{
	currentState = "none";
	myAnimator.SetBool("isAttacking", false);
	myWeapon.SendMessage("DeactivateWeaponCollider");
}

//---RUN
function StartRun()
{
	ResetStates();
	currentState = "isRunning";
	myAnimator.SetTrigger("isRunning");
}

function Run()
{
	//Blend the proper turn animation depending on the input
	myAnimator.SetFloat("runTurnInput", horizontalInput + mouseXInput);

	//exit
	if((!runInput && !dblTapInput2)){StopRun();}
}

function StopRun()
{
	currentState = "none";
	myAnimator.SetFloat("runTurnInput", 0);
	myAnimator.ResetTrigger("isRunning");
}

//--- WALK
function StartWalk()
{
	ResetStates();
	currentState = "isWalking";
	myAnimator.SetTrigger("isWalking");
}

function Walk()
{
	//update animator parameters
	myAnimator.SetFloat("walkInputPosY", verticalInput);
	myAnimator.SetFloat("walkInputPosX", horizontalInput);

	//exit
	if(verticalInput == 0  && horizontalInput == 0 ){StopWalk();}
}

function StopWalk()
{
	currentState = "none";
	myAnimator.SetFloat("walkInputPosY", 0);
	myAnimator.SetFloat("walkInputPosX", 0);
	myAnimator.ResetTrigger("isWalking");
}

//--- IDLE
function StartIdle()
{
	ResetStates();
	currentState = "isIdling";
	myAnimator.SetTrigger("isIdling");
}

function Idle()
{

}

function StopIdle()
{
	currentState = "none";
	myAnimator.ResetTrigger("isIdling");
}



//--- OVERLAPPING STATE FUNCTIONS
//TURN
function StartTurn()
{
	isTurning = true;
}

function Turn()
{
	if(mouseXInput != 0){myTransform.Rotate(Vector3(0, 1, 0) * mouseXInput  * mouseSensitivity, Space.Self);}
	else{StopTurn();}
}

function StopTurn()
{
	isTurning = false;
}

//OPERATE CAMERA VIEW
function OperateCameraView()
{
	//Tilt Camera View Pivot (look up and down)
	if(mouseYInput != 0)
	{
		//parameters
		var cameraViewPivotMaxRotX : float = 45.0f;
		var cameraViewPivotMinRotX : float = -45.0f;

		if(mouseYInput != 0)
		{myCameraViewPivot.Rotate(Vector3(-1, 0, 0) * mouseYInput  * mouseSensitivity, Space.Self);}

		//convert angle
		var cameraViewPivotRotX : float = myCameraViewPivot.localEulerAngles.x;
		if(cameraViewPivotRotX > 180){cameraViewPivotRotX -= 360;}

		//lock to max or min rotation angle
		if(cameraViewPivotRotX > cameraViewPivotMaxRotX) {myCameraViewPivot.localEulerAngles.x = cameraViewPivotMaxRotX;}
		else if(cameraViewPivotRotX < cameraViewPivotMinRotX) {myCameraViewPivot.localEulerAngles.x = cameraViewPivotMinRotX;}
	}
}