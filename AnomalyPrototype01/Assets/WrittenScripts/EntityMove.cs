using UnityEngine;
using System.Collections;

public class EntityMove : MonoBehaviour {

	public  Transform player;
	public Terrain terrain;
	public float speed = 2;

	public bool isActive = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isActive) {
			transform.LookAt (player.position);
			transform.position = new Vector3 (transform.position.x, terrain.SampleHeight (this.transform.position) + transform.localScale.y, transform.position.z);
			transform.position += transform.forward * speed * Time.deltaTime;
		}




	}
}
