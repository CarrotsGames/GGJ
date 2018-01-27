using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[ExecuteInEditMode]
public class Map : MonoBehaviour {
    [Header("Map Size")]
    [Range(3, 15)]
    public int width = 3;
    [Range(3, 15)]
    public int height = 3;
    [Range(1, 5)]
    public int nodeSpacing = 1;

    public List<Node> nodes;
    private int prevSpaceBetweenNodes;

    private void OnEnable()
    {
       // BuildNodes();
    }

    public void BuildNodes()
    {
        if (nodes == null)
            nodes = new List<Node>();
        else
            nodes.Clear();

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Node node = new Node(new Vector3(x * nodeSpacing, 0, z * nodeSpacing));
                

                nodes.Add(node);
            }
        }
    }

    public Node GetNodeAtPosition(Vector3 position)
    {
        return nodes.Find(n => n.Position == position);
    }

    public Node GetNearestNode(Vector3 position)
    {
        if (nodes.IsNullOrEmpty())
            BuildNodes();

        float dist = float.MaxValue;
        Node nearestNode = null;
        for (int i = nodes.Count -1; i >= 0; i--)
        {
            float currentDist = Vector3.Distance(nodes[i].Position, position);

            if(currentDist < dist && nodes[i].Occupied == false)
            {
                nearestNode = nodes[i];
                dist = currentDist;
            }
        }

        return nearestNode;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < nodes.Count; i++)
        {
            Gizmos.DrawSphere(nodes[i].Position, .1f);
        }
    }

    private void OnValidate()
    {
        if (nodes.IsNullOrEmpty() || nodes.Count != width * height || prevSpaceBetweenNodes != nodeSpacing)
        {
            BuildNodes();
            prevSpaceBetweenNodes = nodeSpacing;
        } 
    }
}
