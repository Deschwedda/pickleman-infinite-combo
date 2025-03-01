using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private Vector2 direction;
    public int explosionPower;
    private GameManager gameManagerSc;


    void Start()
    {
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManagerSc = gameManagerObject.GetComponent<GameManager>();
        
    }
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();
            Enemy enemySc = collision.gameObject.GetComponent<Enemy>();
            enemySc.hasBeenHit = true;
            direction = (collision.transform.position - transform.position).normalized;
            direction = new Vector2(direction.x / 3, direction.y * 30);
            enemyRb.AddForce(direction * explosionPower, ForceMode2D.Impulse);
            gameManagerSc.AddPoints();

        }
    }    
}
