using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SnapToNode : MonoBehaviour {
    public static Map map;

    private Node currentNode;

    private void OnEnable()
    {
        if (map == null)
            map = FindObjectOfType<Map>();
    }

    private bool PositionChanged()
    {
        return transform.position != currentNode.Position ? true : false;
    }

    public void Snap()
    {
        if (currentNode != null && transform.position == currentNode.Position)
            return;

        if(currentNode != null)
            currentNode.UpdateOccupied(false);

        Node node = map.GetNearestNode(transform.position);

        if (node == null)
        {
            Debug.LogError(string.Format("{0} could not find a valid node to snap to", transform.name));
            return;
        }

        currentNode = node;
        currentNode.UpdateOccupied(true);

        transform.position = currentNode.Position;
    }

    public void GiveNode(Node node)
    {
        currentNode = node;
    }

    private void OnDestroy()
    {
        currentNode.UpdateOccupied(false);
    }
}
