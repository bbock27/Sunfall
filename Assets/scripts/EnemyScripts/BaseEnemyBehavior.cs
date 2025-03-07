using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyBehavior : MonoBehaviour
{
    public float movementSpeed = 1f;
    public float detectionRange = 3f; //how close the player must be for the enemy to see and start to follow them
    public int health; //current health of the enemy, initialized in Start()
    public int attackDamage = 10; //the amount of damage the enemy attack does
    public bool hasBeenDetected = false; //becomes true when player first steps into range of the enemy
    IsometricCharacterRenderer isoRenderer;

    Rigidbody2D enemyRbody;
    Rigidbody2D playerRbody; //player 
    CapsuleCollider2D playerCollider;
    CircleCollider2D detectionTrigger;
    
    //private Dictionary<Vector2Int, WalkableTile> searchableTiles;

    /* TODO: things that the base enemy should have
     * movement - base enemy just follows the player around. detect within range (stay locked on after you go within range once) 
     * attack - base enemy bites or scratches or something (damage when hitboxes touch)
     * health
     * take damage
     */


    void Start()
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
    void Update()
    {
        if(detectionTrigger.IsTouching(playerCollider)){
            hasBeenDetected = true;
        }
        
    }

    private void Awake()
    {
        //initial health
        health = 100;
    }


    void FixedUpdate()
    {
        if(hasBeenDetected){
            Vector2 path = GetPath();
            NavigatePath(path);
        }
    }

    //returns a vector with the magnitude and direction the enemy should move towards the player
    Vector2 GetPath()
    {
        Vector2 enemyPos = enemyRbody.position;
        Vector2 direction = playerRbody.position - enemyPos;
        direction.Normalize();
        return enemyPos + direction * movementSpeed * Time.fixedDeltaTime; 
    }

    //takes a vector as the path and moves the enemy rigidbody the amount specified by the vector
    void NavigatePath(Vector2 path)
    {
        isoRenderer.SetDirection(path.normalized);
        enemyRbody.MovePosition(path);
    }

    void TakeDamage(int damageDealt)
    {
        health -= damageDealt;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //other.gameObject(); //gives the gameObject that other is colliding with
        if (other.gameObject.tag == "Player Attack")
        {
            //Destroy(other.gameObject); //get rid of the bullet first
            //if you destroy yourself no other processes continue! make sure destroy self is the very last thing
            Destroy(this.gameObject);
        }
    }

    //in attack, reference the thing being attacked and call their take damage method
}
