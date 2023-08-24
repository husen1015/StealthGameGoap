using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : Action
{
    public GameObject player;
    public override bool PostPrefom()
    {
        //Debug.Log("post preforming chase");
        //Debug.Log($"running chase? {running}");
        //Debug.Log($"can see player?: {FOV.Instance.canSeePlayer}");
        //UnityEditor.EditorApplication.isPaused = true;

        //check if caught
        if (Vector3.Distance(transform.position, target.transform.position) < 0.5f)
        {
            Debug.Log("caught!");
        }
        //else player got away - investigate
        else if (!FOV.Instance.canSeePlayer)
        {
            GameWorld.Instance.GetWorldStates1().SetState("Sighted", 1);
        }

        return true;
    }

    public override bool PrePrefom()
    {
        target = player;

        agent.SetDestination(player.transform.position);
        return true;
    }
    private void Update()
    {
        if (target != null && running) // Check if the action is currently running
        {
            agent.SetDestination(target.transform.position);
        }
    }


}
