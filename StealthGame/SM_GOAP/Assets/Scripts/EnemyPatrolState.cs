using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolState : EnemyBaseState
{

    Transform[] waypoints;
    int nextWaypointIndx=0;
    Transform nextWaypoint;
    public override void enterState(EnemyStateManager manager)
    {
        Debug.Log("entered patrolling state");
        //enemy = manager.Enemy;
        //player = manager.Player;
        waypoints = Waypoints.waypointsArr;
        nextWaypoint = waypoints[nextWaypointIndx];
        //agent = manager.GetComponent<NavMeshAgent>();
    }

    public override void updateState(EnemyStateManager manager)
    {
        //if(Vector3.Distance(enemy.transform.position, player.transform.position) <= 10f)
        //{
        //    manager.SwitchState(manager.alertState);
        //}

        //instead of checking for distance only, check if enemy can actually see player using raycasting
        if (FOV.Instance.canSeePlayer)
        {
            manager.SwitchState(manager.alertState);

        }
        // if reached next waypoint update new destination
        if (Vector3.Distance(enemy.transform.position, nextWaypoint.transform.position) <= 0.3)
        {
            //update next index. reset to 0 when done
            nextWaypointIndx = nextWaypointIndx == waypoints.Length ? 0 : nextWaypointIndx + 1;
            nextWaypoint = waypoints[nextWaypointIndx];
            Debug.Log($"goign to new waypoint index = {nextWaypointIndx}");
            
        }
        agent.destination = nextWaypoint.position;

    }

}
