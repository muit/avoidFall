﻿using UnityEngine;
using System.Collections;

public class ThirdPersonCameraRotation : MonoBehaviour {
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;
	
	public float paddingLeft   = 0;
	public float paddingRight  = 0;
	public float paddingTop    = 0;
	public float paddingBottom = 0;
	
	public float minimumX = -360F;
	public float maximumX = 360F;
	
	public float minimumY = -60F;
	public float maximumY = 60F;

	bool enabled = true;	
	float rotationY = 0F;
	
	void Update()
	{
		if(Input.GetKeyUp("escape")){
			enabled = !enabled;
			Screen.showCursor = !enabled;
		}

		//Input.GetKey("escape")
		if (!enabled ||
		    Input.mousePosition.x < paddingLeft || Input.mousePosition.x > Screen.width - paddingRight ||
		    Input.mousePosition.y < paddingTop  || Input.mousePosition.y > Screen.width - paddingBottom)
		{
			return;
		}
		
		if (axes == RotationAxes.MouseXAndY)
		{
			float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
			
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
		}
		else if (axes == RotationAxes.MouseX)
		{
			transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
		}
		else
		{
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
			
			transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
		}
	}
	
	void Start()
	{
		Screen.showCursor = !enabled;
		if (rigidbody) rigidbody.freezeRotation = true;
	}
}
