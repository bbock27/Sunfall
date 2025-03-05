using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    
    public int maxHealth;
    public int currentHealth;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
