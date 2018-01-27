using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {
    public float timeBetweenMoves = 1f;
    public float moveTime = 1f;

    private float count;
    private bool shouldCheck;
    private bool isMoving;

    private TransmitterController transmitterController;
    private Transmitter currentTransmitter;
    private void Awake()
    {
        transmitterController = FindObjectOfType<TransmitterController>();
        currentTransmitter = transmitterController.GetTransmitter(transform.position);
    }

    private void Update()
    {
        CheckMove();
        PerformMove();
    }
    
    private void CheckMove()
    {
        if (isMoving)
            return;

        count += Time.deltaTime;

        if(count >= timeBetweenMoves)
        {
            count = 0f;
            shouldCheck = true;
        }
    }

    private void PerformMove()
    {
        if (shouldCheck == false)
            return;

        
    }

    private IEnumerator Move()
    {
        float timer = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = currentTransmitter.transform.position;

        while(timer <= moveTime)
        {
            timer += Time.deltaTime;

            //transform.position = Vector3.Lerp();

            yield return null;
        }
    }
}
