using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    
    private EnemyHealth enemyHealth;
    void OnTriggerEnter2D(Collider2D other)
    {
        //other.gameObject(); //gives the gameObject that other is colliding with
        if (other.gameObject.tag == "Player Attack")
        {
            
            //if you destroy yourself no other processes continue! make sure destroy self is the very last thing
            Destroy(this.gameObject);
        }
    }
    
    void Awake() {
        //enemyHealth = gameObject.GetComponent<PlayerHealth>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
