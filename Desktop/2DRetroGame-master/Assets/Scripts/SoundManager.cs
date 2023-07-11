using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundManager : MonoBehaviour
{

	public static AudioClip playerShoot, playerShootImpact;
	static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        playerShoot = Resources.Load<AudioClip>("/Assets/Sounds/playerShoot");
        playerShootImpact = Resources.Load<AudioClip>("/Assets/Sounds/playerShootImpact");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound( string clip )
    {
    	switch(clip){
    		case "playerShoot":
    			audioSrc.PlayOneShot(playerShoot);
    			break;
    		case "playerShootImpact":
    			audioSrc.PlayOneShot(playerShootImpact);
    			break;
    	}
    }
}
