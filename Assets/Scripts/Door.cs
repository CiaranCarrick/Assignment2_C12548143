using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject Switch;
	public float Doorsize;
	public float width; // Variable to hold localscale.x


	void Start(){
		Doorsize=100; //100 indicates doors max scale
		width = transform.localScale.x;
		Switch = GameObject.Find ("Stand/Switch");//Make Referance to Child object Switch
	}
	public enum States //our states
	{
		Opened,
		Closed,
	}
	public States currentState = States.Closed; //create an instance of the enum and set it's default to Initialize
	void Update ()
	{
		Switch.renderer.material.color = this.renderer.material.color; //Switch colour dictated by Door State Green(Opened) or Red(Closed)
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
		float timer=0.8f; //Doors closing speed
		Vector3 pos = transform.position;
		Vector3 scale = transform.localScale;
		if (Doorsize >= 1 && currentState==States.Opened) {
			Doorsize -= timer;
			scale.x = (width / 100) * Doorsize;//Helps shrink door over time
			pos.y = transform.position.y - ((transform.localScale.x - scale.x) / 2);
			transform.localScale = scale;//Assign Transforms new values
			transform.position = pos;//
		}
			else
			currentState = States.Opened;
			renderer.material.color = Color.green;
	}

	void Closed(){
		renderer.material.color = Color.red;
	}
}
