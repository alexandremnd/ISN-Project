using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(FlexibleButton))]
public class FlexibleButtonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        FlexibleButton button = (FlexibleButton)target;

        if (GUILayout.Button("Update Script"))
        {
            button.Awake();
        }

        base.OnInspectorGUI();
    }
}
