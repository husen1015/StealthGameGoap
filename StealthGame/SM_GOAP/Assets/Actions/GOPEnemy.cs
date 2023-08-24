using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPEnemy : GameAgent
{
    void Start()
    {
        base.Start();
        SubGoal goal1 = new SubGoal("Patrolling", 3, true);
        SubGoal goal2 = new SubGoal("Chasing", 6, true);
        SubGoal goal3 = new SubGoal("CatchPlayer", 9, true);
        SubGoal goal4 = new SubGoal("Investigated", 5, false);


        SubGoals.Add(goal1, 3);
        SubGoals.Add(goal2, 6); //chase is with higher priority so if both goals are achievable this goal will be chosen
        SubGoals.Add(goal3, 9); 
        SubGoals.Add(goal4, 8);
    }


}
