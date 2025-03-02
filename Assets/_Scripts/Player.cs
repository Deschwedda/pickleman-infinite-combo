using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{
    [Header("Hammer Variables")]
    public GameObject hammerPivot;
    public float knockbackPower;
    public float swingDuration;


    [Header("Player Variables")]
    public GameObject player;
    public float movementSpeed;


    private Rigidbody2D rb;
    private Vector3 input;
    private bool isSwinging;
    private GameManager gameManagerSc;
    public AudioSource hammerSound;
    public Animator hammerAnimator;

    private void Awake()
    {

    }

    void Start()
    {

        hammerSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        GameObject gameManagerObject = GameObject.Find("GameManager");
        gameManagerSc = gameManagerObject.GetComponent<GameManager>();
    }


    void Update()
    {
        HandleMovement();
        HandleSwing();
    }

    private void FixedUpdate()

    {
        rb.linearVelocity = input * movementSpeed;
        if (player.transform.position.x < -8.20f)
            player.transform.position = new Vector2(-8.20f, player.transform.position.y);
        else if (player.transform.position.x > 8.20f)
            player.transform.position = new Vector2(8.20f, player.transform.position.y);
    }

    void HandleSwing()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isSwinging)
            StartCoroutine(SwingHammer());
    }

    void HandleMovement()
    {
        input.x = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.transform.rotation = (Quaternion.Euler(0, 180, 0));
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.transform.rotation = (Quaternion.Euler(0, 0, 0));
        }

    }

    IEnumerator SwingHammer()
    {
        if (gameManagerSc.isComboActive == false)
        {
            gameManagerSc.isComboActive = true;
        }
        if (gameManagerSc.comboCount == 0)
        {
            isSwinging = true;
            hammerAnimator.SetTrigger("Swing");
            yield return new WaitForSeconds(0.80f);
            hammerSound.Play();
            isSwinging = false;
        }


    }

    IEnumerator RotateHammer(Quaternion from, Quaternion to)
    {
        float elapsed = 0f;
        while (elapsed < swingDuration)
        {
            hammerPivot.transform.localRotation = Quaternion.Slerp(from, to, elapsed / swingDuration);
            elapsed += Time.deltaTime;
            yield return null;

        }
    }
}
