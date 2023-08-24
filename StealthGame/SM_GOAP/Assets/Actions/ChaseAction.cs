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
        return true;
    }
    private void Update()
    {
        target = player;
    }


}
