using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { NORTH, EAST, SOUTH, WEST }

public class TransmitterGeneration : MonoBehaviour {
    public List<GameObject> Generate(Connections connection, GameObject transmitter, GameObject noTransmiter, Transform parent)
    {
        List<GameObject> transmitters = new List<GameObject>();

        transmitters.Add(SpawnPiece(connection.north ? transmitter : noTransmiter, parent, GetRotation(Direction.NORTH)));
        transmitters.Add(SpawnPiece(connection.east ? transmitter : noTransmiter, parent, GetRotation(Direction.EAST)));
        transmitters.Add(SpawnPiece(connection.south ? transmitter : noTransmiter, parent, GetRotation(Direction.SOUTH)));
        transmitters.Add(SpawnPiece(connection.west ? transmitter : noTransmiter, parent, GetRotation(Direction.WEST)));

        return transmitters;
    }

    public GameObject SpawnPiece(GameObject piece, Transform parent, Vector3 rotation)
    {
        return Instantiate(piece, parent.position, Quaternion.Euler(rotation));
    }

    public Vector3 GetRotation(Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                return Vector3.zero;
            case Direction.EAST:
                return new Vector3(0, 90, 0);
            case Direction.SOUTH:
                return new Vector3(0, 180, 0);
            case Direction.WEST:
                return new Vector3(0, -90, 0);
            default:
                Debug.LogError("Direction out of bounds");
                return Vector3.zero;
        }
    }
}