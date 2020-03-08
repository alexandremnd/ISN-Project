using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DayCycler))]
public class DayCyclerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DayCycler cycler = (DayCycler)target;

        if (GUILayout.Button("Set time"))
        {
            cycler.SetTime();
        }
    }
}
