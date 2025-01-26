using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Attributes")] 
    [SerializeField]
    private int hitPoints = 2;

    [SerializeField] 
    private int currencyGain = 50;

    private bool isDestroyed = false;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDeath.Invoke();
            LevelManager.main.IncreaseCurrency(currencyGain);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}
