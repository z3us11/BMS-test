using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Obstacles))]
public class ObstaclesEditor : Editor
{
    public static Object _target;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _target = target;
        EditorUtility.SetDirty(target);
    }
}
