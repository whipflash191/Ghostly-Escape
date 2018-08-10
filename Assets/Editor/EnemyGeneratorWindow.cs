/*
* Copyright (c) Dylan Faith (Whipflash191)
* https://twitter.com/Whipflash191
*/

using UnityEngine;
using UnityEngine.AI;
using UnityEditor;
using UnityEngine.Audio;

public class EnemyGeneratorWindow : EditorWindow
{
    //Creates a Window used to Easily Generate new enimies
    //Variables Used in Generation
    //Edited through Editor Window (Window/Enemy Generator)
    GameObject enemyLight;
    AudioClip footstep;
    AudioMixerGroup footstepMixerGroup;
    string enemyTag = "Enemy";
    int enemyLayer = 0;
    int enemyWaypoints = 0;
    string enemyName = "Replace This";
    Sprite enemySprite = null;
    bool isPrefab = false;
    bool canMake = true;
    float mvmSpeed = 4f;
    Vector3 enemyScale = new Vector3(1, 1, 1);

    [MenuItem("Window/Enemy Generator")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EnemyGeneratorWindow>("Enemy Generator");
    }

    /*
    * This chunk of code defines the editor layout
    */

    private void OnGUI()
    {
        enemyName = EditorGUILayout.TextField("Enemy Name", enemyName, EditorStyles.textField);
        DuplicateCheck();
        enemySprite = (Sprite)EditorGUILayout.ObjectField(enemySprite, typeof(Sprite), false);
        enemyScale = EditorGUILayout.Vector3Field("Object Scale", enemyScale);
        enemyLayer = EditorGUILayout.LayerField("Object Layer", enemyLayer);
        enemyTag = EditorGUILayout.TagField("Object Tag", enemyTag);
        isPrefab = EditorGUILayout.Toggle("Make Prefab", isPrefab, EditorStyles.toggle);
        mvmSpeed = EditorGUILayout.Slider("Movement Speed", mvmSpeed, 0, 4);
        enemyWaypoints = EditorGUILayout.IntField("Waypoint to Generate", enemyWaypoints, EditorStyles.numberField);
        EditorGUILayout.LabelField("To set the spawn position create an empty game object and select it in the editor. If no object is selected Enemy will spawn at 0, 0, 0,", EditorStyles.boldLabel);
        EditorStyles.boldLabel.wordWrap = true;
        EditorGUILayout.BeginHorizontal();
        EditorGUI.BeginDisabledGroup(canMake == false);
        if (GUILayout.Button("Make Enemy"))
        {
            MakeEnemy();
        }
        EditorGUI.EndDisabledGroup();
        if (GUILayout.Button("Reset"))
        {
            ResetFields();
        }
        EditorGUILayout.EndHorizontal();

    }

    private void MakeEnemy()
    {
        /*
         * Function to make the Enemy Object or Prefab
         * Adds compnents required & player scripts needed
         * Adds waypoints used for the AI
         * Specifies Layer to add too
        */
        footstep = (AudioClip)AssetDatabase.LoadAssetAtPath("Assets/Audio/Footstep.wav", typeof(AudioClip));
        enemyLight = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Lights/EnemyLight.prefab", typeof(GameObject));
        AudioMixer tempMixer = (AudioMixer)AssetDatabase.LoadAssetAtPath("Assets/Mixer/Master.mixer", typeof(AudioMixer));
        footstepMixerGroup = tempMixer.FindMatchingGroups("Footsteps")[0];
        GameObject newEnemy = new GameObject(enemyName);
        newEnemy.transform.localScale = enemyScale;
        if (Selection.activeGameObject != null)
        {
            newEnemy.transform.position = Selection.activeGameObject.transform.position;
        }
        newEnemy.layer = enemyLayer;
        newEnemy.tag = enemyTag;
        newEnemy.AddComponent<SpriteRenderer>();
        newEnemy.GetComponent<SpriteRenderer>().sprite = enemySprite;
        newEnemy.AddComponent<PolygonCollider2D>();
        newEnemy.AddComponent<EnemyControl>();
        newEnemy.GetComponent<EnemyControl>().movementSpeed = mvmSpeed;
        newEnemy.AddComponent<Rigidbody2D>();
        newEnemy.GetComponent<EnemyControl>().rb = newEnemy.GetComponent<Rigidbody2D>();
        newEnemy.GetComponent<Rigidbody2D>().gravityScale = 0;
        newEnemy.GetComponent<Rigidbody2D>().freezeRotation = true;
        GameObject temp = (GameObject)PrefabUtility.InstantiatePrefab(enemyLight);
        temp.transform.position = newEnemy.transform.position;
        temp.transform.SetParent(newEnemy.transform);
        newEnemy.AddComponent<AudioSource>();
        newEnemy.GetComponent<AudioSource>().clip = footstep;
        newEnemy.GetComponent<AudioSource>().spatialBlend = 1.0f;
        newEnemy.GetComponent<AudioSource>().volume = 0.1f;
        newEnemy.GetComponent<AudioSource>().maxDistance = 3.52f;
        newEnemy.GetComponent<AudioSource>().loop = true;
        newEnemy.GetComponent<AudioSource>().rolloffMode = AudioRolloffMode.Linear;
        newEnemy.GetComponent<AudioSource>().outputAudioMixerGroup = footstepMixerGroup;
        for (int i = 0; i < enemyWaypoints; i++)
        {
            GameObject tempWaypoint = new GameObject("waypoint" + i.ToString());
            tempWaypoint.tag = "Waypoints";
            tempWaypoint.transform.position = newEnemy.transform.position;
            tempWaypoint.transform.SetParent(newEnemy.transform);
        }
        if (isPrefab)
        {
            PrefabUtility.CreatePrefab("Assets/Prefabs/Enemies/" + enemyName + ".prefab", newEnemy);
            GameObject.DestroyImmediate(newEnemy);
        }
        ResetFields();
    }

    private void DuplicateCheck()
    {
        if (GameObject.Find(enemyName) != null)
        {
            EditorGUILayout.LabelField("Name already exists in scene!", EditorStyles.boldLabel);
            canMake = false;
        }
        else if (AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Enemies/" + enemyName + ".prefab", typeof(GameObject)) != null)
        {
            EditorGUILayout.LabelField("A Prefab with this name already exists!", EditorStyles.boldLabel);
            canMake = false;
        }
        else if (enemyName == "Replace This" || enemyName == "")
        {
            canMake = false;
        }
        else
        {
            canMake = true;
        }
    }

    //Resets the fields in the Editor Window
    private void ResetFields()
    {
        enemyTag = "Enemy";
        enemyName = "Replace This";
        enemyWaypoints = 0;
        enemyScale = new Vector3(1, 1, 1);
        enemyLayer = 0;
        enemySprite = null;
        isPrefab = false;
        mvmSpeed = 4f;
    }
}
