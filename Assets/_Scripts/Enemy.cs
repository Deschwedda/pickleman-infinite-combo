using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocity = 1f;
    public float idleTimer = 0f;
    public const float maxIdleTime = 2f;
    public Vector2 lastPosition;
    private Player playerScript;
    public bool hasBeenHit;
    public bool canBeSmashed;
    private float setReadyTimer = 1.1f;
    public int aimedForTimes = 0;
    private GameManager gameManagerSc; 

    private void Awake()
    {
        canBeSmashed = true;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManagerSc = gameManagerObject.GetComponent<GameManager>();

    }


    void Start()
    {
        lastPosition = transform.position;
        Invoke(nameof(SetReady), setReadyTimer);
    }


    void Update()
    {
        //Check if there's any movement 
        CheckMovement();

        if (idleTimer >= maxIdleTime)
        {
            DestroyEnemy();
        }
    }

    private void FixedUpdate()
    {        
        if (!hasBeenHit && canBeSmashed)
        {
            transform.position += Vector3.left * velocity * Time.deltaTime;
        }

    }


    void CheckMovement()
    {
        if ((Vector2)transform.position != lastPosition)
        {
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        // if it moves slitghtly, it will return to the last position
        if (idleTimer > 0 && idleTimer < 0.1f)
        {
            transform.position = lastPosition;
        }

        lastPosition = transform.position;
    }

    void SetReady()
    {
        gameObject.tag = ("Enemy");
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector2 forceRight = new Vector2(500, 1000).normalized * playerScript.knockbackPower;
        Vector2 forceLeft = new Vector2(-500, 1000).normalized * playerScript.knockbackPower;

        if (collision.CompareTag("Hammer") && canBeSmashed && gameObject.CompareTag("Enemy"))
        {
            gameManagerSc.AddPoints();
            Rigidbody2D enemyRb = gameObject.GetComponent<Rigidbody2D>();

            if (collision.transform.position.x > gameObject.transform.position.x)
            {
                enemyRb.AddForce(forceLeft);
                canBeSmashed = false;
            }

            else
            {
                enemyRb.AddForce(forceRight);
                canBeSmashed = false;
            }
        }
    }
}
