using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
public class Area
{
	Vector3 Point_Start;
	Vector3 Point_End;
}
public class Preference : MonoBehaviour {
[CustomEditor(typeof(Preference))]
[CanEditMultipleObjects]
//public class Preference : Editor {
	public List<Area> AreaList; //Drone Data List
	public Vector3 World_size;
	private Vector3 before_width;


	GameObject Floor;
	GameObject Sky;
	// Use this for initialization
	void Start () {
		World_size.x = 1000;
		World_size.y = 1000;
		World_size.z = 1000;
		Floor = GameObject.Find ("Floor");	
		Sky = GameObject.Find ("Height");	

		Debug.Log (Floor.transform.localScale.x);
		Debug.Log (Floor.transform.localScale.y);
		Debug.Log (Floor.transform.localScale.z);
	}

	// Update is called once per frame
	void Update () {
	
		if ( World_size!= before_width) {
			if (Floor.transform.localScale.x != World_size.x || Floor.transform.localScale.z != World_size.y) {
				Floor.transform.localScale = new Vector3 (World_size.x, 1, World_size.y);	
				Floor.transform.position = new Vector3 (0, 0, World_size.x / 2);
			}
			if (Floor.transform.localScale.x != World_size.x || Floor.transform.localScale.z != World_size.y|| Sky.transform.localScale.y != World_size.z) {
				Sky.transform.localScale = new Vector3 (World_size.x, World_size.z,1 );	
				Sky.transform.position = new Vector3 (0, 0, World_size.y);
			}
			before_width = World_size;
		}


	}
		

}
