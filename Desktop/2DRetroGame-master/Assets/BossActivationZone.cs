using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivationZone : MonoBehaviour
{

    public GameObject gate;
    public bool hasActivated = false;
    public Collider2D gateCollider;

    public float whereToStopGateOnY;

    public bool moving = false;

    //Audio
    public AudioSource gateOpen;
    public AudioSource gateConstant;
    public AudioSource gateStop;
    public float initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        Collider2D gateCollider = GetComponent<Collider2D>();
        gateConstant.enabled = false;
        gateStop.enabled = false;

        //To flip the switch were just going to flip the sprite 180 degrees
        Transform transform = gameObject.GetComponent<Transform>();

        initialPosition = gate.transform.position.y;
    }

    // Update is called once per frame
    private void Update()
    {
        float currentY = gate.transform.position.y; //gives a current location of y value of gate to the code to know 
                                                    //when to stop it from moving and to transform its position.
        if (moving)  //if moving is true we allow the gate's positing to move in an upwards manner, updating the position .5 units once per second
        {
            //simulates a slow upwards movement of the gate
            gate.transform.position = new Vector2(gate.transform.position.x, currentY - .7f * Time.deltaTime);
            
        }
        //if gate's current position is higher or equal to the intended serialized height, we stop its movement
        if (gate.transform.position.y <= initialPosition - 1.76f)
        {
            moving = false;

        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !hasActivated)
        {
            gateOpen.Play();
            //start moving the platform
            moving = true;
            //only one activation allowed
            hasActivated = true;
            //turn one the gate's collider
            gateCollider.enabled = true;
            StartCoroutine(StopGateSounds());
        
        }
    }

    IEnumerator StopGateSounds()
    {
        //turn on the sound
        gateConstant.enabled = true;
        yield return new WaitForSeconds(2f);
        gateStop.enabled = true;
        yield return new WaitForSeconds(.5f); //disable the gate close sound effect after to avoid constant sound playing
        gateConstant.enabled = false;
        gateStop.enabled = false;
    }


}

