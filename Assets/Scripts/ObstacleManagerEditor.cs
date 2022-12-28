using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstacleManager))]
public class ObstacleManagerEditor : Editor
{
    ObstacleManager obstacleManager;
    private void OnEnable()
    {
        obstacleManager = (ObstacleManager)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Obstacles");

            for (int i = 0; i < obstacleManager.cubeArray.GetLength(0); i++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int j = 0; j < obstacleManager.cubeArray.GetLength(1); j++)
                {
                    obstacleManager.toggles[i, j] = EditorGUILayout.Toggle(obstacleManager.toggles[i, j]);
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.Space(20);

            if (GUILayout.Button("Save Obstacles"))
            {
                obstacleManager.SaveObstacles();
                EditorUtility.SetDirty(obstacleManager.obstacleScriptableObj);
            }
        }

        if (GUILayout.Button("Reset Obstacles"))
        {
            obstacleManager.ResetObstacles();
            EditorUtility.SetDirty(obstacleManager.obstacleScriptableObj);
        }
    }
}
