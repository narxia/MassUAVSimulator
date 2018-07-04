using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//[Serializable]
public class SettingMain : MonoBehaviour {
	/// <summary>
	/// 아이템으로 사용할 오브젝트(프리팹)
	/// </summary>
	public GameObject ItemObject;
	/// <summary>
	/// 아이템이 추가될 오브젝트
	/// </summary>
	public Transform Content;

	/// <summary>
	/// 바인딩할 리스트
	/// </summary>
	public List<Item> ItemList;

	// Use this for initialization
	void Start () {

		//this.AddItem();
		this.Binding();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	private void Binding()
	{
		GameObject btnItemTemp;
		ItemObject itemObjectTemp;

		int i = 1;
		foreach (Item item in this.ItemList) 
		{
			btnItemTemp = Instantiate (this.ItemObject,this.Content) as GameObject;
			itemObjectTemp = btnItemTemp.GetComponent<ItemObject> ();
			InputField ifX = itemObjectTemp.txtX.GetComponent<InputField> ();
			InputField ifY = itemObjectTemp.txtY.GetComponent<InputField> ();
			InputField ifZ = itemObjectTemp.txtZ.GetComponent<InputField> ();
			InputField ifSpeed = itemObjectTemp.txtSpeed.GetComponent<InputField> ();

			itemObjectTemp.txtTitle.text = "Point" + (i++).ToString();
			ifX.text = item.x.ToString();
			ifY.text = item.y.ToString();
			ifZ.text = item.z.ToString();
			ifSpeed.text = item.speed.ToString();
			//btnItemTemp.transform.SetParent (this.Content);
		}
	}
	public void AddItem()
	{
		Item itemTemp = new Item();
		itemTemp.x = UnityEngine.Random.Range (-10, 10) ;
		itemTemp.y = UnityEngine.Random.Range (-10, 10) ;
		itemTemp.z = UnityEngine.Random.Range (-10, 10) ;
		itemTemp.speed = UnityEngine.Random.Range (-10, 10) ;
		this.ItemList.Add(itemTemp);

	}
//	public Vector3 GetPoint(int iIndex)
//	{
//		Item itemTemp = this.ItemList.FindIndex(iIndex);
//		Vector3 v3 = new Vector3 (itemTemp.x, itemTemp.y, itemTemp.z);
//		return v3;
//	}

}
