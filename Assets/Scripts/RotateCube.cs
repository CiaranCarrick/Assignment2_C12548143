using UnityEngine;
using System.Collections;

public class RotateCube : MonoBehaviour {
	public float seconds = 0.3f;// Add Fields
	public bool rotating, reset, DoorOpened;
	public int counter, MyCounter;
	public GameObject door, player;
	public Interaction interaction;
	Quaternion MyRotation;//
	Vector3 MyPosition;
	public Vector3 sleepposition;
	void Start(){
		counter = MyCounter;
		MyRotation = transform.rotation; //Cache intial rotation
		MyPosition = transform.position; //Cache intial Position
		sleepposition = new Vector3 (transform.position.x, -0.5f, transform.position.z);
		interaction = player.GetComponentInChildren<Interaction> ();
	}
	
	public enum States //States
	{
		Initialize,//Declare States
		Awake,
		Rotate,
		Sleep,
		move,
	}
	public States currentState = States.Awake; //create an instance of the Enum, set to Initialize
	void Update ()
	{	
		switch(currentState) //pass in current state
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
		case States.move:
			move();
			break;
		}
		//Debug.Log (currentState);
		Debug.DrawRay(transform.position, transform.transform.forward * 10f, Color.magenta);
	}
	void Initilize()
	{
		StartCoroutine (Rotateme (-transform.localEulerAngles));
		if (counter == interaction.keyrotation){
			renderer.material.color = Color.green;
		}
	}
	public void Awake()
	{
		if(transform.position.y < MyPosition.y){
			transform.Translate (Vector3.up * 3 * Time.deltaTime); //Lowers Cubes once reachs position
		}
		else
			renderer.material.color=Color.red;

		if (door.GetComponent<Door> ().currentState == Door.States.Opened) {
			currentState = States.move;
		}

	}
	void Rotate()
	{
		StartCoroutine(Rotateme(Vector3.up * 90));
	}
	void Sleep()
	{
		if (door.GetComponent<Door>().Doorsize>=1) {
			door.GetComponent<Door>().currentState=Door.States.Closed;
			}
		 else
			door.GetComponent<Door>().currentState=Door.States.Opened;
			currentState = States.Awake;
		if (door.GetComponent<Door>().Doorsize<= 1) {
			reset=true;
			currentState=States.move;
		}
	}
	void move(){
		renderer.material.color = Color.white;
		if (transform.position.y > sleepposition.y) {
			transform.Translate (Vector3.down * 3 * Time.deltaTime); //Lowers Cubes once reachs position
		} if (transform.position.y < sleepposition.y && reset==false) {
			 currentState = States.Sleep;
		}
		else
			currentState=States.move;
	}

		
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
			if(currentState==States.Rotate){
				transform.rotation = Quaternion.Slerp (startRotation, endRotation, time); //Rotates to desired rotation
			}
			else{
				reset=true;
				transform.rotation = Quaternion.Slerp (startRotation, MyRotation, time);//resets to original rotation
			}
			yield return null;//continue to next line and returns 0
		}
		//print("Turned 90 degrees");
		counter= (counter+1) % 4; //Resets once reachs 4
		if (reset) { // excuted when Switch is pressed 
			counter = MyCounter;//
			reset = false;//
		}
		rotating = false; //deactivate boolean
		interaction.gotem = true;
		currentState = States.Awake; //Set back tp awake
	}
} 