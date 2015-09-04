using UnityEngine;
using System.Collections;

public class StationInteraction : MonoBehaviour {

	public string m_Input;
	public Collider m_Player;
	private bool collision = false;
	// Bool used for display on Terminal
	private bool Activated = false;

	public Light m_Light;
	public Light m_LightHalo;
	private bool flashing = true;
	private float duration = 2.5F;

	// Use this for initialization
	void Start() {}

	// Update is called once per frame
	void Update() {
		if (flashing == true) {
			float phi = Time.time / duration * 2 * Mathf.PI;
			float amplitude = Mathf.Cos (phi) * 5.0F + 3.0F;
			m_Light.intensity = amplitude;
			m_LightHalo.intensity = amplitude;
		}
		if (collision == true) {
			if (Input.GetKeyDown(m_Input)) {
				m_Light.color = Color.green;
				m_LightHalo.color = Color.green;
				Activated = true;
				flashing = false;
				m_Light.intensity = 8;
				m_LightHalo.intensity = 8;
			}
		}
	}

	void OnTriggerEnter(Collider m_player)
	{
		Debug.Log("Enter:" + m_player.name);
		collision = true;
	}
	void OnTriggerExit(Collider m_player) {
		Debug.Log("Exit");
		collision = false;
	}
}
