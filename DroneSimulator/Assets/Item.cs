using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[Serializable]
public class Item
{
	public int point;
	public float x;
	public float y;
	public float z;
	public float speed;
}
[Serializable]
public class DronePointData
{
	private Vector3 vData;
	public List<Item> PointList;
	public DronePointData()
	{
		vData = new Vector3 ();	
		PointList = new List<Item>();
	}
	public void Clear()
	{
		PointList.Clear ();
	}
	public void AddData(float x,float y,float z, float speed)
	{
		Item _item = new Item();
		_item.x = x;
		_item.y = y;
		_item.z = z;
		_item.speed = speed;
		PointList.Add (_item);
	}
	public Vector3 GetPoint_Vector3(int iIndex)
	{
		if (CheckIndex (iIndex)) {
			vData.x = PointList [iIndex].x;
			vData.y = PointList [iIndex].y;
			vData.z = PointList [iIndex].z;
		}

		return vData;
	}
	public float GetSpeed(int iIndex)
	{
		float fSpeed = 0;
		if (CheckIndex (iIndex)) {
			fSpeed = PointList [iIndex].speed;
		}


		return fSpeed;
	}
	public int GetPointCount()
	{
		return PointList.Count;	
	}
	private bool CheckIndex(int iIndex)
	{
		if (iIndex >= 0 && iIndex < PointList.Count)
			return true;
		return false;
	}
		
}