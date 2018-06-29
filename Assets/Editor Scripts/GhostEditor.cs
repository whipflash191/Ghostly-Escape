﻿/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GhostGameObject))]
public class GhostEditor : Editor {

    public override void OnInspectorGUI()
    {
        GhostGameObject ghostGameObject = (GhostGameObject)target;
        base.OnInspectorGUI();
        if (ghostGameObject.ghost.isPlayer == true)
        {
            if (GUILayout.Button("Make Player"))
            {
                ghostGameObject.MakePlayer();
            }
        } 
    }
}
