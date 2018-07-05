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
    public float pointDelay;
    int tempIndex;
    bool canWalk = true;
    NavMeshAgent enemyAgent;
    Vector3 currentWaypoint;

    void Start()
    {  
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAgent.updateUpAxis = false;
        enemyAgent.updateRotation = false;
        enemyAgent.SetDestination(waypoints[0]);
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
        for (int i = 0; i <= transform.childCount; i++)
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
        while (canWalk)
        {
            if (enemyAgent.remainingDistance <= enemyAgent.stoppingDistance)
            {
                if (tempIndex == waypoints.Count)
                {
                    currentWaypoint = waypoints[0];
                    tempIndex = waypoints.IndexOf(currentWaypoint);
                    yield return new WaitForSecondsRealtime(pointDelay);
                    enemyAgent.SetDestination(waypoints[0]);
                }
                else
                {
                    currentWaypoint = waypoints[(tempIndex)];
                    tempIndex = waypoints.IndexOf(currentWaypoint);
                    yield return new WaitForSecondsRealtime(pointDelay);
                    enemyAgent.SetDestination(waypoints[tempIndex]);
                    tempIndex++;
                }
            }
        }
    }
}
