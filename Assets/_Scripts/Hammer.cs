using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float knockbackPower;
    public bool canSmash;
    public Collider2D hammerHitBox;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSmash = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 forceRight = new Vector2(500, 1000).normalized * knockbackPower;
        Vector2 forceLeft = new Vector2(-500, 1000).normalized * knockbackPower;

        if (collision.CompareTag("Enemy") && canSmash)
        {

            Rigidbody2D enemyRb = collision.GetComponent<Rigidbody2D>();

            if (player.transform.position.x > collision.transform.position.x)
            {
                enemyRb.AddForce(forceLeft);
            }

            else
            {
                enemyRb.AddForce(forceRight);
            }



        }
    }

}
