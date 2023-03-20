using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour {

	public Transform lookAt;

	private bool smooth = true;
	private float smoothSpeed = 0.125f;
	private Vector3 offset = new Vector3 (0.2f, 0.5f, -10f);

	private void LateUpdate()
	{
		Vector3 desiredPosition = lookAt.transform.position + offset;

		if (smooth) 
		{
			transform.position = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
		} 
		else 
		{
			transform.position = desiredPosition;
		}


	}


}
