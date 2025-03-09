using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player_Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int maxHealth = 100;
    public int currentHealth;
    private float elapsedTime = 0.0f;
    public Slider healthSlider;
    void Start()
    {
        currentHealth = maxHealth;
        SetHealthSlider(100);
        
        
    }
    
    

    void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        //display health
        SetHealthSlider(currentHealth);
        if (CheckDeath())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    bool CheckDeath()
    {
        return currentHealth <= 0;
    }

    public void SetHealthSlider(int health)
    {
        healthSlider.value = health;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= 1)
        {
            elapsedTime = 0;
            TakeDamage(20);
        }

        
        
    }
}
