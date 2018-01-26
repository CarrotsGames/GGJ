using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SnapToNode))]
public class SnapToNodeEditor : Editor {
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SnapToNode snapToNode = (SnapToNode)target;

        if (GUILayout.Button("Snap"))
            snapToNode.Snap();
    }
}
