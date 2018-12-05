using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class DroneData  : MonoBehaviour{
	
	public List<DronePointData> DroneDataList; //Drone Data List
	public List<GameObject> DroneObjectList; //Drone Object  List 재 로딩시에 편게 사용하기 위해서
	private List<Color> ColorList; // Tracking Line List
	int iColorCount=0;
	bool bLoaded = false;
	void Start () {
		DroneDataList = new List<DronePointData> ();	
		DroneObjectList = new List<GameObject> ();
		ColorList = new List<Color> ();
	}
	public void LoadDroneObject()
	{
		iColorCount = 0;
		float colorFactor = 3.0f/DroneDataList.Count;
		ColorList.Clear ();
		// 드론 Tracking Line 색상을 다르게 하기 위해서
		for(int i = 0;i  < DroneDataList.Count;i++)
		{			
			float fColorValue = i*colorFactor;
			Color _color = new Color (fColorValue - 2, fColorValue - 1, fColorValue, 1);
			ColorList.Add(_color);	
		}
		//Drone Object List 에 생성한 드론 추가
		for (int iDroneObject = 0; iDroneObject < DroneDataList.Count; iDroneObject++) 
		{
			DroneObjectList.Add (MakeDrone (DroneDataList[iDroneObject],iDroneObject));	
		}
		bLoaded = true;
	}
    // 드론을 DronePatrol 에서 복사함
    private GameObject MakeDrone(DronePointData _data, int iNo)
	{
		
		GameObject Create_Drone;
		GameObject org_Drone;
		org_Drone = GameObject.Find("DronePatrol");

		Vector3 startPos = _data.GetPoint_Vector3 (0);
		Create_Drone = Instantiate(org_Drone, startPos, org_Drone.transform.rotation);	
		DroneManager _dm = Create_Drone.GetComponent<DroneManager>(); // 드론 오브젝트 내의 원활한 데이터 처리를 위한 클래스를 불러옴.
		_dm.SetPointData (_data); //Point 데이터리스트를 전달
		_dm.SetColor (ColorList[iColorCount++]); //Line 색상을 전달
        Create_Drone.name = "Drone" + iNo.ToString();

        return Create_Drone;

	}
	// 리스트에 있는 모든 드론을 삭제
	public void ClearDroneObject()
	{
		for (int i = 0; i < DroneObjectList.Count; i++) {
			Destroy (DroneObjectList [i]);	
		}	
		bLoaded = false;
		DroneDataList.Clear ();	
		DroneObjectList.Clear ();
	}
	//CSV 파일 처리
	public void ReadCSVFile()
	{
		string strFile= "DroneData.csv";
		ClearDroneObject ();
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
					DronePointData _dronePoint = new DronePointData ();
					// Must not be empty.
					if (string.IsNullOrEmpty(strLineValue)) return;
					if (bfirstLine) {
						bfirstLine = false;
						continue;
					}
					values = strLineValue.Split(',');
                    //fist Line -> Collision Size
					// Output first Column Skip
					//_dronePoint.Clear ();
					for (int nIndex = 1; nIndex < values.Length; nIndex++)
					{
						
						PointValue = values [nIndex].Split (' ');
						float fx = System.Convert.ToSingle (PointValue [0]);
						float fy = System.Convert.ToSingle (PointValue [1]);
						float fz = System.Convert.ToSingle (PointValue [2]);
						float fspeed = System.Convert.ToSingle (PointValue [3]);

						Mathf.Clamp (fx, 0, 5000);
						Mathf.Clamp (fy, 0, 5000);
						Mathf.Clamp (fz, 0, 500);
						_dronePoint.AddData (fx, fz,fy, fspeed); //Unity 좌표계 때문에 Y와  Z를 바꿔서 전달 및 사용
					}
					DroneDataList.Add (_dronePoint);
				}
			}  
		}
		LoadDroneObject ();

	}
	// Tracking Line 끄고 키는 기능 
	public void ToggleLineTracking(bool _bset)
	{
		for (int i = 0; i < DroneObjectList.Count; i++) {			
				DroneManager _dm = DroneObjectList [i].GetComponent<DroneManager>();
			_dm.SetLineActive (_bset);
		}
	}
	//Todo Area Check
	public int[] Counting_Object_Count(int[] iCount_List)
	{
		if (bLoaded ) {
			for (int i = 0; i < DroneObjectList.Count; i++) {			
				
				float height = DroneObjectList [i].GetComponent<DroneManager>().getCurrentPoint ().z;
				if (height < 10) {
					iCount_List [0]++;
				} else if (height < 20) {
					iCount_List [1]++;
				}
				else if (height < 30) {
					iCount_List [2]++;
				}
				else if (height < 40) {
					iCount_List [3]++;
				}
				else if (height < 50) {
					iCount_List [4]++;
				}
				else if (height < 60) {
					iCount_List [5]++;
				}
				else if (height < 70) {
					iCount_List [6]++;
				}
				else if (height < 80) {
					iCount_List [7]++;
				}
				else if (height < 90) {
					iCount_List [8]++;
				}
				else {
					iCount_List [9]++;
				}

			}
		}
		return iCount_List;
	}
}
