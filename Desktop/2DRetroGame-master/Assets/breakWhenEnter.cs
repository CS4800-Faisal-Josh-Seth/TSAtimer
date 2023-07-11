using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakWhenEnter : MonoBehaviour
{

    public Animator breakingAnimation;
    public AudioSource se;
    // Start is called before the first frame update
    void Start()
    {
        breakingAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Player")
        {
            StartCoroutine(breakThatShit());
            breakingAnimation.SetTrigger("break");
            se.Play();
        }
    }

    IEnumerator breakThatShit()
    {
        yield return new WaitForSeconds(.8f);
        Destroy(gameObject);
    }
}
