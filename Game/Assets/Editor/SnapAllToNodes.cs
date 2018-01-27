using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SnapAllToNodes : EditorWindow {
    [MenuItem("Transmitters/SnapAllNodes %&s")]
    private static void SnapAllToNode()
    {
        SnapToNode[] nodes = FindObjectsOfType<SnapToNode>();

        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i].Snap();
        }
    }
}
