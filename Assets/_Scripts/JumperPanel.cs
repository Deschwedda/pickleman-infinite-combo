using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class JumperPanel : MonoBehaviour

{
    [Header("Jumper Panel Settings")]
    public Transform movingPanel;
    public float platformCooldown = 1f;
    public float animationDuration = 0.5f;
    public float targetYPosition;
    public float startYPosition;
    
       

    [Header("Push Power Settings")]
    public float pushPower;

    //[Header("Other")]
    private GameManager gameManagerSc;
    private bool isMoving;
    private Vector2 direction;
    private Enemy enemySc;
    private Rigidbody2D enemyRb;
    void Start()
    {
        
        movingPanel.localPosition = new Vector3(movingPanel.localPosition.x, movingPanel.localPosition.y, movingPanel.localPosition.z);
        // Find gameManagerObj and save its referance. 
        GameObject gameManagerObject = GameObject.Find("GameManager");
        if (gameManagerObject != null)
        {
            gameManagerSc = gameManagerObject.GetComponent<GameManager>();
            if (gameManagerSc == null)
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy") && gameManagerSc.isComboActive)
        {
            // define the enemy and its rigidbody
            enemySc = collision.gameObject.GetComponent<Enemy>();
            enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();

            //check if the enemy is ready to be pushed
            if (enemySc.hasBeenHit == false && enemySc.canBeSmashed == true) return; //if it's moving by itself, don't do anything
            if (isMoving) return; //if the panel is moving, don't do anything

            //calculate the direction of the push
            direction = (collision.transform.position - movingPanel.position).normalized;
            direction = new Vector2(direction.x/3, direction.y * 3);
            enemyRb.AddForce(direction * pushPower, ForceMode2D.Impulse);

            //move the panel
            StartCoroutine(MovePanel());
            gameManagerSc.AddPoints();
        }
    }

    IEnumerator MovePanel()
    {
        isMoving = true;

        // Set the start and target positions
        Vector3 startPosition = new Vector3(movingPanel.localPosition.x, startYPosition, movingPanel.localPosition.z);
        Vector3 targetPosition = new Vector3(movingPanel.localPosition.x, targetYPosition, movingPanel.localPosition.z);    
            
        float elapsedTime = 0f;

        // Move the panel from START position to TARGET position
        while (elapsedTime < animationDuration)
        {
            movingPanel.localPosition = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        movingPanel.localPosition = targetPosition;

        // Move the panel from TARGET position to START position
        elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            movingPanel.localPosition = Vector3.Lerp(targetPosition, startPosition, (elapsedTime / animationDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Set the panel to start position
        movingPanel.localPosition = startPosition;

        // Wait for the cooldown 
        yield return new WaitForSeconds(platformCooldown * gameManagerSc.platformCDMultiplier);

        isMoving = false;
    }
}
