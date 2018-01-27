using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour {
    public static Map map;

    [Header("Transmitter Prefabs")]
    public GameObject transmitter;
    public GameObject nonTransmitter;
    public GameObject sparkPrefab;
    public LayerMask transmitterMask;
    public float skinWidth = 0.05f;

    [Header("Transmitter Options")]
    public Connections connections;
    public bool startPoint;
    public bool endPoint;

    public bool IsMoving { get { return isMoving; } }

    protected bool isMoving;
    protected Spark spark;

    public bool HasConnections
    {
        get
        {
            if (currentConnections.north)
                return true;
            if (currentConnections.east)
                return true;
            if (currentConnections.south)
                return true;
            if (currentConnections.west)
                return true;
            
                return false;
        }
    }

    protected TransmitterController transmitterController;

    private TransmitterGeneration generation;
    private Connections currentConnections;
    

    // These lists needs to follow the following pattern
    // North, East, South, West, transmitter generation will do this
    private GameObject[] pieces;

    void Awake () {
        if (map == null)
            map = FindObjectOfType<Map>();

        GenerateTransmitter();
        CheckConnections();

        transmitterController = FindObjectOfType<TransmitterController>();
        transmitterController.AddTransmitter(this);
    }

    public virtual void HandleClick(int direction)
    {
        if (transmitterController.AnyMoving)
            return;
    }

    public void GiveSpark(Spark spark)
    {
        this.spark = spark;
    }

    public void RemoveSpark()
    {
        spark = null;
    }

    public void CheckConnections()
    {
        if (currentConnections == null)
            currentConnections = new Connections();

        currentConnections.north = connections.north && HasConnection(pieces[0]);
        currentConnections.east = connections.east && HasConnection(pieces[1]);
        currentConnections.south = connections.south && HasConnection(pieces[2]);
        currentConnections.west = connections.west && HasConnection(pieces[3]);

        DrawConnections();
    }

    public List<GameObject> GetConnectedPieces()
    {
        List<GameObject> connected = new List<GameObject>();

        if (currentConnections.north)
            connected.Add(pieces[0]);
        if (currentConnections.east)
            connected.Add(pieces[1]);
        if (currentConnections.south)
            connected.Add(pieces[2]);
        if (currentConnections.west)
            connected.Add(pieces[3]);

        return connected;
    }

    private bool HasConnection(GameObject transmitter)
    {
        Vector3 rayOrigin = transmitter.transform.position + transmitter.transform.forward;
        Ray ray = new Ray(rayOrigin, transmitter.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, map.nodeSpacing - 1, transmitterMask))
            return true;
            

        return false;
    }

    private void GenerateTransmitter()
    {
        generation = gameObject.AddComponent<TransmitterGeneration>();
        pieces = generation.Generate(connections, transmitter, nonTransmitter, transform).ToArray();

        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces[i].GetComponent<LineRenderer>())
            {
                pieces[i].GetComponent<LineRenderer>().SetPosition(0, pieces[i].transform.position);
                pieces[i].GetComponent<LineRenderer>().enabled = false;
            }
        }

        if (startPoint)
        {
            Spark spark = Instantiate(sparkPrefab, transform.position, Quaternion.identity).GetComponent<Spark>();
            spark.GiveTransmitter(this);
        }
            
    }

    private void DrawConnections()
    {
        if (currentConnections.north)
        {
            pieces[0].GetComponent<LineRenderer>().enabled = true;
            pieces[0].GetComponent<LineRenderer>().SetPosition(1, pieces[0].transform.position + (pieces[0].transform.forward * map.nodeSpacing));
        }
        else
        {
            if(pieces[0].GetComponent<LineRenderer>())
                pieces[0].GetComponent<LineRenderer>().enabled = false;
        }
           

        if (currentConnections.east)
        {
            pieces[1].GetComponent<LineRenderer>().enabled = true;
            pieces[1].GetComponent<LineRenderer>().SetPosition(1, pieces[1].transform.position + (pieces[1].transform.forward * map.nodeSpacing));
        }
        else
        {
            if (pieces[1].GetComponent<LineRenderer>())
                pieces[1].GetComponent<LineRenderer>().enabled = false;
        }
            

        if (currentConnections.south)
        {
            pieces[2].GetComponent<LineRenderer>().enabled = true;
            pieces[2].GetComponent<LineRenderer>().SetPosition(1, pieces[2].transform.position + (pieces[2].transform.forward * map.nodeSpacing));
        }
        else
        {
            if (pieces[2].GetComponent<LineRenderer>())
                pieces[2].GetComponent<LineRenderer>().enabled = false;
        }

        if (currentConnections.west)
        {
            pieces[3].GetComponent<LineRenderer>().enabled = true;
            pieces[3].GetComponent<LineRenderer>().SetPosition(1, pieces[3].transform.position + (pieces[3].transform.forward * map.nodeSpacing));
        }
        else
        {
            if (pieces[3].GetComponent<LineRenderer>())
                pieces[3].GetComponent<LineRenderer>().enabled = false;
        }

    }
}
