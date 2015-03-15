using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public enum States //our states
	{
		Opened,
		Closed,
	}
	public States currentState = States.Closed; //create an instance of the enum and set it's default to Initialize
	void Update ()
	{
		switch (currentState) 
		{ //pass in the current state
		case States.Opened:
				Opened();
				break;
		case States.Closed:
				Closed();
				break;
		}
	}

	void Opened(){
		if (transform.position.y < 5) {
			transform.Translate (Vector3.up * 1 * Time.deltaTime);
		}
	}
	void Closed(){
		renderer.material.color = Color.red;
	}
}
