using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour {
    public static Map map;

    [Header("Transmitter Options")]
    public Connections connections; 

	void Awake () {
        if (map == null)
            map = FindObjectOfType<Map>();
	}

    private void Update()
    {

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

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, Vector3.forward * 4);
    }
}
