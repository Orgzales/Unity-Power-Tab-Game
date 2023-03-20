using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

	public Transform lookAt;

	private Vector3 offset = new Vector3(0, 0.5f, -10f);

	private void Start()
	{
		
	}

	private void LateUpdate()
	{
		transform.position = lookAt.transform.position + offset;
	}

}
