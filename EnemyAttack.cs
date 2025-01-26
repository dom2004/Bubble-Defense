using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    private void OnCollisionStay2D(Collision2D collision)
    {
        HealthController controller = collision.gameObject.GetComponent<HealthController>();
        if (controller != null)
        {
            controller.TakeDamage(_damageAmount);

            Debug.Log($"Bubble Health: {controller.RemainingHealthPercentage * 100}%");
        }
    }
}
