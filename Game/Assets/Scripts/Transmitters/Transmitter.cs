using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour {
    public static Map map;

    [Header("Transmitter Prefabs")]
    public GameObject transmitter;
    public GameObject nonTransmitter;
    public LayerMask transmitterMask;

    [Header("Transmitter Options")]
    public Connections connections;
    public bool startPoint;
    public bool endPoint;
    public Color gizmoColour;

    private TransmitterGeneration generation;
    private Connections currentConnections;

    // These lists needs to follow the following pattern
    // North, East, South, West, transmitter generation will do this
    private List<GameObject> pieces;

    void Awake () {
        if (map == null)
            map = FindObjectOfType<Map>();

        GenerateTransmitter();
    }

    public virtual void HandleClick() { }

    protected void CheckConnections()
    {
        if(connections.north)
    }



    private void GenerateTransmitter()
    {
        generation = gameObject.AddComponent<TransmitterGeneration>();
        pieces = generation.Generate(connections, transmitter, nonTransmitter, transform);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
            return;

        Gizmos.color = gizmoColour;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
