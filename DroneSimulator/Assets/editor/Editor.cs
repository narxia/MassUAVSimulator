using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class Editor : EditorWindow {

	[MenuItem("Drone/Setting")]
	static void showWindow()
	{
		EditorWindow.GetWindow<Editor> ();	
	}
	void OnGui()
	{
		GUI.Button (new Rect (0, 0, 100, 50), "Button");
	}

}
