//Script for dictating the main behaviors of a dummy avatar used for testing.
//First created by Salim Larochelle on February 24 2017.

//----------------------------------------------

using UnityEngine;
using System.Collections;

public class DummyAvatar : MonoBehaviour 
{

	//--- BASIC VARIABLES
	private Transform myTransform; //This object's transform component (used of optimization)
	private CharacterController myController; //This object's character controller component
	private Animator myAnimator; //This object's animator component
	private AnimatorStateInfo myAnimatorStateInfo;
	private float myAnimatorNormalizedTime = 0.0f;

	//--- LINK VARIABLES
	public GameObject myWeapon; //This is the weapon that is caried
	
	//--- CAMERA VIEW VARIABLES
	private Transform myCameraViewPivot;
	
	//--- STATE VARIABLES
	private string currentState = "none";
	private bool  isTurning = false;
	
	//--- INPUT VARIABLES
	//movement control
	private float verticalInput = 0.0f; //To know if the player is pressing up or down (w or s)
	private float horizontalInput = 0.0f; //To know if the player is pressing left or right (a or d)
	private float mouseXInput = 0.0f; //To know if the player is aiming left or right with the mouse.
	private float mouseYInput = 0.0f; //To know if the player is aiming up or down with the mouse.
	private float mouseSensitivity = 3.0f; //how fast is the mouse.
	private bool  cameraXAxisInverted = false; //are controls on the mouse and right stick inverted when rotating the camera
	private bool  cameraYAxisInverted = false;
	private float myXVelocity  = 0.0f; //current X speed of the avatar
	private float myZVelocity  = 0.0f; //current Z speed of the avatar
	private float myYVelocity  = 0.0f; //current Y speed of the avatar
	//actions
	private bool  runInput = false; // To know if the player is pressing "run"
	private bool  attackInput = false; // To know if the player is pressing "attack"
	//input effect
	private bool  dblTapInput1 = false; //To make the run also a double tap of the W key
	private bool  dblTapInput2 = false;
	private float defaultDblTapDelay = 0.3f; //time required between two input of a double tap in second
	private float dblTapDelay = 0.3f;
	
	//--- OUTSIDE FACTORS VARIABLES
	//Gravity
	private float gravity = -9.8f; // meters per seconds *seconds
	private float terminalYVelocity = -53.0f; //meters per seconds (53 for average human)
	private float myDefaultYVelocity  = -1.0f; //default Y speed of the avatar when grounded
	
	
	//-------------------------------------------------
	//--- BASIC FUNCTIONS
	
	void  Awake ()
	{
		//Link components
		myTransform = gameObject.GetComponent<Transform>();
		myController = gameObject.GetComponent<CharacterController>() as CharacterController;
		myAnimator = gameObject.GetComponent<Animator>();
		
		//Link children
		myCameraViewPivot = transform.Find("CameraViewPivot");
	}
	
	void  Update ()
	{
		//Debug
		//Debug.Log(currentState);
		
		RecordInput();
		OutsideFactors();
		RunStates();
	}
	
	void  LateUpdate ()
	{
		//Apply Movement
		myController.Move(myTransform.TransformDirection(new Vector3(myXVelocity, myYVelocity, myZVelocity)) * Time.deltaTime);
	}
	
	void  OnTriggerEnter (Collider impactObject)
	{
		//Get hit
		if(impactObject.name == "EnemyKingSword" && currentState != "isGettingHit")
		{
			StartGetHit();
		}
	}
	
	void  RecordInput ()
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
	
	void  OutsideFactors ()
	{
		//Gravity
		if(!myController.isGrounded)
		{
			if(myYVelocity > terminalYVelocity){myYVelocity += gravity * Time.deltaTime;}
			else if(myYVelocity != terminalYVelocity){myYVelocity = terminalYVelocity;}
		}
		else{myYVelocity = myDefaultYVelocity;}
	}
	
