using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOPEnemy : GameAgent
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SubGoal s1 = new SubGoal("Patrol", 1, true);
        SubGoals.Add(s1, 3);
    }


}
