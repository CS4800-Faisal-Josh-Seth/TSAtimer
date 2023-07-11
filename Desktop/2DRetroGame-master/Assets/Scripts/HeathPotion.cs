using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathPotion : MonoBehaviour
{

	public AudioSource playSound;
    // Start is called before the first frame update
    private void OnTriggerEnter2D( Collider2D collision)
     {

     	if ( collision.tag == "Player")
     	{
     		Destroy(gameObject);
     		playSound.Play();
     		LivesManager.instance.AddLife();

     	}
     }
}
