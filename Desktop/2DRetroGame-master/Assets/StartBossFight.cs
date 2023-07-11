using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFight : MonoBehaviour
{

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        if (boss != null)
        {
            boss.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //this will start the boss fight
        boss.SetActive(true);
    }
}
