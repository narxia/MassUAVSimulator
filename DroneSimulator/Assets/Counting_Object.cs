using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
using UnityEngine.UI;


public class Counting_Object : MonoBehaviour {
	GameObject _DroneData;
	DroneData _dm;
    Preference _prof;
    public Text txtCountList;
    Dictionary<string, int> dCount;
	// Use this for initialization
	void Start () {
		_DroneData = GameObject.Find ("ScriptObject");		
		_dm = (DroneData)_DroneData.GetComponent ("DroneData");
        _prof = (Preference)_DroneData.GetComponent("Preference");
        txtCountList = (Text)GetComponent ("Text");
        dCount = new Dictionary<string, int>();

    }
	
	// Update is called once per frame
	void Update () {
        
        bool bChange=false;
        //int iClass=0;
        int iArea = 0;
        foreach(GameObject go in _dm.DroneObjectList)
        {
            Vector3 _point = go.GetComponent<DroneManager>().getCurrentPoint();
            foreach (Area _area in _prof.AreaList)                          
            //for (int iClass = 0; iClass < _prof.ClassList.Count; iClass++)
            {
                //foreach (float fclass in _prof.ClassList)
                for (int iClass = 0; iClass < _prof.ClassList.Count; iClass++)
                {
                    float top = _prof.ClassList[iClass];
                    float bottom = 0;
                    if(iClass!=0)
                    {
                        bottom = _prof.ClassList[iClass-1];
                    }

                    if ((_point.x >= _area.Point_Start.x && _point.x <= _area.Point_End.x)  
                        &&(_point.y >= _area.Point_Start.y && _point.y <= _area.Point_End.y) 
                        &&(_point.z >= bottom && _point.z <= top))
                    {
                        string sName = "Area";
                        sName += "_";
                        sName += (char)((int)'A' + iClass);
                        sName += "-" + (iArea + 1).ToString();
                        int iCount = 0;

                        if(dCount.ContainsKey(sName))
                        {
                            iCount = dCount[sName];
                        }
                        iCount++;
                        dCount[sName] = iCount;
                        bChange = true;
                    }
                    //iClass++;
                }
                iArea++;
                //iClass = 0;
            }
            iArea = 0;
        }
        //		txtCountList.text = "1~10 : " + newcounts [0] +"\n";
        if (bChange)
        {
            if (txtCountList)
            {
                txtCountList.text = "";

                foreach (KeyValuePair<string, int> it in dCount)
                {
                    txtCountList.text += it.Key + " : " + it.Value + "\n";
                }
            }
            else
            {
                txtCountList = (Text)GetComponent("Text");
            }
        }
        dCount.Clear();


    }
    

}
