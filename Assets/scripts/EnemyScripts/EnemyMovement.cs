using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float movementSpeed = 1f;
    public float detectionRange = 3f; //how close the player must be for the enemy to see and start to follow them
    IsometricEnemyRenderer isoRenderer;

    Rigidbody2D enemyRbody;
    Rigidbody2D playerRbody; //player 
    
    private Dictionary<Vector2Int, WalkableTile> searchableTiles;

    BoxCollider2D playerCollider;
    private BoxCollider2D enemyHitbox;
    CircleCollider2D detectionTrigger;
    Player_Health playerHealth;
    public bool hasBeenDetected = false;
    public bool inRange = false;
    float elapsedTime = 0f;

    private void Awake()
    {
        enemyRbody = GetComponent<Rigidbody2D>();
        playerRbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>(); //player
        isoRenderer = GetComponentInChildren<IsometricEnemyRenderer>();

        playerCollider = GameObject.FindWithTag("Player").GetComponentInChildren<BoxCollider2D>();
        enemyHitbox = GetComponent<BoxCollider2D>();
        
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Player_Health>();

        //trigger for detection radius
        detectionTrigger = GetComponent<CircleCollider2D>();
        detectionTrigger.radius = detectionRange;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasBeenDetected){
            
            Vector2 path = GetPath();
            NavigatePath(path);
        }
    }

    void Update() {
        if(detectionTrigger.IsTouching(playerCollider)){
            hasBeenDetected = true;
        }
        else
        {
            hasBeenDetected = false;
        }

        if (enemyHitbox.IsTouching(playerCollider))
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (inRange && elapsedTime > 2)
        {
            elapsedTime = 0f;
            playerHealth.TakeDamage(10);
        }
        else
        {
            elapsedTime += Time.deltaTime;
        }
    }

    Vector2 GetPath()
    {
        if (inRange)
        {
            isoRenderer.SetDirection(new Vector2(0, 0));
            return enemyRbody.position;
        }
        Vector2 enemyPos = enemyRbody.position;
        Vector2 direction = playerRbody.position - enemyPos;
        direction.Normalize();
        isoRenderer.SetDirection(direction);
        return enemyPos + direction * (movementSpeed * Time.fixedDeltaTime); 
    }

    void NavigatePath(Vector2 path)
    {
        enemyRbody.MovePosition(path);
    }

}
