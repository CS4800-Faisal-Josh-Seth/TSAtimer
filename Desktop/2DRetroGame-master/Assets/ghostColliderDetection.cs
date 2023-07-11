using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostColliderDetection : MonoBehaviour
{
    public CircleCollider2D circleCollider;
    public Animator anim;
    public LayerMask attackMask;
    public float attackRange = .7f;
    public PlayerController pc;

    //for sounds
    public AudioSource se;
    public AudioClip clip;

    public void Start()
    {

        pc = GameObject.FindObjectOfType<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the circle collider attached to the ghost / gameobject then we follow them
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("follow");
        }
    }


    public void playSound()
    {
            //this plays the sound once the update into ghost gets hit is played
            if ((clip) != null && se != null)
            {
                //once hit this sound will play
                se.PlayOneShot(clip);
            }
     }

     
    public void attack()
    {
        Vector2 pos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);

        // Visualize the overlap circle
        Debug.DrawRay(pos, Vector2.up * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.right * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.left * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.down * attackRange, Color.green);

        // Check if any of the colliders are the player's collider
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
              
                ScoreManager.instance.TakeScore(1);
                pc.StartCoroutine(pc.flashSprite());
                break;
            }
        }
    }




}

