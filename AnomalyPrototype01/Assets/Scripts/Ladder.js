#pragma strict

var spd = Vector3(0,0,0);
var r : Rigidbody;

function Start () {

}

function OnTriggerStay (c : Collider) {
	if (c.gameObject.tag == "Ladder" && Input.GetAxis("Vertical") >= 0.5) {
		r.MovePosition(transform.position + spd * Time.deltaTime);
		//r.velocity.y = 1;
		Debug.Log("Laddering");
	}
	//Debug.Log("Triggering");
}