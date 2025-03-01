using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Turret Settings:")]
    public Transform turretPivot;
    public Transform shootingPoint;
    public float attackRate = 2f;
    public bool isShooting;
    private Coroutine shootingCoroutine;
    public AudioClip shootingSound;

    [Header("Aim Settings:")]
    public float detectionRadius = 5f;
    public Transform enemy;
    public LayerMask enemyLayer;
    private Enemy enemyScript;

    [Header("Bullet Settings:")]
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;   
   

    private GameManager gameManagerSc;
    private void Start()
    {        
        gameManagerSc = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        DetectEnemy();

        if (enemy != null)
        {
            //Check if enemy is in the range
            if (Vector2.Distance(shootingPoint.position, enemy.position) > detectionRadius) //if not
            {
                enemy = null; //clear enemy variable
            }
            else //if it is
            {
                Aim();
            }

        }

        // Automaticly stop shooting when enemy is out of range
        if (enemy == null && isShooting)
        {
            isShooting = false;
        }

    }


    private IEnumerator Shoot()
    {
        while (true)
        {
            if (enemy != null)
            {
                AudioManager.Instance.PlaySound(shootingSound); // Play shooting sound
                GameObject newBullet = Instantiate( //spawn the bullet 
                    bulletPrefab,
                    shootingPoint.position,
                    Quaternion.identity
                );

                Bullets bulletScript = newBullet.GetComponent<Bullets>();
                if (bulletScript != null)
                {
                    bulletScript.target = enemy;
                    bulletScript.speed = bulletSpeed;
                }

                Vector2 direction = (enemy.position - shootingPoint.position).normalized;
                newBullet.transform.right = direction;
            }

            yield return new WaitForSeconds(attackRate * gameManagerSc.turretAttackRateMultiplier); // Check CD, wait before the next shoot

            if (enemy == null) break; // check if the enemy is still there
        }

        // Reset Corotine
        shootingCoroutine = null;
        isShooting = false;
    }

    private void DetectEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(
            shootingPoint.position,
            detectionRadius,
            enemyLayer
        );

        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        //find the nearest enemy
        foreach (Collider2D hit in hits)
        {
            {
                float distance = Vector2.Distance(shootingPoint.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }

        }

        if (closestEnemy != null)
        {
            enemy = closestEnemy;
        }

        if (!isShooting)
        {
            isShooting = true;
            if (shootingCoroutine == null)
            {
                shootingCoroutine = StartCoroutine(Shoot());
            }
        }
        else
        {
            enemy = null;
        }
    }

    void Aim()
    {
        Vector2 direction = enemy.position - turretPivot.position;
        turretPivot.up = -direction;
    }


}