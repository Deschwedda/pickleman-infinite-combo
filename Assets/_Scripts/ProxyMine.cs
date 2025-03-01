using UnityEngine;

public class ProxyMine : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionArea;
    public GameObject explodedMine;
    public GameManager GameManagerSc;
    public bool hasExploded;
    public AudioClip boomSound;

    
    void Start()
    {
        GameManagerSc = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if (GameManagerSc.comboCount == 0)
        {
            explodedMine.SetActive(false);            
            explosionArea.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hammer"))
        {
            AudioManagerProxyMine.Instance.PlaySound(boomSound); // play boom sfx
            GameManagerSc.AddPoints(); 
            if (explosion != null)
                explosion.SetActive(true);
            if (explosionArea != null)
                explosionArea.SetActive(true);
            if (explodedMine != null)
                explodedMine.SetActive(true);
            Invoke("DeactiveExplosionArea", 0.2f);
        }

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemySc = collision.gameObject.GetComponent<Enemy>();
            if (enemySc.hasBeenHit == true && explodedMine.activeSelf == false)
            {
                AudioManager.Instance.PlaySound(boomSound); // play boom sfx
                if (explosion != null)
                    explosion.SetActive(true);
                if (explosionArea != null)
                    explosionArea.SetActive(true);
                if (explodedMine != null)
                    explodedMine.SetActive(true);
                Invoke("DeactiveExplosionArea", 0.2f);

            }
        }

    }


    private void DeactiveExplosionArea()
    {
        explosionArea.SetActive(false);
        explosion.SetActive(false);
    }

   
}
