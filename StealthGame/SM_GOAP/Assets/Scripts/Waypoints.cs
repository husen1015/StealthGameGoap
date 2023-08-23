using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static GameObject[] waypointsArr;

    void Awake()
    {
        waypointsArr= new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            waypointsArr[i] = transform.GetChild(i).gameObject;
        }
    }
}
