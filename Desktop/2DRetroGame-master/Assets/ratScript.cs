using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ratScript : MonoBehaviour
{
	public Rigidbody2D rb;
	[SerializeField] private float speed = 1f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 5f;
    
    public bool movingRight = true;
    public void Update()
    {
        float currentX = transform.position.x;

        if (movingRight)
        {
            rb.transform.position = new Vector2(currentX + speed * Time.deltaTime, transform.position.y);
            StartCoroutine(stopMoving());
        }
        else
        {
            transform.position = new Vector2(currentX - speed * Time.deltaTime, transform.position.y);
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

    public void OnTriggerEnter2D(Collider2D col)
    {
    	if (col.gameObject.tag == "Player")
    	{
    		//do the running away
    		Debug.Log("run away rat!");
    	}
    }


    IEnumerator stopMoving()
    {
    	Debug.Log("should stop moving");
    	transform.position = new Vector3(0,0,0);
    	yield return new WaitForSeconds(2f);
    	
    }



}