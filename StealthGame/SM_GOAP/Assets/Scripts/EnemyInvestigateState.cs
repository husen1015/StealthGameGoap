//using System;
//using System.Diagnostics;
//using UnityEngine;

//public class EnemyInvestigateState : EnemyBaseState
//{
//    Vector3 lastKnownLocation;
//    public override void enterState(EnemyStateManager manager)
//    {
//        UnityEngine.Debug.Log("ínvestigating");
//        lastKnownLocation = player.position;
//        agent.destination = lastKnownLocation;
//    }

//    public override void updateState(EnemyStateManager manager)
//    {
//        UnityEngine.Debug.Log($"last knwon loc: {lastKnownLocation}");

//        //if at last known location for at least 2 seconds then return to patrol, if can see enemy then chase
//        if (Vector3.Distance(enemy.transform.position, lastKnownLocation) <= 0.3f)
//        {
//            // TODO- if seen player here should chase
//            manager.SwitchState(manager.patrolState);
//        }
//        else
//        {
//            agent.destination = lastKnownLocation;

//        }
//    }
//}
