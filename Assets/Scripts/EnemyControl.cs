/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();
    NavMeshAgent enemyAgent;

    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(WalkCycle());
    }

    void Update()
    {
        
    }

    public void AssignWaypoints()
    {
        if(transform.GetChild(0) != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                waypoints.Add(transform.GetChild(i).transform.position);
            }
        } else
        {
            Debug.Log("No Waypoints Found");
        }
    }

    public void ResetWaypointList()
    {
        waypoints.Clear();
    }

    public void DestroyWaypoints()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public void CreateWaypoint()
    {
        GameObject newWaypoint = new GameObject("waypoint" + transform.childCount.ToString());
        newWaypoint.transform.position = transform.position;
        newWaypoint.transform.SetParent(gameObject.transform);
    }

    IEnumerator WalkCycle()
    {
        return null;
    }
}
