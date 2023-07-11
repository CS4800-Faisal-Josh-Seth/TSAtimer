using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockFall : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject rock;
    public Vector3 spawnPosition;
    public AudioSource ass;

   void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll; // lock the rigidbody in place
        spawnPosition = rock.transform.position;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(respawn());
            
        }
    }

    IEnumerator respawn()
    {
        //Wait for 2 seconds, then rock falls
    	yield return new WaitForSeconds(1f);
        ass.Play();
        rb.constraints = RigidbodyConstraints2D.None; // release the rigidbody constraints
        rb.AddForce(Vector2.down * 1.2f, ForceMode2D.Impulse); // add downward force to simulate dropping
        yield return new WaitForSeconds(3f);
        Instantiate(rock, spawnPosition, Quaternion.identity);
    	Destroy(gameObject);
    }

    void Spawn()
    {
    	Instantiate(rock, spawnPosition, Quaternion.identity);	
    }
}
