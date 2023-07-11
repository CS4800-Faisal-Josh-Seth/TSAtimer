using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialStarter : MonoBehaviour
{
    public GameObject initialMessage;
    public int counter;

    private bool inputProcessed = false;

    public void Start()
    {
        //first message turns on
        initialMessage.SetActive(true);

        //initialize counter to 0
        counter = 0;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.P) && !inputProcessed)
        {
            counter++;
            inputProcessed = true;
        }

        switch (counter)
        {
            case 1:
                Debug.Log("Case 1");
                break;
            case 2:
                Debug.Log("Case 2");
                break;
            case 3:
                Debug.Log("Case 3");
                break;
            default:
                Debug.Log("Default case");
                break;
        }
    }

    private void LateUpdate()
    {
        // Reset the inputProcessed flag at the end of each frame
        inputProcessed = false;
    }
}
