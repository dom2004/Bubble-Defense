using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maxHealth;

    private Animator anim;

    public GameOver gameOver;

    public float RemainingHealthPercentage => _currentHealth / _maxHealth;
    public float CurrentHealth => _currentHealth; 
    public float MaxHealth => _maxHealth;        

    public UnityEvent OnHealthChanged;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        _currentHealth -= damageAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        OnHealthChanged?.Invoke();

        if (_currentHealth == 0)
        {
            TriggerGameOver();
            return;
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maxHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth); 

        OnHealthChanged?.Invoke();
    }

    private void TriggerGameOver()
    {
        if (gameOver != null)
        {
            gameOver.Setup();
        }
        else
        {
            Debug.LogError("GameOver is not assigned in the Inspector!");
        }
    }
}
