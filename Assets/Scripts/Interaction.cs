using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public GameObject[] cube;
	public GameObject door;
	// Use this for initialization
	void Start () {
		cube = GameObject.FindGameObjectsWithTag("RotationCube");
		
		for(int i = 0; i < cube.Length; i++)
		{
			//Debug.Log("Cube "+i+" "+cube[i].name);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Interact ();
	}
	
	void Interact() {
		if (Input.GetKeyDown(KeyCode.E)) 
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, 3f) && door.GetComponent<Door>().currentState==Door.States.Closed)
			{
				if (hit.collider.GetComponent<RotateCube>()!=null){ //Check if object has Keycards Script
				//Debug.Log(hit.collider.GetComponent<RotateCube>().currentState);
					if(hit.collider.GetComponent<RotateCube>().currentState==RotateCube.States.Awake)
					{
					hit.collider.GetComponent<RotateCube>().currentState=RotateCube.States.Rotate;
					}
				}
				for (int i = 0; i < cube.Length; i++) 
				{ 
					if (hit.collider.gameObject.name==("Door")){
						if(cube[0].GetComponent<RotateCube>().counter==2 && cube[1].GetComponent<RotateCube>().counter==2){
							hit.collider.GetComponent<Door>().currentState=Door.States.Opened;
							cube[i].GetComponent<RotateCube>().currentState=RotateCube.States.Initialize;
						}
						else
							cube[i].GetComponent<RotateCube>().currentState=RotateCube.States.Initialize;
					}
				}//end for loops
					
				}//end
			}//end ray
		}//end input
}
