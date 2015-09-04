using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.ImageEffects;

public class Player : MonoBehaviour {
	//makes fields visible in the editor. Wanted to make it readonly but apparently that is more complicated.
	[SerializeField]
	private bool inWater = false;
	//IsInWater property, so whenever this is set, will also send bool to the movement script.
	public bool IsInWater
	{
		get
		{
			return inWater;
		}
		set{
			this.inWater = value;
			//print ("Player is in water" + inWater);
			//gameObject.transform.GetComponent<RigidbodyFirstPersonController> ().movementSettings.SetIsInWater(value);
		}
	}

	private float oxygenValue = 100;
	private float oxygenMax = 100;
	public float oxygenDepleteRate = 5; // Depletes oxygen by value every second.
	public float boostUse = 15; // Amount of oxygen used by the boost.
	//Oxygen property. Will correctly set between 0 and oxygenMax. May also call a death function if it does reach 0;

	public Rigidbody r;

	private bool arm = false; //should the arm be up?
	public string raiseArmKey;
	public GameObject armObj;
	public GameObject arrow;
	//float lastChange = 0.0f;

	public float Oxygen {
		get{ return oxygenValue;}
		set {
			oxygenValue = value;
			if (oxygenValue > oxygenMax)
				oxygenValue = oxygenMax;
			if (oxygenValue <= 0)
			{
				oxygenValue = 0;
				//death?
			}
		}
	}

	private GameObject playerCamera;

	public RaycastHit firstPersonCast;

	public List<GameObject> children;

	DepthOfField blurScript;

	// Use this for initialization
	void Start () {
		/*
		foreach (Transform child in transform) {
			children.Add(child.gameObject);
		}
		playerCamera = children[0].gameObject;
		*/
		playerCamera = Camera.main.gameObject;
		blurScript = playerCamera.GetComponent<DepthOfField> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Oxygen goes down by 5 every real life second.
		Oxygen -= oxygenDepleteRate*Time.deltaTime;

		//code to test the get set properties were working correctly.
		//	if (Input.GetKey (KeyCode.A) == true) {
		//	Oxygen -= 5*Time.deltaTime;
		//} else {
		//		Oxygen += 5*Time.deltaTime;
		//}

		print (Oxygen);

		float asd = armObj.transform.localEulerAngles.x;
		if (arm) {
			asd = Mathf.Lerp(asd,0, Time.deltaTime*10);
			//transform.localEulerAngles = Vector3(Mathf.Lerp(transform.localEulerAngles.x,0,Time.deltaTime*10),0,0);
		} else {
			asd = Mathf.Lerp(asd,60, Time.deltaTime*3);
			//transform.localEulerAngles = new Vector3(asd,0,0);
		}
		armObj.transform.localEulerAngles = new Vector3(asd,0,0);

		if (Input.GetKeyDown(raiseArmKey)) {
			arm = !arm;
		}
		
		if (Oxygen > 100)
			Oxygen = 100;
		if (Oxygen < 0)
			Oxygen = 0;

		if (Oxygen < 20) {
			//blurScript.enabled = true;
			blurScript.focalSize = Mathf.Lerp (blurScript.focalSize,0.1f*Oxygen,Time.deltaTime);
			//blurScript.focalSize = Oxygen*0.1f;
		} else {
			//blurScript.enabled = false;
			blurScript.focalSize = Mathf.Lerp (blurScript.focalSize,50f,Time.deltaTime*0.05f);
		}

		asd = (285 * (Oxygen / oxygenMax)) + 35;
		arrow.transform.localEulerAngles = new Vector3(0,asd,0);

		if (Input.GetKeyDown (KeyCode.LeftShift) && Oxygen >= boostUse) {
			//r.AddForce(Camera.main.gameObject.transform.forward * 30000);
			r.velocity = Camera.main.gameObject.transform.forward*20;
			Oxygen -= boostUse;
		}
	}

	/// <summary>
	/// Gets the first collider that collides with the player raycast that casts out where the player is looking. If no collision, returns null.
	/// </summary>
	/// <returns>The first collider the playercast collides with</returns>
	/// <param name="a_raycastDistance">A_raycast distance.</param>
	public Collider GetPlayerRayCast(float a_raycastDistance)
	{
		Debug.Log ("PlayerRay");
		 Collider raycastHitCollider;
		if (Physics.Raycast (playerCamera.transform.position, playerCamera.transform.forward, out firstPersonCast, a_raycastDistance)) {
			raycastHitCollider = firstPersonCast.collider;
			Debug.Log ("PlayerRay : " + raycastHitCollider);
			return raycastHitCollider;
		} else {
			return null;
		}
	}

	public void SetInWater(bool a_inWater)
	{
		IsInWater = a_inWater;
	}
}
