using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public static LevelManager Instance{set;get;}

	private int hitpoint = 3;
	private int score = 0;

	public Transform spawnPosition;
	public Transform playerTransform;

	public Text scoreText;
	public Text hitpointText;

	// private void start similare
	private void Awake()
	{
		Instance = this;
		scoreText.text = "Score : " + score.ToString();
		hitpointText.text = "Lifes : " + hitpoint.ToString();
	}

	// Every FPS
	private void Update()
	{
		if (playerTransform.position.y < -25) 
		{
			playerTransform.position = spawnPosition.position;
			hitpoint--; //hp = hp -1
			hitpointText.text = "Lifes : " + hitpoint.ToString();
			if (hitpoint <= 0)
			{
				Application.LoadLevel("GameOver"); //replace with game over screen
				PlayerPrefs.SetInt ("PlayerScore", score); 
			}
		}
			
	}

	public void Win()
	{
		PlayerPrefs.SetInt ("PlayerScore", score); 
		Application.LoadLevel("Main_menu"); // change to finish credits
	}

	public void CollectCoin()
	{
		score++;
		scoreText.text = "Score : " + score.ToString ();
	}

}
