using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public GameObject[] cube;
	public GameObject door;
	public int keyrotation;

	// Use this for initialization
	void Start () {
		keyrotation = 2;//Intially set keycombination to be 2
		cube = GameObject.FindGameObjectsWithTag("RotationCube"); //Locte Rotation and Add to Cube[]
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
				if (hit.collider.GetComponent<RotateCube>()!=null){ //Check if object has Rotate Script
					if(hit.collider.GetComponent<RotateCube>().currentState==RotateCube.States.Awake)
					hit.collider.GetComponent<RotateCube>().currentState=RotateCube.States.Rotate;
				}
				if (hit.collider.gameObject.name==("Switch")){

				for (int i = 0; i < cube.Length; i++) 
				{ 
						if(cube[0].GetComponent<RotateCube>().counter==keyrotation && cube[1].GetComponent<RotateCube>().counter==keyrotation ){
							cube[i].GetComponent<RotateCube>().currentState=RotateCube.States.Initialize;
							door.GetComponent<Door>().currentState=Door.States.Opened;
							keyrotation=Random.Range(0,4);
						}
						else
							cube[i].GetComponent<RotateCube>().currentState=RotateCube.States.Initialize; // reset Cube
					}
				}//end for loops
					
				}//end
			}//end ray
		}//end input
}
