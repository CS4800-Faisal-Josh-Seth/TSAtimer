using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

	public string levelToLoad;

	public int defaultLives;
	public int defaultScore;

	private void Start() 
	{
			PlayerPrefs.SetInt("CurrentLives", defaultLives);
			PlayerPrefs.SetInt("CurrentScore", defaultScore);
	}
	public void playGame()
	{
		SceneManager.LoadScene(levelToLoad);
	}

	public void QuitGame()
	{
		Application.Quit(); 
	}
}
