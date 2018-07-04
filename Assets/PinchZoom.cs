using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoom : MonoBehaviour {
	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
	public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
	public float horizontalMouseSpeed= 40.0f;
	public float verticalMouseSpeed  = 40.0f;

	public Transform m_kTarget;
	public float  m_fDistance = 10.0f;
	public float  m_fxSpeed = 250.0f;
	public float  m_fySpeed = 120.0f;

	public float  m_fyMinLimit = -90f;
	public float  m_fyMaxLimit = 90f;

	private float x = 0.0f;
	private float y = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;
	}
	
	// Update is called once per frame
	void Update () {
		// If there are two touches on the device...

		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			// If the camera is orthographic...
			if (Camera.main.orthographic)
			{
				// ... change the orthographic size based on the change in distance between the touches.
				Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

				// Make sure the orthographic size never drops below zero.
				Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
			}
			else
			{
				
				// Otherwise change the field of view based on the change in distance between the touches.
				Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

				// Clamp the field of view to make sure it's between 0 and 180.
				Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
			}
		}   
//		while (Input.GetMouseButtonDown(1)) {
//			float h= horizontalMouseSpeed * Input.GetAxis ("Mouse Y");
//			 float v = verticalMouseSpeed * Input.GetAxis ("Mouse X");
//			transform.Translate(v,h,0);
//		}
    }
	public void zoomin()
	{
		float deltaMagnitudeDiff =10;
//		if (Camera.main.orthographic)
//		{
//			// ... change the orthographic size based on the change in distance between the touches.
//			Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
//
//			// Make sure the orthographic size never drops below zero.
//			Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
//		}
//		else
//		{
//
//			// Otherwise change the field of view based on the change in distance between the touches.
//			Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
//			Debug.Log (Camera.main.fieldOfView);
//			// Clamp the field of view to make sure it's between 0 and 180.
//			Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
//			Debug.Log (Camera.main.fieldOfView);
//
//		}
		Vector3 v3 = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z+deltaMagnitudeDiff);
		Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, v3,1);
	}
	public void zoomOut()
	{
		float deltaMagnitudeDiff =-10;
//		if (Camera.main.orthographic)
//		{
//			// ... change the orthographic size based on the change in distance between the touches.
//			Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
//
//			// Make sure the orthographic size never drops below zero.
//			Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize, 0.1f);
//		}
//		else
//		{
//
//			// Otherwise change the field of view based on the change in distance between the touches.
//			Camera.main.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
//			Debug.Log (Camera.main.fieldOfView);
//			// Clamp the field of view to make sure it's between 0 and 180.
//			Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 0.1f, 179.9f);
//			Debug.Log (Camera.main.fieldOfView);
//
//		}
		Vector3 v3 = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,Camera.main.transform.position.z+deltaMagnitudeDiff);
		Camera.main.transform.position = Vector3.MoveTowards (Camera.main.transform.position, v3,1);
	}	
	public void LateUpdate()
	{
		x += Input.GetAxis("Mouse X") * m_fxSpeed * 0.02f;
		y -= Input.GetAxis("Mouse Y") * m_fySpeed * 0.02f;

		y = ClampAngle(y, m_fyMinLimit, m_fyMaxLimit);

		Quaternion rotation = Quaternion.Euler(y, x, 0);
		Vector3 position = transform.position;

		if (m_kTarget) {
			if(Input.GetMouseButtonDown (0)) {	
				position = rotation * new Vector3 (0.0f, 0.0f, -m_fDistance);
				position += m_kTarget.position;
				transform.rotation = rotation;
				transform.position = position;
			}


		}
		else
		{
			if (Input.GetKey(KeyCode.W))
			{
				position += (rotation * new Vector3(0.0f, 0.0f, 1.0f));
			}
			if(Input.GetKey(KeyCode.S))
			{
				position += (rotation * new Vector3(0.0f, 0.0f, -1.0f));
			}
			if(Input.GetKey(KeyCode.D))
			{
				position += (rotation * new Vector3(1.0f, 0.0f, 0));
			}
			if(Input.GetKey(KeyCode.A))
			{
				position += (rotation * new Vector3(-1.0f, 0.0f, 0));
			}
			transform.rotation = rotation;
			transform.position = position;
		}

	}
	public float ClampAngle (float angle ,float min,  float max) {
		if (angle < -360)
			angle += 360;
		if (angle > 360)
			angle -= 360;
		return Mathf.Clamp (angle, min, max);
	}
}



