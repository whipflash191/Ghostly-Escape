﻿/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using UnityEngine;
using UnityEditor;

public class GhostGeneratorWindow : EditorWindow
{
    int ghostLayer = 0;
    string ghostName = "Replace This";
    Sprite ghostSprite = null;
    bool isPlayer = false;
    bool isPrefab = false;
    bool canMake = true;
    float mvmSpeed = 4f;
    Vector3 ghostScale = new Vector3(1,1,1);

    [MenuItem("Window/Ghost Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<GhostGeneratorWindow>("Ghost Generator");
    }

    /*
     * This chunk of code defines the editor layout
     */

    private void OnGUI()
    {
        ghostName = EditorGUILayout.TextField("Ghost Name", ghostName, EditorStyles.textField);
        DuplicateCheck();
        ghostSprite = (Sprite)EditorGUILayout.ObjectField(ghostSprite, typeof(Sprite), false);
        ghostScale = EditorGUILayout.Vector3Field("Object Scale", ghostScale);
        ghostLayer = EditorGUILayout.LayerField("Object Layer", ghostLayer);
        isPrefab = EditorGUILayout.Toggle("Make Prefab", isPrefab, EditorStyles.toggle);
        isPlayer = EditorGUILayout.Toggle("Is Player", isPlayer, EditorStyles.toggle);
        if(isPlayer)
        {
            EditorGUILayout.BeginFadeGroup(1);
            EditorGUI.indentLevel++;
            mvmSpeed = EditorGUILayout.Slider("Movement Speed", mvmSpeed, 0, 4);
            EditorGUI.indentLevel--;
        } else
        {
            EditorGUILayout.EndFadeGroup();
        }
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(canMake == false);
        if(GUILayout.Button("Make Ghost"))
        {
            MakeGhost();
        }
        EditorGUI.EndDisabledGroup();
        if(GUILayout.Button("Reset"))
        {
            ResetFields();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.LabelField("To set the spawn position create an empty game object and select it in the editor. If no object is selected Ghost will spawn at 0, 0, 0,", EditorStyles.boldLabel);
        EditorStyles.boldLabel.wordWrap = true;
    }

    private void MakeGhost()
    {
        /*
         * Function to make the Ghost Object or Prefab
         * Adds compnents required & player scripts needed
         * Adds to player layer or sprite layer
        */
        GameObject newGhost = new GameObject(ghostName);
        newGhost.transform.localScale = ghostScale;
        if(Selection.activeGameObject != null)
        {
            newGhost.transform.position = Selection.activeGameObject.transform.position;
            GameObject.DestroyImmediate(Selection.activeGameObject);
        }
        newGhost.layer = ghostLayer;
        newGhost.AddComponent<SpriteRenderer>();
        newGhost.GetComponent<SpriteRenderer>().sprite = ghostSprite;
        newGhost.AddComponent<PolygonCollider2D>();
        if(isPlayer)
        {
            newGhost.AddComponent<PlayerControl>();
            newGhost.GetComponent<PlayerControl>().movementSpeed = mvmSpeed;
        }
        if(isPrefab)
        {
            PrefabUtility.CreatePrefab("Assets/Prefabs/Ghosts/" + ghostName + ".prefab", newGhost);
            GameObject.DestroyImmediate(newGhost);
        }
        ResetFields();
    }

    private void DuplicateCheck()
    {
        if (GameObject.Find(ghostName) != null)
        {
            EditorGUILayout.LabelField("Name already exists in scene!", EditorStyles.boldLabel);
            canMake = false;
        }
        else if (AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Ghosts/" + ghostName + ".prefab", typeof(GameObject)) != null)
        {
            EditorGUILayout.LabelField("A Prefab with this name already exists!", EditorStyles.boldLabel);
            canMake = false;
        }
        else if (ghostName == "Replace This" || ghostName == "")
        {
            canMake = false;
        }
        else
        {
            canMake = true;
        }
    }

    private void ResetFields()
    {
        ghostName = "Replace This";
        ghostLayer = 0;
        ghostSprite = null;
        ghostScale = new Vector3(1, 1, 1);
        isPlayer = false;
        isPrefab = false;
        mvmSpeed = 4f;
    }
}
