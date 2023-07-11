using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBoss : MonoBehaviour
{
    private Enemy bossInstance;

    public float timeBetweenSpawn = 20f;
    public GameObject eyeballPrefab;
    public Transform spawnPosition1;
    public Transform spawnPosition2;
    public Transform spawnPosition3;

    public bool phase2Enabled;

    private float timer = 0f;
    private bool spawnEyesTriggered = false;

    public Animator bossAnimator;

    public GameObject healthBar;

    public GameObject itemsToDestroy;

    public LayerMask attackMask;
    public float attackRange = .8f;

    public PlayerController pc;

    public GameObject leverForDoor;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindObjectOfType<PlayerController>();

        //So I dont have to place the script in the inspector tab, yes I'm that lazy
        bossInstance = GetComponent<Enemy>();

        bossAnimator = GetComponent<Animator>();

        phase2Enabled = false;

        bossAnimator.SetTrigger("Idle");

        //At the start of the fight I want eyes to spawn
        SpawnEye();

     
    }

    // Update is called once per frame
    void Update()
    {
        if (bossInstance != null)
        {
            //get the current HP of the boss
            float currentHp = bossInstance.currentHealth;

            //if the boss hp dips below 15 then its time for phase 2
            if ( currentHp <= 15 )
            {
                phase2Enabled = true;
                Debug.Log("It's time for PHASE 2 BITCH");

                if (currentHp <= 0)
                {
                    //call boss death function outside of Update
                    bossDeath();
                }
            }
        }


        if (!phase2Enabled)
        {
            // logic for every 20-second spawn of eyes
            timer += Time.deltaTime;
            if (timer >= timeBetweenSpawn)
            {
                if (!spawnEyesTriggered)
                {
                    bossAnimator.SetTrigger("SpawnEyes");
                    StartCoroutine(spawnEyes());
                    spawnEyesTriggered = true;
                }
                else
                {
                    bossAnimator.ResetTrigger("SpawnEyes");
                    spawnEyesTriggered = false;
                    timer = 0f;
                }
            }
        } else if (phase2Enabled)
        {
            //once the health goes to a point where I want the chracter and boss to drop, I'm going to destroy specified prefabs/GOs
            Destroy(itemsToDestroy);

            //also move the animation to the next phase
            bossAnimator.SetTrigger("SecondPhase");
        }

        }

    public void SpawnEye()
    {
        if((eyeballPrefab != null) && (spawnPosition1 != null) && (spawnPosition2 != null) && (spawnPosition3 != null))
        {
            StartCoroutine(spawnEyes());
        }
       

    }


    public IEnumerator spawnEyes()
    {
        Debug.Log("SPAWN DEM EYES BOI");
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition1.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition2.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(eyeballPrefab, spawnPosition3.position, Quaternion.identity);
    }


    public void attack()
    {
        Vector2 pos = transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, attackRange, attackMask);

        // Visualize the overlap circle
        Debug.DrawRay(pos, Vector2.up * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.right * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.left * attackRange, Color.green);
        Debug.DrawRay(pos, Vector2.down * attackRange, Color.green);

        // Check if any of the colliders are the player's collider
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {

                ScoreManager.instance.TakeScore(2);
                pc.StartCoroutine(pc.flashSprite());
                break;
            }
        }
    }


    public void bossDeath()
    {
        //if the code finds itself here that means that the boss has died

        //Spawn the lever for the gate to leave
        //Instantiate(leverForDoor, transform.position, Quaternion.identity);
        leverForDoor.SetActive(true);
    }
}
