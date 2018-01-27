using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {
    public float timeBetweenMoves = 1f;
    public float moveTime = 1f;
    public LayerMask transmitterMask;

    public bool IsMoving { get { return isMoving; } }
    public Transmitter CurrentTransmitter { get { return currentTransmitter; } }

    private float count;
    private bool shouldCheck;
    private bool isMoving;

    private Map map;

    private GameController gameController;
    private TransmitterController transmitterController;
    private Transmitter currentTransmitter;
    private Transmitter previousTransmitter;
    private void Awake()
    {
        map = FindObjectOfType<Map>();
        transmitterController = FindObjectOfType<TransmitterController>();
        gameController = FindObjectOfType<GameController>();
        gameController.UpdateSparkCount(1);
    }

    private void Update()
    {
        CheckMove();
        PerformMove();
    }

    public void GiveTransmitter(Transmitter transmitter)
    {
        currentTransmitter = transmitter;
        currentTransmitter.GiveSpark(this);
    }

    public void ConnectionBroken()
    {
        previousTransmitter = null;
    }
    
    private void CheckMove()
    {
        if (isMoving || shouldCheck)
            return;

        if (currentTransmitter.IsMoving && transform.position != currentTransmitter.transform.position)
            transform.position = currentTransmitter.transform.position;

        count += Time.deltaTime;

        if(count >= timeBetweenMoves)
        {
            count = 0f;
            shouldCheck = true;
        }
    }

    private void PerformMove()
    {
        if (shouldCheck == false || isMoving || currentTransmitter.IsMoving)
            return;

        if (currentTransmitter.HasConnections)
        {
            List<GameObject> connectedPieces = currentTransmitter.GetConnectedPieces();

            for (int i = 0; i < connectedPieces.Count; i++)
            {
                Vector3 rayOrigin = connectedPieces[i].transform.position + connectedPieces[i].transform.forward;
                Ray ray = new Ray(rayOrigin, connectedPieces[i].transform.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, map.nodeSpacing, transmitterMask))
                {
                    Transmitter t = hit.transform.GetComponentInParent<Transmitter>();

                    if (t == null || t == previousTransmitter)
                        continue;

                    StartCoroutine(Move(t));
                    shouldCheck = false;

                    currentTransmitter.RemoveSpark();
                    previousTransmitter = currentTransmitter;
                    currentTransmitter = t;
                    currentTransmitter.GiveSpark(this);
                    transmitterController.UpdateConnections();

                    return;
                }
            }
        }
    }

    private void CheckEndPoint()
    {
        if (currentTransmitter.endPoint == false)
            return;

        gameController.UpdateSparkCount(-1);
        currentTransmitter.RemoveSpark();
        Destroy(gameObject);
    }

    private IEnumerator Move(Transmitter newTransmitter)
    {
        isMoving = true;
        float timer = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = newTransmitter.transform.position;

        while(timer <= moveTime)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(startPos, endPos, timer / moveTime);

            yield return null;
        }

        transform.position = endPos;

        isMoving = false;

        CheckEndPoint();
    }
}
