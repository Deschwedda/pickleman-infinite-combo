using UnityEngine;

public class Bullets : MonoBehaviour
{
    
    [HideInInspector] public Transform target;
    [HideInInspector] public float speed;
    public float knockbackForce = 10f; // Inspector'dan ayarlanabilir
    private Vector2 direction;
    GameManager gameManager;
    public bool hasHit;

    void Start()
    {
        // Find gameManagerObj and save its referance. 
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("can't find GameManager script!");
            }
        }
        else
        {
            Debug.LogError("can't find GameManager object!");
        }
    }

    void Update()
    {
        if (target != null && !hasHit)
        {
            direction = (target.position - transform.position).normalized;
        }
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        if (collision.CompareTag("Enemy") && gameManager.isComboActive)
        {
            hasHit = true;
            Enemy enemyScript = collision.GetComponent<Enemy>(); // reach Enemy.cs ###########            
            ApplyKnockback(collision);
            if (gameManager != null)
            {
                gameManager.AddPoints();
            }
            Destroy(gameObject);
            enemyScript.hasBeenHit = true;

        }
    }

    void ApplyKnockback(Collider2D enemyCollider)
    {
        Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
        if (enemyRb == null) return;

        // Set knockback direction
        Vector2 knockbackDirection = direction;

        // Set its power
        enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    

}