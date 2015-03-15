using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {
	public float seconds = 0.3f;
	public bool rotating;
	public int counter;
	public GameObject door;
	Quaternion MyRotation;
	void Start(){
		MyRotation = transform.rotation;
	}
	
	public enum States //our states
	{
		Initialize,
		Awake,
		Rotate,
		Sleep,
	}
	public States currentState = States.Awake; //create an instance of the enum and set it's default to Initialize
	void Update ()
	{
		switch(currentState) //pass in the current state
		{
		case States.Initialize:
			Initilize();
			break;
		case States.Awake:
			Awake ();
			break;
		case States.Rotate:
			Rotate ();
			break;
		case States.Sleep:
			Sleep();
			break;
		}
		//Debug.Log (currentState);
		Debug.DrawRay(transform.position, transform.transform.forward * 10f, Color.magenta);
	}
	void Initilize()
	{
		StartCoroutine (Rotateme (-transform.localEulerAngles));
		counter=0;
	}
	void Awake()
	{
		if (currentState != States.Initialize && door.GetComponent<Door> ().currentState == Door.States.Opened) {
			currentState = States.Sleep;
		} else
			currentState = States.Awake; 
	}
	void Rotate()
	{
		StartCoroutine(Rotateme(Vector3.up * 90));
	}
	void Sleep()
	{
		if(transform.position.y > -0.5) {
			transform.Translate (Vector3.down * 1 * Time.deltaTime);
		}
	}
		
	IEnumerator Rotateme(Vector3 degrees)
	{
		if (rotating) {
			return true;
		}
		rotating = true;
		float time = 0f;
		float rate = 1.0f /seconds;
		Quaternion startRotation = this.transform.rotation; //Create referance to rotation
		Quaternion endRotation = this.transform.rotation * Quaternion.Euler (degrees);
		while (time<1.0f) {
			time += Time.deltaTime * rate;
			if(currentState==States.Rotate){
			transform.rotation = Quaternion.Slerp (startRotation, endRotation, time); //Rotates to desired rotation
			}
			else{
				transform.rotation = Quaternion.Slerp (startRotation, MyRotation, time);//resets to original rotation
			}
			yield return 0;//continue to next line and return
		}
		//print("Turned 90 degrees");
		counter= (counter + 1) % 4;
		rotating = false;
		currentState = States.Awake;
	}
} 