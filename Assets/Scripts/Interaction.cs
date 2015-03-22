using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	public bool displaybox = false; //Displays GUI box when raycast hights interaction layers
	public bool Checkcubes=true; //Bool to check if all cube[] elements are equal

	public GameObject[] cube;
	public GameObject door;
	public Door doorscript;
	public int keyrotation;
	
	// Use this for initialization
	void Start () {
		Checkcubes=true;
		keyrotation = 2;//Intially set keycombination to be 2
		cube = GameObject.FindGameObjectsWithTag("RotationCube"); //Locte Rotation and Add to Cube[]
		doorscript = door.GetComponent<Door> ();
	}
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(transform.position, transform.forward * 3f, Color.green);
		Interact ();
	}

	void Interact() {
		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.forward, out hit, 3f, 1<<8) && doorscript.currentState==Door.States.Closed)
		{
			displaybox=true;
			if (Input.GetKeyDown(KeyCode.E)) 
			{
				if (hit.collider.GetComponent<RotateCube>()!=null) { //Check if object has Rotate Script
					if (hit.collider.GetComponent<RotateCube>().currentState==RotateCube.States.Awake)
						hit.collider.GetComponent<RotateCube>().currentState=RotateCube.States.Rotate;
				}
				if (hit.collider.gameObject.name==("Switch")) {
					for (int i = 0; i < cube.Length; i++)//Iterate through Cube array
					{ 
						if (cube[i].GetComponent<RotateCube>().counter!=keyrotation ) { //if any cubes don't equal keyrotation, set false and reset each cube and break from loop
							Checkcubes=false;
							foreach(GameObject c in cube){
							c.GetComponent<RotateCube>().currentState=RotateCube.States.Initialize;
							}
							break; //iterates through loop then breaks all cubes rather than cube[0]
						}
					}
					if(Checkcubes){// if true, search all cubes and rotate, open door and randomize keyrotation
						foreach(GameObject c in cube){
							c.GetComponent<RotateCube>().currentState=RotateCube.States.Initialize; // reset Cube
						}
						doorscript.currentState=Door.States.Opened;
						keyrotation=Random.Range(0, 4);
					}
				}//end for loops
			}//end
		}//end ray
		else
			displaybox=false;

	}//end input
	void OnGUI(){
		if (displaybox) {
			GUI.Box(new Rect(Screen.width/2-25, Screen.height /2-25, 50, 20),"E");
		}
	}
}
