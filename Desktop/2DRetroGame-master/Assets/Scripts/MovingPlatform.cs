using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 5f;
    
    private bool movingUp = true;
    private bool canMove = true;
    public bool isOnBackground;
    public BoxCollider2D col;
    public AudioSource ad;

    public void Start()
    {
      col = GetComponent<BoxCollider2D>();

        if (isOnBackground)
        {
            col.enabled = false;
        }

        ad.enabled = true;
    }

    private void Update()
    {
        float currentY = transform.position.y;

        //if canMove then we see if we need to go up or down
        if (canMove)
        {
            if (movingUp)
            {
                transform.position = new Vector2(transform.position.x, currentY + speed * Time.deltaTime);
            }
            else if (!movingUp)
            {
                transform.position = new Vector2(transform.position.x, currentY - speed * Time.deltaTime);
            }


            if (transform.position.y >= maxY)
            {
                StartCoroutine(PauseUp());

            }
            else if (transform.position.y <= minY)
            {
                StartCoroutine(PauseDown());

            }
        }
    }

    IEnumerator PauseUp()
    {
        canMove = false;
        ad.enabled = false;
        //freeze the gameobject's position for 1.3 seconds
        transform.position = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(1.3f);
        //then send down
        canMove = true;
        movingUp = false;
        ad.enabled = true;

    }

    IEnumerator PauseDown()
    {
        ad.enabled = false;
        canMove = false;
        //freeze the gameobject's position for 1.3 seconds
        transform.position = new Vector2(transform.position.x, transform.position.y);
        yield return new WaitForSeconds(1.3f);
        //then send up
        canMove = true;
        movingUp = true;
        ad.enabled = true;
    }



}

