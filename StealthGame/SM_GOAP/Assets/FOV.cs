using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public Transform player;
    public float radius;
    float PlayerEnemyDistance;

    //raycasting
    public bool canSeePlayer = false;
    [Range(0, 360)]
    public float angle;
    public LayerMask targetMask; // mask for targets - currently contains only player
    public LayerMask obstructionMask; // mask for obstructions such as walls

    public static FOV Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }
    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {

        PlayerEnemyDistance = Vector3.Distance(player.transform.position, transform.position);
        //Debug.Log(PlayerEnemyDistance);
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);


        //if there is an enemy in range
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform; //only the player is in the array
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, player.transform.position);

            //if enemy facing player
            if (Vector3.Angle(transform.forward, dirToTarget) < angle / 2)
            {
                //raycast to check whether enemy can see player and no obstruction is in the way
                if (!Physics.Raycast(transform.position, dirToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }

        }

        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }

    }
}
