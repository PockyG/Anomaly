#pragma strict

var spd = 0.2;

function Start () {

}

function OnTriggerStay (c : Collider) {
	if (c.gameObject.tag == "Ladder" && Input.GetAxis("Vertical") >= 0.5) {
		transform.position.y += spd;
		Debug.Log("Laddering");
	}
	Debug.Log("Triggering");
}