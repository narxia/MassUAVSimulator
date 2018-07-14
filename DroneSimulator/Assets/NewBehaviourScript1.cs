using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewBehaviourScript1 : MonoBehaviour {
    float x;
    float y;
    float z;
	Vector3 point1;
	Vector3 point2;
	Vector3 point3;
	Vector3 point4;
	Vector3 Target;
	int iCurrentPoint=0;
    // Use this for initialization
    void Start () {


		Generate_Point ();
		Target = point1;
        //transform.position = new Vector3(x, y, z);
        //transform.Translate(speed, 0, 0);		
    }

    // Update is called once per frame
    void Update () {
        

		transform.position = Vector3.MoveTowards(transform.position, Target,0.1f);
		check_NextPoint (transform.position, Target);

	}
	void check_NextPoint(Vector3 curr,Vector3 tgt)
	{
		if (curr == tgt) {
			if (tgt == point1) {
				Target = point2;	
			}
			else if (tgt == point2) {
				Target = point3;	
			}
			else if (tgt == point3) {
				Target = point4;	
			}
		}
	}
	void Read_Point()
	{
		
		GameObject.Find ("Camera").GetComponent("SettingMain");
	}
	void Generate_Point()
	{
		point1 = new Vector3 (Random.value * 10, Random.value *  10, Random.value *  10);
		point2 = new Vector3 (Random.value *  10, Random.value *  10, Random.value *  10);
		point3 = new Vector3 (Random.value * 10, Random.value * 10, Random.value *  10);
		point4 = new Vector3 (Random.value * 10, Random.value *10, Random.value *  10);		
	}
	 
	public void Create_cube()
	{
		GameObject Create_Cube;
		GameObject org_cube;
		org_cube = GameObject.Find("DronePatrol");
		Vector3 startPos = new Vector3 (0, 0, 0);
		Create_Cube = Instantiate(org_cube, startPos, org_cube.transform.rotation);
	}
	public void ChangeSettingScene()
	{
		SceneManager.LoadScene("Setting");
		//SceneManager.LoadScene (Setting);
	}

}
