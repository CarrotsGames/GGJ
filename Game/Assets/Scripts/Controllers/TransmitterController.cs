using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterController : MonoBehaviour {
    public bool AnyMoving { get { return transmitters.Exists(t => t.IsMoving); } }

    private List<Transmitter> transmitters;

    public void AddTransmitter(Transmitter transmitter)
    {
        if (transmitters == null)
            transmitters = new List<Transmitter>();

        transmitters.Add(transmitter);
    }

    public void UpdateConnections()
    {
        if (transmitters.IsNullOrEmpty())
            return;

        for (int i = 0; i < transmitters.Count; i++)
        {
            transmitters[i].CheckConnections();
        }
    }

    public Transmitter GetTransmitter(Vector3 position)
    {
        return transmitters.Find(t => t.transform.position == position);
    }
}
