using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator anim;
    private bool isDying = false;

    [SerializeField]
    private float _damageAmount = 10f;

    private HealthController controller;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        controller = GetComponent<HealthController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (!isDying)
            {
                anim.SetTrigger("isHit");
            }

            controller.TakeDamage(_damageAmount);

            if (controller.RemainingHealthPercentage <= 0)
            {
                isDying = true;
                StartCoroutine(PlayDeathAnimation());
            }
        }
    }

    private IEnumerator PlayDeathAnimation()
    {
        anim.SetTrigger("isDead"); 

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        Destroy(gameObject); 
    }
}
