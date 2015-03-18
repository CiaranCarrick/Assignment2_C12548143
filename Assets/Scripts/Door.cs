using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public Vector3 openpos = new Vector3 (-1.5f, -1.5f, 1.5f);
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
		if (transform.position.y >= openpos.y) {
			transform.Translate (Vector3.down * 1 * Time.deltaTime);
		} else
			currentState = States.Opened;
			renderer.material.color = Color.green;
	}
	void Closed(){
		renderer.material.color = Color.red;
	}
}
