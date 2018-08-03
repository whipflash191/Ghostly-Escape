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
    [Range(0f, 4f)]
    public float movementSpeed;
    public Rigidbody2D rb;
    public List<Vector3> waypoints = new List<Vector3>();
    public float pointDelay;
    public int tempIndex;
    bool canWalk = true;
    public Vector3 currentWaypoint;

    void Start()
    {  
        currentWaypoint = waypoints[0];
        StartCoroutine(WalkCycle());
    }

    void Update()
    {
        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
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
            if (Vector3.Distance(transform.position, currentWaypoint) < 0.2f)
            {
                rb.velocity = new Vector2(0, 0);
                tempIndex = waypoints.IndexOf(currentWaypoint);
               if (tempIndex != waypoints.Count -1 || tempIndex == 0)
                {
                    currentWaypoint = waypoints[(tempIndex + 1)];
                } else
                {
                    currentWaypoint = waypoints[(tempIndex - 1)];
                }
                yield return new WaitForSeconds(2f);
            } else
            {
                rb.velocity = Vector3.Normalize((currentWaypoint - transform.position)) * movementSpeed;
                //rb.velocity = Vector2.MoveTowards(transform.position, currentWaypoint, movementSpeed);
                    //new Vector2(Mathf.Lerp(0, (currentWaypoint.x * movementSpeed), 0.8f), Mathf.Lerp(0, (currentWaypoint.y * movementSpeed), 0.8f));
            }

            yield return null;
        }
    }
}
