using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{

	public int soulValue;

	public AudioSource playSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.tag == "Player")
        {
    		Destroy(gameObject);
    		playSound.Play();
    		ScoreManager.instance.ChangeScore(soulValue);
    	}
    }
}
