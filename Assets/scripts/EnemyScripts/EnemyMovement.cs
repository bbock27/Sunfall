using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float movementSpeed = 1f;
    public float detectionRange = 3f; //how close the player must be for the enemy to see and start to follow them
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D enemyRbody;
    Rigidbody2D playerRbody; //player 
    
    private Dictionary<Vector2Int, WalkableTile> searchableTiles;

    CapsuleCollider2D playerCollider;
    CircleCollider2D detectionTrigger;
    public bool hasBeenDetected = false;

    private void Awake()
    {
        enemyRbody = GetComponent<Rigidbody2D>();
        playerRbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>(); //player
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();

        playerCollider = GameObject.FindWithTag("Player").GetComponentInChildren<CapsuleCollider2D>();

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
    }

    Vector2 GetPath()
    {
        Vector2 enemyPos = enemyRbody.position;
        Vector2 direction = playerRbody.position - enemyPos;
        direction.Normalize();
        return enemyPos + direction * movementSpeed * Time.fixedDeltaTime; 
    }

    void NavigatePath(Vector2 path)
    {
        isoRenderer.SetDirection(path.normalized);
        enemyRbody.MovePosition(path);
    }

}
