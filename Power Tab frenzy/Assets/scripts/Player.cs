using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

	private float inputDirection;			// X value of our movevector
	private float verticalVeclocity; 		// Y value of our movevector
	private float inputSprint;

	private float speed = 5.0f;
	private float gravity = 30.0f;
	private float jumpforce = 10.0f;
	private bool secondJumpAvail = false;
	private bool sprintAvail = false;

	private Vector3 moveVector;
	private Vector3 lastMotion;
	private CharacterController controller;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController> ();

	}
	
	// Update is called once per frame
	void Update () {
		IsControllerGrounded ();
		moveVector = Vector3.zero;
		inputDirection = Input.GetAxis("Horizontal") * speed;




		if (IsControllerGrounded()) 
		{
			verticalVeclocity = 0;
			inputSprint = 10.0f;
				
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				// Make the player jump
				verticalVeclocity = jumpforce;
				secondJumpAvail = true;
			}

			if (Input.GetKeyDown (KeyCode.LeftShift)) 
			{
				// Make the player sprint
				speed = inputSprint;
				sprintAvail = true;
			
			}

			if (Input.GetKeyUp (KeyCode.LeftShift)) 
			{
				// Make the player walk
				speed = 5.0f;
				sprintAvail = false;
			}

			moveVector.x = inputDirection;

		}



		else
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				if (secondJumpAvail) 
				{
					verticalVeclocity = jumpforce;
					secondJumpAvail = false;
				}
			}

			verticalVeclocity -= gravity * Time.deltaTime;
			moveVector.x = lastMotion.x;
		}
			
		moveVector.y = verticalVeclocity;

		controller.Move (moveVector * Time.deltaTime);
		lastMotion = moveVector;
	}

	private bool IsControllerGrounded()
	{
		Vector3 leftRayStart;
		Vector3 rightRayStart;

		leftRayStart = controller.bounds.center;
		rightRayStart = controller.bounds.center;

		leftRayStart.x -= controller.bounds.extents.x;
		rightRayStart.x += controller.bounds.extents.x;
			
		Debug.DrawRay (leftRayStart,Vector3.down,Color.red);
		Debug.DrawRay (rightRayStart,Vector3.down,Color.green);

		if(Physics.Raycast(leftRayStart, Vector3.down,(controller.height/2) + 0.1f))
			return true;

		if(Physics.Raycast(rightRayStart, Vector3.down,(controller.height/2) + 0.1f))
			return true;


		return false;
	}


	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (controller.collisionFlags == CollisionFlags.Sides) 
		{
			if (Input.GetKeyDown (KeyCode.Space)) 
			{
				moveVector = hit.normal * speed;
				verticalVeclocity = jumpforce;
				secondJumpAvail = true;
			}
		}
		// Collectables
		switch (hit.gameObject.tag) 
		{
		case "coin":
			LevelManager.Instance.CollectCoin();
			Destroy (hit.gameObject);
			break;
		case "Jumppad":
			verticalVeclocity = jumpforce * 2;
			inputDirection = speed * 2;
			break;
		case "Teleport":
			transform.position = hit.transform.GetChild(0).position;
			break;
		case "Winbox": 
			LevelManager.Instance.Win ();
			break;
		case "MenuChanger":
			Application.LoadLevel("world1"); 
			break;
		case "ArcadeChanger":
			Application.LoadLevel("Arcade"); 
			break;
		case "ArcadeExit":
			Application.LoadLevel("Main_menu"); 
			break;
		case "TutorialChanger":
			Application.LoadLevel("Tutorial"); 
			break;
		case "TutorialExit":
			Application.LoadLevel("Main_menu"); 
			break;
		case "EndGame":
			Application.LoadLevel("EndGame"); 
			break;
		default:
			break;
		
			

		}
	}

}





