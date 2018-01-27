using UnityEngine;

public class Node {
    public Vector3 Position { get { return position;} }
    public bool Occupied { get { return occupied; } }

    private Vector3 position;
    private bool occupied;

    public Node(Vector3 position)
    {
        this.position = position;
    }

    public void UpdateOccupied(bool occupied)
    {
        this.occupied = occupied;
    }
}
