#pragma strict

var arm = false; //should the arm be up?
var raiseArm : String;

var arrow : GameObject;
var lastChange = 0.0;
var air = 100.0; //air %



function Update () {
	if (arm) {
		transform.localEulerAngles.x = Mathf.Lerp(transform.localEulerAngles.x,0, Time.deltaTime*10);
	} else {
		transform.localEulerAngles.x = Mathf.Lerp(transform.localEulerAngles.x,60, Time.deltaTime*10);
	}
	if (Input.GetKeyDown(raiseArm)) {
		arm = !arm;
	}
	
	//Consume air
	if (Time.time - lastChange > 3.0) {
		lastChange = Time.time;
		air--;
	}
	
	if (air > 100)
		air = 100;
	if (air < 0)
		air = 0;
	
	arrow.transform.localEulerAngles.y = (285*(air/100))+35;
}