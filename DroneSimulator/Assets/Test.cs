using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
        GameObject org_cube;
        GameObject Create_Cube;
        org_cube = GameObject.Find("cbTest");
        Create_Cube = Instantiate(org_cube, org_cube.transform.position, org_cube.transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
        
    }
}
