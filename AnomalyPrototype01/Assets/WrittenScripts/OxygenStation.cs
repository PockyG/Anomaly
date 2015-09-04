using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OxygenStation : MonoBehaviour {

	private Player player;
	private Collider button;
	public string interactKey;
	public List<GameObject> children;

	// Use this for initialization
	void Start () {
		foreach (Transform child in transform) {
			children.Add(child.gameObject);
		}

		foreach (GameObject o in children) {
			if(o.name == "Button")
			{
				button = o.GetComponent<Collider>();
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		//If a player has entered the collider of the object which will be around the general area of the object
		if (player != null) {

			/*Once player is near, check whether the player ever presses the use key. If does, we assume the player
			 * is trying to activate something*/
			if(Input.GetKey(interactKey) == true)
			{
				//Now we check if the player is actually looking at the 'button' (or other trigger)
				//getplayerraycast will return a collider. if it returns the same collider as this objects collider, it means the player is facing it.


				if(button == player.GetPlayerRayCast(5))
				{
					//This means they 'pressed' the button. appropiate response here.
					foreach (GameObject o in children) {
						if(o.name == "Spotlight")
						{
							o.GetComponent<Light>().intensity = 8;
						}
					}

					player.Oxygen += 1;
					print (player.Oxygen);
				}


			}







		}
	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			player = other.gameObject.GetComponent<Player>();
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player") {
			player = null;
		}
	}


}
