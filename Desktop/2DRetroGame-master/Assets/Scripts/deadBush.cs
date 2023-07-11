using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadBush : MonoBehaviour
{

	public Animator rustleBush;

	private void Start()
	{
		rustleBush = GetComponent<Animator>();
	}
	private void OnTriggerEnter2D(Collider2D col)
    {
    	if(col.gameObject.tag == "Player")
    	{
    		rustleBush.SetBool("Rustle", true);
    	}

    	if(col.gameObject.tag == "bullet")
    	{
    		Destroy(gameObject);
    	}
    }

    private void OnTriggerExit2D(Collider2D col)
    {
    	rustleBush.SetBool("Rustle", false);
    }

}
