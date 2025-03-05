using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D rbody;
    
    private Dictionary<Vector2Int, WalkableTile> searchableTiles;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        GetPath();
        NavigatePath();
        
        // Vector2 currentPos = rbody.position;
        // float horizontalInput = Input.GetAxis("Horizontal");
        // float verticalInput = Input.GetAxis("Vertical");
        // Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        // inputVector = Vector2.ClampMagnitude(inputVector, 1);
        // Vector2 movement = inputVector * movementSpeed;
        // Vector2 newPos = currentPos + movement * Time.fixedDeltaTime;
        // isoRenderer.SetDirection(movement);
        // rbody.MovePosition(newPos);
    }
    
    
    



    void GetPath()
    {
        
    }

    void NavigatePath()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.GameObject.tag == "Player" && this.GameObject.tag == "Enemy") {
            
        }
    }
}
