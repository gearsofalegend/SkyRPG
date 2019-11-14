//Script that hides the object when the game starts (used mostly for editor icons).
//First created by Salim Larochelle on May 26 2016.

#pragma strict

function Start ()
{
	gameObject.GetComponent(Renderer).enabled = false;
}
