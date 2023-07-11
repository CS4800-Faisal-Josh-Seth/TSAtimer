using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private Animator theAnimator;
    public Transform firePoint;
    public GameObject bulletPrefab;

    public bool shoot;

    public AudioSource audioSource;
    public AudioClip shootingAudioClip;
    
    public static int shotsLeft; //going to be amount tied to soul count and initially 3 at beginning of game

    public static int maxShots = 5; 

    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    	
    	theAnimator = GetComponent<Animator>();
        if (Input.GetKeyDown(KeyCode.Space) && shotsLeft > 0)
        {
	        ScoreManager.instance.TakeScore(1);
	        StartCoroutine(Shoot());
        	 //calls the animation with given boolean
        } 
          //if not shooting, false. 
        
      
    }

    IEnumerator Shoot () ///bulletlogic
    {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // SoundManager.PlaySound("playerShoot");
            audioSource.PlayOneShot(shootingAudioClip);
            theAnimator.SetBool("Shoot", true);
            yield return new WaitForSeconds(.7f);
            theAnimator.SetBool("Shoot", false);
       
    }
}
