using UnityEngine;

public class EscapeTunnel : MonoBehaviour
{
    private Enemy enemySc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemySc = collision.gameObject.GetComponent<Enemy>();
            if (enemySc.hasBeenHit == false && enemySc.canBeSmashed == true)
            {
            Destroy(collision.gameObject);
            }



    }
}
