using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumkin : MonoBehaviour
{
	public GameManager gm;
    public GameObject victoryScreen;


    public void OnTriggerStay2D(Collider2D other)
    {
    	if (other.gameObject.tag == "Player")
    	{
    		Debug.Log("EPIC W");
            victoryScreen.SetActive(true);
        }

            
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        victoryScreen.SetActive(false);
    }
}
