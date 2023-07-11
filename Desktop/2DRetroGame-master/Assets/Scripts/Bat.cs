using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{

	public GameManager gm;
    public LivesManager lm;

    private Rigidbody2D rb;

    private Animator theAnim;

    public static float health = .5f;

    public AudioSource playSound;

    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.gameObject.tag == "Player")
    	{
            playSound.Play();
            gm.Reset();
    		lm.TakeLife();
    		Debug.Log(health);
        
        }
    }

    
}
