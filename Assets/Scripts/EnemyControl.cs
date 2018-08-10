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
    /*
     * This Script Controls the Enemy NPCs
     * It Also Contains the Functions Used in the "EnemyEditor" Script Used to Manage Nav Waypoints
     * Waypoints are assigned to a list in the order they appear in the heirarchy
     */
    [Range(0f, 4f)]
    public float movementSpeed;
    public float waypointDelay = 2f;
    public float waypointMinDistance = 0.2f;
    public Rigidbody2D rb;
    public List<Vector3> waypoints = new List<Vector3>();
    float pointDelay;
    int tempIndex;
    bool canWalk = true;
    public Vector3 currentWaypoint;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        currentWaypoint = waypoints[0]; //Sets Starting Waypoint
        StartCoroutine(WalkCycle()); //Starts WalkCycle
        animator.SetBool("idle", false);
        animator.SetBool("walking", true);
    }

    void Update()
    {
        //Rotates Enemy Sprite
        Vector2 moveDirection = rb.velocity;
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void AssignWaypoints()
    {
        /*
         * Assigns Waypoints to List
         * Assigned in the order they appear in the heirarchy
         * Waypoints are spawned with the name "WaypointX" To make sorting easier on the user i.e "Waypoint 1, Waypoint 2 ETC"
         * Rearange them in the heirarchy to represent this order
         */
        if(transform.GetChild(0) != null)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).tag == "Waypoints")
                {
                    waypoints.Add(transform.GetChild(i).transform.position);
                }
            }
        } else
        {
            Debug.Log("No Waypoints Found");
        }
    }

    public void ResetWaypointList()
    {
        waypoints.Clear(); //Clears the Current List
    }

    public void DestroyWaypoints()
    {
        /*
         * Used to destroy blank waypoint in the scene that are attached to the enemy GameObject
         * Is Currently Bugged, May need to click multiple times
         */
        for (int i = 0; i <= transform.childCount; i++)
        {
            if(transform.GetChild(i).tag == "Waypoinnts")
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }

    public void CreateWaypoint()
    {
        //Creates a new waypoint GameObject and spawns it as a child of this Enemy GameObject
        GameObject newWaypoint = new GameObject("waypoint" + transform.childCount.ToString());
        newWaypoint.tag = "Waypoints";
        newWaypoint.transform.position = transform.position;
        newWaypoint.transform.SetParent(gameObject.transform);
    }

    IEnumerator WalkCycle()
    {
        while (canWalk)
        {
            /*
             * This walkcycle Coroutine checks the enemy distance from the currently assigned waypoint
             * When its within a certain distance (0.2f in this case) waits a few seconds and switches to the next waypoint in the list
             */
            if (Vector3.Distance(transform.position, currentWaypoint) < waypointMinDistance)
            {
                animator.SetBool("idle", true);
                animator.SetBool("walking", false);
                rb.velocity = new Vector2(0, 0);
                tempIndex = waypoints.IndexOf(currentWaypoint);
               if (tempIndex != waypoints.Count -1 || tempIndex == 0)
                {
                    currentWaypoint = waypoints[(tempIndex + 1)];
                } else
                {
                    currentWaypoint = waypoints[(tempIndex - 1)];
                }
                yield return new WaitForSeconds(waypointDelay);
                animator.SetBool("idle", false);
                animator.SetBool("walking", true);
            } else
            {
                rb.velocity = Vector3.Normalize((currentWaypoint - transform.position)) * movementSpeed;
            }

            yield return null;
        }
    }
}
