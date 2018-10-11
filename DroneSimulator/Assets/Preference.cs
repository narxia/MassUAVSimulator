using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Area
{
	public Vector2 Point_Start;
	public Vector2 Point_End;
	public Area(Vector2 start,Vector2 end)
	{
		Point_Start = start;
		Point_End = end;
	}
}
public class Preference : MonoBehaviour {
	public List<Area> AreaList; //
	public List<float> ClassList; 
	public Vector3 World_size;
	private Vector3 before_size;
	private List<GameObject> AreaObjctList;
	public bool bRefresh;

	GameObject Floor;
	GameObject Sky;
	// Use this for initialization
	void Start () {
		World_size.x = 1000;
		World_size.y = 1000;
		World_size.z = 1000;

		AreaList = new List<Area> ();
		ClassList = new List<float> ();
		AreaObjctList = new List<GameObject> ();

		Floor = GameObject.Find ("Floor");	
		Sky = GameObject.Find ("Height");	

		AreaList.Clear ();

		ClassList.Clear ();
		AreaObjctList.Clear ();
		bRefresh = true;
		//todo: Test
		AreaList.Add(new Area(new Vector2(0,0),new Vector2(100,100)));
		ClassList.Add (100);

	}

	// Update is called once per frame
	void Update () {
		//Floor,Sky Setting
		if ( World_size!= before_size) {
			if (Floor.transform.localScale.x != World_size.x || Floor.transform.localScale.z != World_size.y) {
				Floor.transform.localScale = new Vector3 (World_size.x, 1, World_size.y);	
				Floor.transform.position = new Vector3 (0, 0, World_size.x / 2);
			}
			if (Floor.transform.localScale.x != World_size.x || Floor.transform.localScale.z != World_size.y|| Sky.transform.localScale.y != World_size.z) {
				Sky.transform.localScale = new Vector3 (World_size.x, World_size.z,1 );	
				Sky.transform.position = new Vector3 (0, 0, World_size.y);
			}
			before_size = World_size;
		}
		//Area Making
		if (AreaList.Count > 0 && ClassList.Count > 0) {
			if (bRefresh) {
				bRefresh = false;
				//Clear Object
				for (int i=0; i < AreaObjctList.Count; i++) {
					Destroy (AreaObjctList [i]);	
				}
				AreaObjctList.Clear ();
				//Create Object
				for(int i=0;i<AreaList.Count;i++)
				{					
					GameObject Create_Obj;
					GameObject Org_Obj;
					float width = Math.Abs(AreaList[i].Point_End.x-AreaList[i].Point_Start.x);
					float height= Math.Abs(AreaList[i].Point_End.y-AreaList[i].Point_Start.y);
					Org_Obj = GameObject.Find ("Area");
					Create_Obj = Instantiate (Org_Obj, new Vector3 (AreaList[i].Point_Start.x,AreaList[i].Point_Start.y,1), Org_Obj.transform.rotation);	
					Create_Obj.transform.localScale = new Vector3 (width,100 , height);
					AreaObjctList.Add (Create_Obj);

				}
			}
		}
	}
		

}
