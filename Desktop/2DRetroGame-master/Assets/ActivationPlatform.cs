using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationPlatform : MonoBehaviour
{ 
    public bool complete;
    private float collisionTime;
    public float interval = 1.5f;
    private float timer = 0f;

    //GOs
    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;
    public GameObject six;
    public GameObject seven;
    public GameObject eight;
    public GameObject object1;
    public GameObject object2;
    public GameObject object3;
    public GameObject object4;

    //sounds
    public AudioSource se;
    public AudioClip stackSound;
    public AudioClip initialZap;
    public AudioClip continousZap;

    // Start is called before the first frame update
    void Start()
    {
        if ((object1 != null) || (object2 != null) || (object3 != null) || (object4 != null))
        {
            one.SetActive(false);
            two.SetActive(false);
            three.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            six.SetActive(false);
            seven.SetActive(false);
            eight.SetActive(false);
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(false);
            object4.SetActive(false);
        }
    }

    public void Update()
    {
        //constantly gets currentTime and ahead time to store instance in bottom and top
        timer += Time.deltaTime;

        if ( complete )
        {

            if (timer >= interval)
            {
                se.PlayOneShot(continousZap);
                timer = 0f;  // Reset the timer
            }

        }
    }


    //purely just to get the start time of when the enter enters the collider
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionTime = Time.time;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //activate only if the player is colliding with the hitbox and null checker
        if (collision.gameObject.tag == "Player" && stackSound != null)
        {
            if (Time.time - collisionTime == .5f)
            {
                se.PlayOneShot(stackSound);
                one.SetActive(true);
            }
            if (Time.time - collisionTime == 1f)
            {
                se.PlayOneShot(stackSound);
                two.SetActive(true);
            }
            if (Time.time - collisionTime == 1.5f)
            {
                se.PlayOneShot(stackSound);
                three.SetActive(true);
            }
            if (Time.time - collisionTime == 2f)
            {
                se.PlayOneShot(stackSound);
                four.SetActive(true);
            }
            if (Time.time - collisionTime == 2.5f)
            {
                se.PlayOneShot(stackSound);
                five.SetActive(true);
            }
            if (Time.time - collisionTime == 3f)
            {
                se.PlayOneShot(stackSound);
                six.SetActive(true);
            }
            if (Time.time - collisionTime == 3.5f)
            {
                se.PlayOneShot(stackSound);
                seven.SetActive(true);
            }
            if (Time.time - collisionTime == 4f)
            {
                se.PlayOneShot(stackSound);
                eight.SetActive(true);

                //play initial zap on thecomplettion of the last homie
                se.PlayOneShot(initialZap);

                complete = true;

                if (( object1  != null) || (object2 != null) || (object3 != null) || (object4 != null))
                {
               
                    StartCoroutine(enableGameObjects());
                }
                
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       if (!complete)
        {
            one.SetActive(false);
            two.SetActive(false);
            three.SetActive(false);
            four.SetActive(false);
            five.SetActive(false);
            six.SetActive(false);
            seven.SetActive(false);
            eight.SetActive(false);
        }
    }



    IEnumerator enableGameObjects()
    {
        yield return new WaitForSeconds(.3f);
        object1.SetActive(true);
        yield return new WaitForSeconds(.3f);
        object2.SetActive(true);
        yield return new WaitForSeconds(.3f);
        object3.SetActive(true);
        yield return new WaitForSeconds(.3f);
        object4.SetActive(true);
    }
}
