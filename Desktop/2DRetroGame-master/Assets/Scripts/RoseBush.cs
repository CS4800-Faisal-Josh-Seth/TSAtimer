using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseBush : MonoBehaviour
{

	    public GameManager gm;
    	public LivesManager lm;
        public Animator am;

    public void Start()
    {
        //get the animator component
        am = GetComponent<Animator>();

        if (am != null)
        {
            //start the coroutine
            StartCoroutine(spawnEm());
        }
    }
       public void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.gameObject.tag == "Player")
    	{
           
            gm.Reset();
    		lm.TakeLife();
        
        }
    }

    public IEnumerator spawnEm()
    {
        am.enabled = false;
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        am.enabled = true;
    }
}
