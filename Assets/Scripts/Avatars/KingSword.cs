using UnityEngine;
using System.Collections;

public class KingSword : MonoBehaviour {

	//--- VARIABLES
	//Component link
	private Collider myCollider = null;

	//--- BASIC FUNTIONS
	void Awake ()
	{
		//Link components
		myCollider = GetComponent<Collider>();
		myCollider.enabled = false;
	}

	//---FUNCTIONS
	void ActivateWeaponCollider() //Called by avatar script
	{
		myCollider.enabled = true;
	}
	
	void DeactivateWeaponCollider()
	{
		myCollider.enabled = false;
	}
}
