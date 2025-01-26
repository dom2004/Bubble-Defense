using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [Header("References")] 
    [SerializeField]
    private Transform towerRotation;

    [SerializeField] 
    private LayerMask enemyMask;

    [SerializeField] 
    private GameObject arrowPrefab;

    [SerializeField] 
    private Transform firingPoint;

    [Header("Attribute")] 
    [SerializeField] 
    private float targetRange = 5f;

    [SerializeField] 
    private float rotationSpeed = 5f;

    [SerializeField] 
    private float aps = 1f; //Arrows Per Second

    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateToTarget();

        if (!CheckTargetInRange())
        {
            target = null;
        }
        else
        {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / aps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }

    }

    private void Shoot()
    {
        Quaternion arrowRotation = Quaternion.LookRotation(Vector3.forward, target.position - firingPoint.position);
        GameObject arrowObj = Instantiate(arrowPrefab, firingPoint.position, arrowRotation);
        Arrow arrowScript = arrowObj.GetComponent<Arrow>();
        arrowScript.SetTarget(target);
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetRange;
    }

    private void RotateToTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) *
                      Mathf.Rad2Deg + 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        towerRotation.rotation = Quaternion.RotateTowards(
            towerRotation.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime 
        );
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetRange);
    }
#endif
}
