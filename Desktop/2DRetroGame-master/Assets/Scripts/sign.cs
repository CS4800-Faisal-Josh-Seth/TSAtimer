using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sign : MonoBehaviour
{
	public GameObject message;
    // Start is called before the first frame update

    public void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.gameObject.tag == "Player")
    	{
           
            Debug.Log("Message Shown");
        	message.SetActive(true);

    	}
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        message.SetActive(false);
    }

}


