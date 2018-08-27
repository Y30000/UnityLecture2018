using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public const int maxHealth = 100;
    public float currentHealth = maxHealth;
    public RectTransform healthBar;

    public void DecreaseHP(float amount)
    {
        currentHealth -= amount;
        //Mathf.Clamp01(currentHealth / maxHealth);
        if(currentHealth <= 0f)
        {
            currentHealth = 0f;
            print(gameObject.name + " 쥬금");
            Destroy(gameObject);
        }
        healthBar.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }
    public void IncreaseHP(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth,0,maxHealth);
        healthBar.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
    }
}
