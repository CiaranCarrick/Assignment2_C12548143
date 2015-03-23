using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {
	public float seconds = 0.3f;// Add Fields
	public bool rotating, reset, DoorOpened;//Left all public to show whats happening in inspector
	public int counter, MyCounter;
	public GameObject door, player;
	public Interaction interaction;
	Quaternion MyRotation;//
	public Vector3 Sleepposition, MyAwakePosition;
	
	void Start() {//I could have run a for loop and printed 3 cubes and used add.component to add scripts but visually easier this way
		counter = MyCounter;
		MyRotation = transform.rotation; //Cache intial rotation
		MyAwakePosition = transform.position; //Cache intial Position
		Sleepposition = new Vector3 (transform.position.x, -0.5f, transform.position.z);
		door = GameObject.Find ("Door");
		player = GameObject.Find ("Player");
		interaction = player.GetComponentInChildren<Interaction> ();//Find interaction script in player object
	}//  End Start
	
	public enum States //States
	{
		Initialize, //Declare States
		Awake, 
		Rotate, 
		Sleep, 
		Move,
	}//end States
	public States currentState = States.Awake; //create an instance of the Enum, set to Initialize
	void Update ()
	{  
		switch(currentState) //pass in current state
		{
		case States.Initialize:
			initialize();
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
		case States.Move:
			Move();
			break;
		}
		//Debug.Log (currentState);
		Debug.DrawRay(transform.position, transform.transform.forward * 10f, Color.magenta);
	}//end Update
	
	void initialize()
	{
		StartCoroutine (Rotateme (-transform.localEulerAngles)); //Resets to default orientation
		if (counter == interaction.keyrotation) {
			renderer.material.color = Color.green; //Flashs green during initialize state, so when you activate switch it will show you whether the blocks Counter == Interaction.Keyrotation
		}
	}//End Initialize

	
	public void Awake()
	{
		if (transform.position.y < MyAwakePosition.y) { //Resets Cube back to Awake position from SleepPosition
			transform.Translate (Vector3.up * 3 * Time.deltaTime); //Lowers Cubes once reachs position
		}
		else
			renderer.material.color=Color.red;
		
		if (door.GetComponent<Door> ().currentState == Door.States.Opened) {
			currentState = States.Move;
		}
	}//  End Awake

	
	void Rotate()
	{
		StartCoroutine(Rotateme(Vector3.up * 90)); //Coroutine Rotates the cube 90 degrees,
	}//End Rotate


	void Sleep()
	{
		if (door.GetComponent<Door>().Doorsize>=1) {//Whens the doors Size reachs 1, set door to closed state
			door.GetComponent<Door>().currentState=Door.States.Closed;
		}
		else
			door.GetComponent<Door>().currentState=Door.States.Opened;//Until then, set door to opened state
			currentState = States.Awake;//Keep cube active while doorsize != 1
	}//  End Sleep


	void Move() {
		renderer.material.color = Color.white;
		if (transform.position.y >= Sleepposition.y) { 
			transform.Translate (Vector3.down * 3 * Time.deltaTime); //Lowers Cubes once reachs position
			if (transform.position.y <= Sleepposition.y) {
				currentState = States.Sleep;//Set currentstate to Sleep
			}
		}//end if
	}//end Move
	
	
	IEnumerator Rotateme(Vector3 degrees)
	{
		if (rotating) {
			return true;//Set true to avoid the Coroutine calling itself
		}
		rotating = true;
		float time = 0f;
		float rate = 1.0f /seconds;//Speed of rotation
		Quaternion startRotation = this.transform.rotation; //Create referance to rotation
		Quaternion endRotation = this.transform.rotation * Quaternion.Euler (degrees); //Rotates the angle by perameter the 'degrees'
		while (time<1.0f) {
			time += Time.deltaTime * rate;
			if (currentState==States.Rotate) {
				transform.rotation = Quaternion.Slerp (startRotation, endRotation, time); //Rotates to desired rotation
			}
			else {// Only activateed via switch, rotating blocks never reachs this code
				reset=true;
				transform.rotation = Quaternion.Slerp (startRotation, MyRotation, time);//resets to original rotation
			}
			yield return null;//stop, continue to next line and return null
		}
		//print("Turned 90 degrees");
		counter= (counter+1) % 4; //Resets once reachs 4, when cube reachs its original rotation
		if (reset) { // excuted when Switch is pressed and Rotation cubes Counter != keyrotation
			counter = MyCounter;//reset counter to MyCounter
			reset = false;//reset boolean
		}//End Reset
		rotating = false; //reset boolean
		interaction.Checkcubes = true; //Referance Re-activates bool to allow Rotation cubes to be checked
		currentState = States.Awake; //Set State back to awake
	}//End Rotateme
}//End Main
