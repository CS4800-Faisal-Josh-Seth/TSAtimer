using UnityEngine;

public class MovingPlatformX : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 5f;
    
    public bool movingRight = true;
    public GameObject player;
    public void Update()
    {
        float currentX = transform.position.x;

        if (movingRight)
        {
            transform.position = new Vector2(currentX + speed * Time.deltaTime, transform.position.y);
            if(player != null)
            {
                player.transform.position += new Vector3(speed * Time.deltaTime,0,0);
            }
        }
        else
        {
            transform.position = new Vector2(currentX - speed * Time.deltaTime, transform.position.y);
            if(player != null)
            {
                player.transform.position -= new Vector3(speed * Time.deltaTime,0,0);
            }
        }

        if (transform.position.x >= maxX)
        {
            movingRight = false;
        }
        else if (transform.position.x <= minX)
        {
            movingRight = true;
        }
    }

    public void OnCollisionEnter2D(Collision2D coal){
        if(coal.gameObject.tag == "Player")
        {
            player = coal.gameObject;
        }
    }
    public void OnCollisionExit2D(Collision2D coal){
        if(coal.gameObject.tag == "Player")
        {
            player = null;
        }
    }

}

