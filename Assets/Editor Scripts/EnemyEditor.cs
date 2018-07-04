/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyControl))]
public class EnemyEditor : Editor
{
    bool canDestroy = false;
    bool haveSetWaypoints = false;

    public override void OnInspectorGUI()
    {
        EnemyControl enemy = (EnemyControl)target;
        base.OnInspectorGUI();
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Set Waypoints"))
        {
            enemy.AssignWaypoints();
            haveSetWaypoints = true;
        }
        if(GUILayout.Button("Clear Waypoint List"))
        {
            enemy.ResetWaypointList();
        }
        if(enemy.waypoints.Count != 0 && haveSetWaypoints == true)
        {
            canDestroy = true;
        } else
        {
            canDestroy = false;
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(canDestroy == false);
        if(GUILayout.Button("Destroy Waypoint Objects"))
        {
            enemy.DestroyWaypoints();
            haveSetWaypoints = false;
        }
        EditorGUI.EndDisabledGroup();
        if(GUILayout.Button("Create Waypoint Object"))
        {
            enemy.CreateWaypoint();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("When you have set the waypoint's position hit 'Destroy Waypoint Objects' to clear the empty objects from the scene", EditorStyles.boldLabel);
        EditorStyles.boldLabel.wordWrap = true;
    }
}
