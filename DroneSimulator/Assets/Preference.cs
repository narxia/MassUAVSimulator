using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

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
		//AreaList.Add(new Area(new Vector2(0,0),new Vector2(100,100)));
		//ClassList.Add (100);
		ReadCSVFile ();
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
					for (int j = 0; j < ClassList.Count; j++) {	
						GameObject Create_Obj;
						GameObject Org_Obj;
						float height = ClassList [j];
						float floor = 0;
						if (j > 0)
							floor = ClassList [j - 1];
						float width = Math.Abs (AreaList [i].Point_End.x - AreaList [i].Point_Start.x);
						float depth = Math.Abs (AreaList [i].Point_End.y - AreaList [i].Point_Start.y);
						Org_Obj = GameObject.Find ("Area");
						Create_Obj = Instantiate (Org_Obj, new Vector3 (), Org_Obj.transform.rotation);	
						Create_Obj.transform.position = Create_Obj.transform.position + new Vector3 (width/2, (height-floor)/2, depth/2); // Anchor Point Move
						//Debug.Log(Create_Obj.transform.position);
						Create_Obj.transform.position = Create_Obj.transform.position + new Vector3 (AreaList [i].Point_Start.x,floor, AreaList [i].Point_Start.y); // Move Start Position
						//Debug.Log(Create_Obj.transform.position);
						Create_Obj.transform.localScale = new Vector3 (width, height-floor, depth);

						AreaObjctList.Add (Create_Obj);
					}

				}
			}
		}
	}
	public void ReadCSVFile()
	{
		string strFile= "Preference.csv";

		//FILE OPEN
		using (FileStream fs = new FileStream(strFile, FileMode.Open)) 
		{

			using (StreamReader sr = new StreamReader(fs, Encoding.UTF8, false))
			{
				string strLineValue = null;
				string[] values = null;
				string[] PointValue = null;
				bool bfirstLine = true;
				while ((strLineValue = sr.ReadLine()) != null)
				{
					
					// Must not be empty.
					if (string.IsNullOrEmpty(strLineValue)) return;
					if (bfirstLine) {
						bfirstLine = false;
						continue;
					}

					values = strLineValue.Split(',');

					// Output first Column Skip
					//_dronePoint.Clear ();
					/*
					 *  CSV FILE
					 *  POINT_START_X,POINT_START_Y,POINT_END_X,POINT_END_X,CLASS
					 * */

					if (values.Length >= 6) {
						float x1 = -1;
						float y1 = -1;
						float x2 = -1;
						float y2 = -1;
						float f_class = -1;

						if(values [1] !="")
							x1 = System.Convert.ToSingle (values [1]);
						if(values [2] !="")
							y1 = System.Convert.ToSingle (values [2]);
						if(values [3] !="")
							x2 = System.Convert.ToSingle (values [3]);
						if(values [4] !="")
							y2 = System.Convert.ToSingle (values [4]);
						if(values [5] !="")
							f_class = System.Convert.ToSingle (values [5]);
						if((x1>-1)&&(y1>-1)&&(x2>-1)&&(y2>-1))
							AreaList.Add(new Area(new Vector2(x1,y1),new Vector2(x2,y2)));
						if(f_class >-1)
							ClassList.Add (f_class);
					}


				}
			}  
		}


	}

}
