using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesManager : MonoBehaviour
{

	//public int defaultLives;
	public int livesCounter;

	public Text livesText;

	private GameManager theGM;

    public int soulReduction = 0;

    public static LivesManager instance;
    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        //livesCounter = defaultLives;
        livesCounter = PlayerPrefs.GetInt("CurrentLives");

        theGM = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = " " + livesCounter;

        if (livesCounter < 1)
        {
        	theGM.GameOver();
        }
    }

    public void TakeLife()
    {
        if (PlayerController.isInvincible == false)
        {
            livesCounter--;
            PlayerPrefs.SetInt("CurrentLives", livesCounter);
            if (Weapon.shotsLeft > 0)
            {
                ScoreManager.instance.TakeScore(soulReduction);
            }
        }
    }

    public void AddLife()
    {
    	livesCounter++;
    	PlayerPrefs.SetInt("CurrentLives", livesCounter);
    }
}
