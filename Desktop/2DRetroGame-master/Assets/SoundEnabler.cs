using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEnabler : MonoBehaviour
{

    public AudioSource song;

    public void Start()
    {
        //initialize to false so it doens't sound weird at the start
        song.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            song.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            song.enabled = false;
        }
    }
}
