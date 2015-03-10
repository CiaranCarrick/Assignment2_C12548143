using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {
	public Transform player;
	float shootTime, shootTimer = 0.3f;
	float searchTime, searchTimer = 4f;
	float randLookTime, randLookTimer = 0.3f;
	Quaternion rotation = Quaternion.identity;
	public float seconds = 0.2f;
	public bool rotating;

	public enum States //our states
	{
		Initialize,
		Sleep,
		Rotate,
	}
	public States currentState = States.Initialize; //create an instance of the enum and set it's default to Initialize
	void Update ()
	{
		switch(currentState) //pass in the current state
		{
		case States.Initialize:
			Initilize();
			break;
		case States.Sleep:
			Sleep ();
			break;
		case States.Rotate:
			Rotate ();
			break;
		}
		//Debug.Log (currentState);
		Debug.DrawRay(transform.position, transform.transform.forward * 10f, Color.magenta);
	}
	void Initilize()
	{
		transform.rotation = Quaternion.identity;
		player = GameObject.Find ("Player").transform;

		currentState = States.Sleep;
	}
	void Sleep()
	{
		if(Vector3.Distance(transform.position, player.position) > 4f)
		{
			currentState = States.Initialize;
		}
	}
	void Rotate()
	{
		StartCoroutine(Rotateme(Vector3.up*90));

	}
	
	IEnumerator Rotateme(Vector3 degrees)
	{
		if (rotating) {
			return true;
		}
		rotating = true;
		float time = 0.0f;
		float rate = 1.0f /seconds;
		Quaternion startRotation = this.transform.rotation;
		Quaternion endRotation = this.transform.rotation * Quaternion.Euler (degrees);
		while (time<1.0f) {
			time += Time.deltaTime * rate;
			transform.rotation = Quaternion.Slerp (startRotation, endRotation, time);
			yield return 0;
		}
		print("Turned 90 degrees");
		rotating = false;
		currentState = States.Sleep;
	}
} 