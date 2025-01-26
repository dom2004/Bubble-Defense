using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("References")] 
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Attributes")] 
    [SerializeField]
    private float arrowSpeed = 5f;

    [SerializeField] 
    private int arrowDamage = 1;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
      
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * arrowSpeed;
    }

    private void OnCollisionEnter2D(UnityEngine.Collision2D other)
    {
        other.gameObject.GetComponent<EnemyHealth>().TakeDamage(arrowDamage);

        Destroy(gameObject);
    }

}
