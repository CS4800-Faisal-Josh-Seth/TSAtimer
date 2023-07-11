using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string mainMenu;

	public PlayerController thePlayer;
	public Pumkin thePumpkin;
    private Vector2 playerStart;

	public GameObject victoryScreen;
	public GameObject gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerStart = thePlayer.transform.position;
    }


    /*
    public void Victory() 
    {
    		victoryScreen.SetActive(true);
    		thePlayer.gameObject.SetActive(false);
            thePumpkin.gameObject.SetActive(false);
            
    }
    */

    public void GameOver() 
    {

    		gameOverScreen.SetActive(true);
    		thePlayer.gameObject.SetActive(false);
            StartCoroutine("GameReset");

    }

    IEnumerator GameReset()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(mainMenu);
        thePlayer.gameObject.SetActive(true);
        thePumpkin.gameObject.SetActive(true);
        PlayerController.canMove = true;
    }

    public void Reset() 
    {
    		victoryScreen.SetActive(false);
    		gameOverScreen.SetActive(false);
    		thePlayer.gameObject.SetActive(false);

            //If the player is hit or runs into anything there will be a delay before they respawn
            StartCoroutine(respawnDelay());
    }


    IEnumerator respawnDelay()
    {

        yield return new WaitForSeconds(2f);
        thePlayer.transform.position = playerStart;
        thePlayer.gameObject.SetActive(true);
    }
    
    }
