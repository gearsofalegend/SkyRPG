using UnityEngine;
using System.Collections;

public class DummyEnemy : MonoBehaviour {

	//--- BASIC VARIABLES
	private Transform myTransform; //This object's transform component (used of optimization)
	private CharacterController myController; //This object's character controller component
	private Animator myAnimator; //This object's animator component
	private AnimatorStateInfo myAnimatorStateInfo;
	private float myAnimatorNormalizedTime = 0.0f;

	//--- LINK VARIABLES
	public GameObject target;
	public GameObject myWeapon; //This is the weapon that is caried

	//--- STATE VARIABLES
	private string currentState = "none";
	//attack
	private float attackRange = 2.0f;

	//--- MOVEMENT VARIABLES
	private float myXVelocity = 0.0f; //current X speed of the avatar
	private float myZVelocity = 0.0f; //current Z speed of the avatar
	private float myYVelocity = 0.0f; //current Y speed of the avatar

	//--- OUTSIDE FACTORS VARIABLES
	//Gravity
	private float gravity = -9.8f; // meters per seconds *seconds
	private float terminalYVelocity = -53.0f; //meters per seconds (53 for average human)
	private float myDefaultYVelocity = -1.0f; //default Y speed of the avatar when grounded

	void Awake()
	{
		//Link components
		myTransform = GetComponent<Transform>();
		myController = GetComponent<CharacterController>();
		myAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(currentState);

		OutsideFactors();
		RunStates();
	}

	void LateUpdate()
	{
		//Apply Movement
		myController.Move(myTransform.TransformDirection(new Vector3(myXVelocity, myYVelocity, myZVelocity)) * Time.deltaTime);
	}

	void OnTriggerEnter (Collider impactObject)
	{
		//Get hit
		if(impactObject.name == "KingSword" && currentState != "isGettingHit")
		{
			StartGetHit();
		}
	}

	void RunStates()
	{
		//Start new state using priority system
		//Attack
		if(currentState == "isIdling" && currentState != "isGettingHit" && Vector3.Distance(myTransform.position, target.transform.position) <= attackRange)
		{StartAttack();}
		//Walk
		else if(currentState != "isWalking" && currentState != "isGettingHit" && currentState != "isAttacking" && Vector3.Distance(myTransform.position, target.transform.position) > attackRange)
		{StartWalk();}
		//Idle
		else if (currentState == "none")
		{StartIdle();}

		//Run ongoing states
		if(currentState == "isGettingHit") GetHit();
		else if(currentState == "isAttacking") Attack();
		else if(currentState == "isWalking")Walk();
		else if(currentState == "isIdling")Idle();

		//Run ongoing overlapping states
		Turn();
		
		//Animator State Info
		myAnimatorStateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
		myAnimatorNormalizedTime = myAnimatorStateInfo.normalizedTime;
	}

	void OutsideFactors()
	{
		//Gravity
		if(!myController.isGrounded)
		{
			if(myYVelocity > terminalYVelocity){myYVelocity += gravity * Time.deltaTime;}
			else if(myYVelocity != terminalYVelocity){myYVelocity = terminalYVelocity;}
		}
		else{myYVelocity = myDefaultYVelocity;}
	}

	void ResetStates()
	{
		StopWalk();
		StopIdle();
		StopAttack();
		StopGetHit();
	}


	//--- STATE FUNCTIONS
	
	//---GET HIT
	void StartGetHit()
	{
		ResetStates();
		currentState = "isGettingHit";
		myAnimatorNormalizedTime = 0;
		myAnimator.SetBool("isGettingHit", true);
		//reduce health here
	}
	
	void GetHit()
	{
		if(myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("GetHit"))
		{
			StopGetHit();
		}
	}
	
	void StopGetHit()
	{
		currentState = "none";
		myAnimator.SetBool("isGettingHit", false);
	}

	//---ATTACK
	void StartAttack()
	{
		ResetStates();
		currentState = "isAttacking";
		myAnimatorNormalizedTime = 0;
		myAnimator.SetBool("isAttacking", true);
		
		myWeapon.SendMessage("ActivateWeaponCollider");
	}
	
	void Attack()
	{
		if(myAnimatorNormalizedTime >= 1 && myAnimatorStateInfo.IsName("Attack"))
		{
			StopAttack();
		}
	}
	
	void StopAttack()
	{
		currentState = "none";
		myAnimator.SetBool("isAttacking", false);
		myWeapon.SendMessage("DeactivateWeaponCollider");
	}

	//--- WALK
	void StartWalk()
	{
		ResetStates();
		currentState = "isWalking";
		myAnimator.SetTrigger("isWalking");
	}
	
	void Walk()
	{
		//update animator parameters
		myAnimator.SetFloat("walkInputPosY", 1);
		//myAnimator.SetFloat("walkInputPosX", horizontalInput);
		
		//exit
		if(Vector3.Distance(myTransform.position, target.transform.position) <= attackRange){StopWalk();}
	}
	
	void StopWalk()
	{
		currentState = "none";
		myAnimator.SetFloat("walkInputPosY", 0);
		myAnimator.SetFloat("walkInputPosX", 0);
		myAnimator.ResetTrigger("isWalking");
	}

	//--- IDLE
	void StartIdle()
	{
		ResetStates();
		currentState = "isIdling";
		myAnimator.SetTrigger("isIdling");
	}
	
	void Idle()
	{
		//nothing yet
	}
	
	void StopIdle()
	{
		currentState = "none";
		myAnimator.ResetTrigger("isIdling");
	}


	//--- OVERLAPPING STATE FUNCTIONS
	//TURN
	void Turn()
	{
		myTransform.LookAt(target.transform);
	}
}
