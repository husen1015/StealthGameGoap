using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : Action
{
    public GameObject player;
    public override bool PostPrefom()
    {
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
