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

	void Awake () {
        if (map == null)
            map = FindObjectOfType<Map>();

        generation = new TransmitterGeneration();
        generation.Generate(connections, transmitter, nonTransmitter, transform);
    }

    public virtual void HandleClick() { }

    protected void CheckConnections()
    {
        if (connections.north)
        {
            
        }

        if (connections.east)
        {

        }

        if (connections.south)
        {

        }

        if (connections.west)
        {

        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
            return;

        Gizmos.color = gizmoColour;
        Gizmos.DrawCube(transform.position, Vector3.one);
    }
}
