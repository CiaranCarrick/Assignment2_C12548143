using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public static float myro;
	public GameObject cube;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Interact ();
	}
	
	void Interact() {
		if (Input.GetKeyDown(KeyCode.E)) 
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, 3f))
			{
				//if (hit.collider.GetComponent<KeyCards>()!=null) //Check if object has Keycards Script
				//{
					if (hit.collider.GetComponent<RotateCube>()!=null){ //Check if object has Keycards Script
					Debug.Log(hit.collider.GetComponent<RotateCube>().currentState);
						//{
						if(hit.collider.GetComponent<RotateCube>().currentState==RotateCube.States.Sleep)
						{
						hit.collider.GetComponent<RotateCube>().currentState=RotateCube.States.Rotate;
						}
					}
				if (hit.collider.gameObject.name==("Door") &&myro==90f){
					Destroy(hit.collider.gameObject);
				}
				if (hit.collider.gameObject.name==("Door") &&myro!=90f){
					cube.GetComponent<RotateCube>().currentState=RotateCube.States.Initialize;
				}

			}
		}//end input
	}//end interact
}
