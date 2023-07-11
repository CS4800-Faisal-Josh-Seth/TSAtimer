using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPlayer : MonoBehaviour
{
    //this script uses a rigidbody2D to constantly follow towards the main player.


    public GameObject player;
    public Rigidbody2D eyeballRb;
    public float speed = 3f;

    private void Start()
    {
        //find player in scene (should only be one)
        player = GameObject.FindGameObjectWithTag("Player");

        //Get the rigidbody of the gameobject this script is attached to
        eyeballRb = GetComponent<Rigidbody2D>();

    }
    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - eyeballRb.transform.position).normalized;
        eyeballRb.MovePosition(eyeballRb.position + direction * speed * Time.deltaTime);
    }
}
