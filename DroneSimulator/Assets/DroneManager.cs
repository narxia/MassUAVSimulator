using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour {
	DronePointData _DronePointData;
	int currentPoint;
	LineRenderer _Line;
	Color _LineColor;
	bool bInit=true;
	// Use this for initialization
	void Start () {
		currentPoint = 0;	
		_Line = GetComponent<LineRenderer> ();
		_Line.loop = false;

		_Line.startWidth = 0.1f;
		_Line.endWidth = 0.1f;


		_Line.positionCount = 1;
		_Line.enabled = false;

	}
	void OnCollisionEnter(Collision col) {
		Debug.Log ("Col : "+ col.gameObject.name);
	}
	void onTriggerEnter(Collider col)
	{
		Debug.Log (col.gameObject.name);
	}
	// Update is called once per frame
	void Update () {
		if (_DronePointData != null) {
			//this.gameObject.SetActive(true);
			if (bInit) {
				bInit = false;
				_Line.startColor = _LineColor;
				_Line.endColor = _LineColor;
				if (_Line.GetPosition (0) == Vector3.zero) {
					_Line.SetPosition (0, transform.position);	
					_Line.enabled = true;
				}
			}
			transform.position = Vector3.MoveTowards (transform.position, getCurrentPoint (), getCurrentSpeed()); //다음 포지션으로 이동 
			check_NextPoint (transform.position);
			if (_Line != null) {
				_Line.SetPosition (currentPoint, transform.position); //Line 그림
			}



		} else {
			//this.gameObject.SetActive(false);
		}


	}
	//현재 드론이 다음 위치까지 왔는지 확인
	private void check_NextPoint(Vector3 curr)
	{
		if (curr == getCurrentPoint()) {
			if (currentPoint < _DronePointData.GetPointCount ()) {
				++currentPoint;	
				_Line.positionCount = currentPoint+1; 	

			}
		}
	}
	//현재 목표 Point 값 전달
	public Vector3 getCurrentPoint()
	{
		return _DronePointData.GetPoint_Vector3 (currentPoint);		
	}
	// 다음 목표 Point 값 전달
	private Vector3 getNextPoint()
	{
		return _DronePointData.GetPoint_Vector3 (currentPoint+1);		
	}
	
	private float getCurrentSpeed()
	{
		return _DronePointData.GetSpeed (currentPoint);
	}
	//외부에서 Point 데이터 전달
	public void SetPointData(DronePointData _PointData)
	{
		_DronePointData = _PointData;	
	}
	//현재 몇번째 Point 인지 알려줌
	public int getCurrentPointCount()
	{
		return currentPoint;	
	}
	//Line 색상 지정
	public void SetColor(Color _color)
	{
		_LineColor = _color;	
	}
	//Line  을 보일건지 안보일건지 설정하는 함수
	public void SetLineActive(bool _bset)
	{
		_Line.enabled = _bset;	
	}
}