	void  RunStates (){
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
	
	void  ResetStates ()
	{
		StopRun();
		StopWalk();
		StopIdle();
		StopAttack();
		StopGetHit();
	}
	

	//--- STATE FUNCTIONS
	//---GET HIT
	void  StartGetHit ()
	{
		ResetStates();
		currentState = "isGettingHit";
		myAnimatorNormalizedTime = 0;
		myAnimator.SetBool("isGettingHit", true);
	}
	
	void  GetHit ()
	{
		if(myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("GetHit"))
		{
			StopGetHit();
		}
	}
	
	void  StopGetHit ()
	{
		currentState = "none";
		myAnimator.SetBool("isGettingHit", false);
	}
	
	//---ATTACK
	void  StartAttack ()
	{
		ResetStates();
		currentState = "isAttacking";
		myAnimatorNormalizedTime = 0;
		myAnimator.SetBool("isAttacking", true);
		myWeapon.SendMessage("ActivateWeaponCollider");
	}
	
	void  Attack ()
	{
		if(myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Attack"))
		{
			StopAttack();
		}
	}
	
	void  StopAttack ()
	{
		currentState = "none";
		myAnimator.SetBool("isAttacking", false);
		myWeapon.SendMessage("DeactivateWeaponCollider");
	}
	
	//---RUN
	void  StartRun ()
	{
		ResetStates();
		currentState = "isRunning";
		myAnimator.SetTrigger("isRunning");
	}
	
	void  Run ()
	{
		//Blend the proper turn animation depending on the input
		myAnimator.SetFloat("runTurnInput", horizontalInput + mouseXInput);
		
		//exit
		if((!runInput && !dblTapInput2)){StopRun();}
	}
	
	void  StopRun ()
	{
		currentState = "none";
		myAnimator.SetFloat("runTurnInput", 0);
		myAnimator.ResetTrigger("isRunning");
	}
	
	//--- WALK
	void  StartWalk ()
	{
		ResetStates();
		currentState = "isWalking";
		myAnimator.SetTrigger("isWalking");
	}
	
	void  Walk ()
	{
		//update animator parameters
		myAnimator.SetFloat("walkInputPosY", verticalInput);
		myAnimator.SetFloat("walkInputPosX", horizontalInput);
		
		//exit
		if(verticalInput == 0  && horizontalInput == 0 ){StopWalk();}
	}
	
	void  StopWalk ()
	{
		currentState = "none";
		myAnimator.SetFloat("walkInputPosY", 0);
		myAnimator.SetFloat("walkInputPosX", 0);
		myAnimator.ResetTrigger("isWalking");
	}
	
	//--- IDLE
	void  StartIdle ()
	{
		ResetStates();
		currentState = "isIdling";
		myAnimator.SetTrigger("isIdling");
	}
	
	void  Idle ()
	{
		//nothing so far
	}
	
	void  StopIdle ()
	{
		currentState = "none";
		myAnimator.ResetTrigger("isIdling");
	}
	

	//--- OVERLAPPING STATE FUNCTIONS
	//TURN
	void  StartTurn ()
	{
		isTurning = true;
	}
	
	void  Turn ()
	{
		if(mouseXInput != 0){myTransform.Rotate(new Vector3(0, 1, 0) * mouseXInput  * mouseSensitivity, Space.Self);}
		else{StopTurn();}
	}
	
	void  StopTurn ()
	{
		isTurning = false;
	}
	
	//OPERATE CAMERA VIEW
	void  OperateCameraView ()
	{
		//Tilt Camera View Pivot (look up and down)
		if(mouseYInput != 0)
		{
			//parameters
			int cameraViewPivotMaxRotX = 45;
			int cameraViewPivotMinRotX = -45;
			Vector3 tempRotation = new Vector3(0,0,0);
			
			if(mouseYInput != 0)
			{myCameraViewPivot.Rotate(new Vector3(-1, 0, 0) * mouseYInput  * mouseSensitivity, Space.Self);}
			
			//convert angle
			float cameraViewPivotRotX = myCameraViewPivot.localEulerAngles.x;
			if(cameraViewPivotRotX > 180){cameraViewPivotRotX -= 360;}
			
			//lock to max or min rotation angle
			if(cameraViewPivotRotX > cameraViewPivotMaxRotX) 
			{tempRotation.x = cameraViewPivotMaxRotX; myCameraViewPivot.transform.localEulerAngles = tempRotation;}
			else if(cameraViewPivotRotX < cameraViewPivotMinRotX)
			{tempRotation.x = cameraViewPivotMaxRotX; myCameraViewPivot.localEulerAngles = tempRotation;}
		}
	}
}
