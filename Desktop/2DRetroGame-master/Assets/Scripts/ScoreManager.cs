using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

	public static ScoreManager instance;
	public Text scoreText;

    private GameManager theGM;

    public bool canChange = true;

    public GameObject soulSlot1;
    public GameObject soulSlot2;
    public GameObject soulSlot3;
    public GameObject soulSlot4;
    public GameObject soulSlot5;

	private Color defaultColor;
	private Renderer g1;
	private Renderer g2;
	private Renderer g3;
	private Renderer g4;
	private Renderer g5;

	// Start is called before the first frame update
	void Start()
    {
		//get the current color of the soulSlots
		g1 = soulSlot1.GetComponent<Renderer>();
		g2 = soulSlot2.GetComponent<Renderer>();
		g3 = soulSlot3.GetComponent<Renderer>();
		g4 = soulSlot4.GetComponent<Renderer>();
		g5 = soulSlot5.GetComponent<Renderer>();
		defaultColor = g1.material.color;

		if (instance == null)
        {
        	instance = this;
        }
        Weapon.shotsLeft = PlayerPrefs.GetInt("CurrentScore");

        theGM = FindObjectOfType<GameManager>();
    }


    
    public void ChangeScore(int soulValue)
    {
	    if (Weapon.shotsLeft >= 5)
	    {
		    canChange = false;
		    Weapon.shotsLeft = 5; //tie back to five total in case of overflow
	    }
	    
	    if (canChange)
	    {
		    Weapon.shotsLeft += soulValue;
	    }

	    PlayerPrefs.SetInt("CurrentScore", Weapon.shotsLeft);
    	scoreText.text = "" + Weapon.shotsLeft.ToString();
        
    }

	public void TakeScore(int soulReduction)
	{
		if (Weapon.shotsLeft <= 0)
		{
			LivesManager.instance.TakeLife();
			Weapon.shotsLeft = 0;
		}
		else
		{
			Weapon.shotsLeft -= soulReduction;
		}
		PlayerPrefs.SetInt("CurrentScore", Weapon.shotsLeft);
		scoreText.text = Weapon.shotsLeft.ToString();
		canChange = true;
	}


	public void Update()
    {
		if (greenPotion.active)
        {
			g1.material.color = Color.green;
			g2.material.color = Color.green;
			g3.material.color = Color.green;
			g4.material.color = Color.green;
			g5.material.color = Color.green;
		} else
		{
			//set all of the colors to strictly default
			g1.material.color = defaultColor;
			g2.material.color = defaultColor;
			g3.material.color = defaultColor;
			g4.material.color = defaultColor;
			g5.material.color = defaultColor;

		}
	    switch (Weapon.shotsLeft)
	    {
		    case 0:
			    soulSlot1.SetActive(false);
			    soulSlot2.SetActive(false);
			    soulSlot3.SetActive(false);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 1:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(false);
			    soulSlot3.SetActive(false);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 2:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(false);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 3:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(true);
			    soulSlot4.SetActive(false);
			    soulSlot5.SetActive(false);
			    break;
		    case 4:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(true);
			    soulSlot4.SetActive(true);
			    soulSlot5.SetActive(false);
			    break;
		    case 5:
			    soulSlot1.SetActive(true);
			    soulSlot2.SetActive(true);
			    soulSlot3.SetActive(true);
			    soulSlot4.SetActive(true);
			    soulSlot5.SetActive(true);
			    
			    break;
	    }
    }
    
}
