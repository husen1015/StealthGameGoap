using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPEnemy : GameAgent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SubGoal goal1 = new SubGoal("Patrolling", 3, true);
        SubGoal goal2 = new SubGoal("Chasing", 9, true);

        SubGoals.Add(goal1, 3);
        SubGoals.Add(goal2, 6); //chase is with higher priority so if both goals are achievable this goal will be chosen
    }
    //private void Update()
    //{
    //    if (FOV.Instance.canSeePlayer)
    //    {
    //        Debug.Log("recalculating...");
    //        RecalculatePlan();
    //    }
    //}


}
