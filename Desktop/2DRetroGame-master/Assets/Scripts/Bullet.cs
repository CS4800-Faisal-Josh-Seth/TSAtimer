using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject theBullet;
    public static float speed = 1.5f;
    public Rigidbody2D rb;
    public float damage = 3f;
    public GameObject impactEffect;

    public GameObject player; //for the player's info

    public AudioSource playSound;

    private float scaleDecreaseRate = 0.05f; // rate at which to decrease the scale of the game object

    public static float timeDuration = 4;

    public static SpriteRenderer ren;
    public static Color startingColor;

    public static bool canHold;


    // Start is called before the first frame update
    void Start()
    {
        // gets the start color so I can make changes to the color and revert back
        ren = GetComponent<SpriteRenderer>();

        //this conditional checks if the green potion is on. If it is then this potion will stay green throughout its entire life 
        if ( greenPotion.active)
        {
            startingColor = Color.green;
        } else
        {
            startingColor = ren.color;
        }

        //make canHold true
        canHold = true;

        //because we want this bullet to have a lifetime we create this coroutine to kill it after specified time
        StartCoroutine(timeStop());
    }

    void Update()
    {
        Debug.Log("FREEZE? " + PlayerController.freezeOn);
        Debug.Log("released " + PlayerController.released);

        if (PlayerController.freezeOn && !PlayerController.released)
        {
            //action 1 to happen
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;

        }
        if (!PlayerController.freezeOn && PlayerController.released)
        {
            //action 2 to happen
            //moves the rigidbody of the bullet down by 4 times the speed
            rb.velocity = -transform.up * speed + new Vector3(0, -4, 0);

            //put proper constraints on for downwards motion
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

            //flip the released variable back to false
            StartCoroutine(deRelease());
        }
        if (!PlayerController.freezeOn && !PlayerController.released)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;


            //if neither action 1 or 2, do this last resort action if P isnt clicked at all
            rb.isKinematic = false;

            //this is what makes it move
            rb.velocity = transform.right * speed;

            if (player != null)
            {
                if (PlayerController.FacingRight == true)
                {
                    player.transform.position += new Vector3(1.5f * Time.deltaTime, 0, 0);
                }
                else if (PlayerController.FacingRight == false)
                {
                    player.transform.position -= new Vector3(1.5f * Time.deltaTime, 0, 0);
                }
            }

            // decrease the X scale of the game object
            transform.localScale -= new Vector3(scaleDecreaseRate * Time.deltaTime, 0f, 0f);


            if (greenPotion.active == true)
            {
                //change the color of the soul shot
                Bullet.ren.color = Color.green;
            }
            else
            {
                Bullet.ren.color = startingColor;
            }
        }

            

    }

    public void OnTriggerEnter2D(Collider2D hitInfo)  //when anything happens when bullet hits
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            //destroy the gameobject and instantiate impactEffect if it hits the enemy
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }


        playSound.Play();
    }


    public void OnCollisionEnter2D(Collision2D hitInfo)
    {
        Enemy enemy = hitInfo.collider.GetComponent<Enemy>();

        if (hitInfo.gameObject.tag == "Player")
        {
            Debug.Log("Connected Player");
            player = hitInfo.gameObject;
        } else if ( hitInfo.gameObject.tag == "Enemy")
        {
            enemy.TakeDamage(damage);
            //we don't want repetative damage on this
            Destroy(gameObject);
        } else if ( hitInfo.gameObject.tag != "Enemy" && hitInfo.gameObject.tag != "Player")
        {
            //destroy the gameobject if it hits anything with a solid collider
            Destroy(gameObject);
        }


        Instantiate(impactEffect, transform.position, transform.rotation);


    }


    //don't know why this is needed
    public void OnCollisionExit2D(Collision2D coal)
    {
        if (coal.gameObject.tag == "Player")
        {
            player = null;
        }
    }

    //time until the bullet stops
    IEnumerator timeStop()
    {
        yield return new WaitForSeconds(timeDuration);

        //Make the cool effect just in case
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator deRelease()
    { 
        //however long it takes them to leave the screen
        yield return new WaitForSeconds(1f);

        //after all is done make canHold back to False
        canHold = false;

        //change the rigidbody constraints back
        //hold the y but not the x
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;

        PlayerController.released = false;

        //this is so that after when you drop the soul shot it destroys before going back to normal
        Destroy(gameObject);
    }
}
