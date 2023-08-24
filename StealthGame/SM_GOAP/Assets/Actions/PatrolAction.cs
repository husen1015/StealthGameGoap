using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PatrolAction : Action
{
    public GameObject[] waypoints;
    private int nextWaypointIndx = 0;
    GameObject nextWaypoint;
    public override bool PostPrefom()
    {
        Debug.Log("post preforming");
        ////update nexr target
        nextWaypointIndx = nextWaypointIndx + 1 >= waypoints.Length ? 0 : nextWaypointIndx + 1;
        target = waypoints[nextWaypointIndx];
        return true;
    }

    public override bool PrePrefom()
    {
        return true;
    }
    private void Start()
    {
        waypoints = Waypoints.waypointsArr;
        nextWaypoint = waypoints[nextWaypointIndx];
        target = waypoints[0];

    }

}
