﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmitterController : MonoBehaviour {
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
}