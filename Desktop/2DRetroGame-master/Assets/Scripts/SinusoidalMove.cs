using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalMove : MonoBehaviour
{

	[SerializeField]
	float moveSpeed = 5f;

	[SerializeField]
	float frequency = 20f;

	[SerializeField]
	float magnitude = 0.5f;

	bool facingRight = true;

	[SerializeField]
	float leftBound = 0f;

	[SerializeField]
	float rightBound = 0f;

	Vector3 pos, localScale;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;

        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {

    	CheckWhereToFace();

    	if(facingRight)
    	{
    		MoveRight();
    	} else
    	{
    		Debug.Log("turning left");
    		//Flip();
    		MoveLeft();
    	} 
    	
        
    }

    void CheckWhereToFace()
    {
    	if(pos.x < leftBound)
    	{
    		facingRight = true; 
    	} else if (pos.x > rightBound)
    	{
    		facingRight = false; 
    	}

    	if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
    	{
    		
    		localScale.x *= -1;
    		

    	}

    	transform.localScale = localScale;
    	
    }

    void MoveRight()
    {
    	pos += transform.right * Time.deltaTime * moveSpeed;
    	transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    }

    void MoveLeft()
    {
    	pos -= transform.right * Time.deltaTime * moveSpeed;
    	transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
    	
    }

}
