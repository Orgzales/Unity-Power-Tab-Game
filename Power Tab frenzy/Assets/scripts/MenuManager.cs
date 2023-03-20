using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public Text scoreText;
	private int score; 

	public void toGame()
	{
		SceneManager.LoadScene ("world1");
	}

	private void Start()
	{
		score = PlayerPrefs.GetInt ("PlayerScore");
		scoreText.text = "Highscore : " + score.ToString (); 
	}


}
