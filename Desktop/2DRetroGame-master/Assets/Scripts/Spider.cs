using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{

    private float dirY;
    private float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 localScale;

    private bool FacingDown = true;

    private Animator theAnim;
    private bool isDead;

    public int soulValue = 1; 

    public static float health = .5f;

    public GameObject deathEffect;

    public AudioSource playSound;

    public GameObject healthBar;
    
    public float knockbackDuration = 0.1f;
    public float knockbackForce = 10f;
    
    [SerializeField]
    public bool isOnBackground;

    //Can make changeable through inspector tab later but for now 5
    public int TopBGSpiderLenght = 5;
    public int BottomBGSpiderLength = -5;

    public static bool useForBackground;

        void Start()
    {

        //theAnim = transform.parent.GetComponent<Animator>();
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        dirY = 1f;
        moveSpeed = 1f;

        
    }

        public void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag == "Player" && !isOnBackground)
            {
                useForBackground = true;
                playSound.Play();
             
                ScoreManager.instance.TakeScore(1);
                
                
            }

            useForBackground = false;

            if (col.GetComponent<Wall>())
            {
                dirY *= -1f;
                Flip();
            }
        }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
        if (isOnBackground)
        {
            if (transform.position.y >= TopBGSpiderLenght)
            {
                Flip();
                dirY = -1f;
            }
            else if (transform.position.y <= BottomBGSpiderLength)
            {
                Flip();
                dirY = 1f;
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, dirY * moveSpeed);
            }
        }
    }

    private void Flip()
    {

        FacingDown = !FacingDown;

        transform.Rotate(180f, 0f, 0f);
    }
    

    

    
}
