using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D enemyRbody;
    Rigidbody2D playerRbody; //player 
    
    private Dictionary<Vector2Int, WalkableTile> searchableTiles;

    private void Awake()
    {
        enemyRbody = GetComponent<Rigidbody2D>();
        playerRbody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>(); //player
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 path = GetPath();
        NavigatePath(path);
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
