using UnityEngine;

public class Slide : MonoBehaviour
{
    public Rigidbody2D rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    	if(collision.gameObject.tag == "Wall")
    	{
    		rigidbody.gravityScale = 0.5f;
    	}
        
    }
}