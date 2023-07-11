using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Essentials
    private Rigidbody2D rb;
    public Transform startPoint;
    private bool shouldPerformRaycast = true;

    // Sounds
    public AudioSource se;
    public AudioClip spikeFall;
    private bool hasPlayedAudio = false;

    // Falling speed
    public float fallingSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    // Update is called once per frame
    void Update()
    {
        // Check if startPoint is null
        if (startPoint == null)
        {
            return;
        }

        // Perform raycast only if the flag is set to true
        if (shouldPerformRaycast && !hasPlayedAudio)
        {
            // Obtain the position of the specified start point
            Vector2 startPos = startPoint.position;

            // Define the direction of the raycast
            Vector2 direction = Vector2.down;

            // Cast a ray downwards from the specified start point
            RaycastHit2D hit = Physics2D.Raycast(startPos, direction);

            if (hit.collider != null && !hit.collider.CompareTag("sliders"))
            {
                se.PlayOneShot(spikeFall);
                hasPlayedAudio = true;

                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddForce(Vector2.down * fallingSpeed, ForceMode2D.Impulse);

                StartCoroutine(FallingSpike());

                // A collision occurred, you can access the hit information here
            }
        }
    }

    IEnumerator FallingSpike()
    {
        yield return new WaitForSeconds(2f);

        // Disable the raycast by setting the flag to false
        shouldPerformRaycast = false;

        // Disable the gameObject
        gameObject.SetActive(false);
    }
}
