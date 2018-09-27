using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.UI;


public class Counting_Object : MonoBehaviour {
	GameObject _DroneData;
	DroneData _dm;
	public Text txtCountList;
	// Use this for initialization
	void Start () {
		_DroneData = GameObject.Find ("ScriptObject");		
		_dm = (DroneData)_DroneData.GetComponent ("DroneData");
		txtCountList = (Text)GetComponent ("Text");
	}
	
	// Update is called once per frame
	void Update () {
		int[] newcounts = new int[10];

		_dm.Counting_Object_Count (newcounts);
		txtCountList.text = "1~10 : " + newcounts [0] +"\n";
		txtCountList.text += "10~20 : " + newcounts [1] +"\n";
		txtCountList.text += "20~30 : " + newcounts [2] +"\n";
		txtCountList.text += "30~40 : " + newcounts [3] +"\n";
		txtCountList.text += "40~50 : " + newcounts [4] +"\n";
		txtCountList.text += "50~60 : " + newcounts [5] +"\n";
		txtCountList.text += "60~70 : " + newcounts [6] +"\n";
		txtCountList.text += "70~80 : " + newcounts [7] +"\n";
		txtCountList.text += "80~90 : " + newcounts [8] +"\n";
		txtCountList.text += "90~100 : " + newcounts [9] +"\n";


	}
}
