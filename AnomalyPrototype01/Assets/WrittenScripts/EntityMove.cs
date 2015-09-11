using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EntityPattern
{
	Straight = 0,
	Circle = 1,
	Above = 2,
	Count = 3,
};

public class EntityMove : MonoBehaviour
{
	public Transform player;
	public Terrain terrain;
	public float speed = 1;
	public int detectDistance = 3;
	public bool isActive = true;
	public int entityPattern = (int)EntityPattern.Straight;
	private bool detectedFlare = false;
	 

	
	// Use this for initialization
	void Start ()
	{

	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//If the entity is active, start player-finding patterns. Turn off if player is in lab
		if (isActive) {

			switch (entityPattern) {
			case (int)EntityPattern.Straight:
				transform.LookAt (player.position);
				transform.position = new Vector3 (transform.position.x, terrain.SampleHeight (this.transform.position) + transform.localScale.y, transform.position.z);
				transform.position += transform.forward * speed * Time.deltaTime;

				break;
			default:
				transform.LookAt (player.position);
				transform.position = new Vector3 (transform.position.x, terrain.SampleHeight (this.transform.position) + transform.localScale.y, transform.position.z);
				transform.position += transform.forward * speed * Time.deltaTime;
				break;

			}
			//If the entity is a certain distance away, player can start to detect it 
			if ((player.position - this.transform.position).magnitude < detectDistance) {
				if (IsEntitySeen ()) {

					print ("ENTITY SEEEEEN");
					ResetEntity ();

				}
			}
			if(detectedFlare)
			{
				//the entity is in the vicinity of a flare or light source. scare entity
				ResetEntity();
			}
			


		}




	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") {
			//playerdeath.
			print ("Player died");
		}

		if (other.tag == "Flare") {
			//scared by a flare
			print ("scared by a flare");
			detectedFlare = true;
			//reposition?
		}
	}

	bool IsEntitySeen ()
	{
		if(this.gameObject.GetComponent<Renderer>().isVisible)
			return true;

		return false;
	}

	IEnumerator ResetEntity ()
	{
		//speed = 
		yield return new WaitForSeconds(0.25f);
		entityPattern = (int)Random.Range (0, (float)EntityPattern.Count);
		print ("entity pattern = " + entityPattern);
		int distanceAway = Random.Range (20, 30);
		float angleRespawn = Random.Range (0, 360);
		Vector3 distanceVec = new Vector3 (Mathf.Sin (Mathf.Deg2Rad * angleRespawn) * distanceAway, 0, Mathf.Cos (Mathf.Deg2Rad * angleRespawn) * distanceAway);
		Vector3 newPosition = new Vector3 ();
		newPosition = player.position + distanceVec;
		this.transform.position = newPosition;


			

	}
}
