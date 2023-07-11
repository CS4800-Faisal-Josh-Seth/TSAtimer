using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] public float currentHealth;
    public GameObject deathEffect;
    public GameObject healthBar;
    public AudioSource deathSound;
    public int soulValue;
    private Animator theAnim;
    private bool isDead;
    public static bool isInvincible = false;
    private void Start()
    {
        theAnim = GetComponent<Animator>();
        currentHealth = maxHealth;

        if ( healthBar != null)
        {
            // Set the initial health bar size
            healthBar.transform.localScale = new Vector3(1f, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (gameObject.tag != "Enemy" || gameObject.tag != "Spooder")
        {
            theAnim.SetTrigger("takeDamage");
        }
        StartCoroutine(flashSprite());


        if (healthBar != null)
        {
            // Calculate and set the new health bar size
            float proportionalDamage = damage / maxHealth;
            float newHealthSize = healthBar.transform.localScale.x - proportionalDamage;
            healthBar.transform.localScale = new Vector3(newHealthSize, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }

        if (currentHealth <= 0)
        {
            //turn off health when dead
            Destroy(healthBar);

            isDead = true;

            // Disable movement and collider
            Collider2D hitBox = GetComponent<Collider2D>();
            hitBox.enabled = false;

            SinusoidalMove sinMov = GetComponent<SinusoidalMove>();
            if (sinMov != null)
            {
                sinMov.enabled = false;
            }

            Spider spider = GetComponent<Spider>();
            if (spider != null)
            {
                spider.enabled = false;
            }

            ScoreManager.instance.ChangeScore(soulValue);
            Die();
        }
    }

    private void Die()
    {
        deathSound.Play();
        isDead = true;

        // Disable movement and collider
        Collider2D hitBox = GetComponent<Collider2D>();
        hitBox.enabled = false;

        SinusoidalMove sinMov = GetComponent<SinusoidalMove>();
        if (sinMov != null)
        {
            sinMov.enabled = false;
        }

        Spider spider = GetComponent<Spider>();
        if (spider != null)
        {
            spider.enabled = false;
        }

        ScoreManager.instance.ChangeScore(soulValue);
        theAnim.SetTrigger("die"); // Set the die trigger in the Animator component
        StartCoroutine(DestroyEnemy());
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(theAnim.GetCurrentAnimatorStateInfo(0).length); // Wait for the length of the die animation state
        Destroy(gameObject); // Destroy the enemy game object
    }

    private IEnumerator StopAnim()
    { 
        yield return new WaitForSeconds(0.05f);
        theAnim.SetBool("Dead", isDead);
        Destroy(gameObject);
    }

    public IEnumerator flashSprite()
    {
        isInvincible = true;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(.2f);
            sprite.enabled = true;
            yield return new WaitForSeconds(.2f);
            sprite.enabled = false;
            yield return new WaitForSeconds(.2f);
            sprite.enabled = true;
            yield return new WaitForSeconds(.2f);
            sprite.enabled = false;
            yield return new WaitForSeconds(.2f);
            sprite.enabled = true;
            isInvincible = false;
            if (deathEffect != null)
            {
                Instantiate(deathEffect, transform.position, Quaternion.identity);
            }

        }

    }
}
