using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transmitter : MonoBehaviour {
    public static Map map;

    [Header("Transmitter Prefabs")]
    public GameObject transmitter;
    public GameObject nonTransmitter;
    public GameObject sparkPrefab;
    public GameObject endPointPrefab;
    public GameObject endPointParticle;
    public LayerMask transmitterMask;

    [Header("Transmitter Audio")]
    public AudioClip connectSound;
    public AudioClip disconnect;
    public AudioClip rotation;

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

        if (startPoint && endPoint)
            Debug.LogError("MAX STOP IT! An end point cannot be a start point!");

        GenerateTransmitter();
        CheckConnections();

        transmitterController = FindObjectOfType<TransmitterController>();
        transmitterController.AddTransmitter(this);
        DrawConnections();
    }

    public virtual void HandleClick(int direction)
    {
        if (transmitterController.AnyMoving)
            return;

        if(spark != null)
            spark.ConnectionBroken();
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
            if(pieces[i].transform.childCount > 0)
                pieces[i].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (startPoint)
        {
            Spark spark = Instantiate(sparkPrefab, transform.position, Quaternion.identity).GetComponent<Spark>();
            spark.GiveTransmitter(this);
        }

        if (endPoint)
        {
            // Spawn the end point thing here
            if(endPointPrefab != null)
                Instantiate(endPointPrefab, transform.position, endPointParticle.transform.rotation);

            if (endPointParticle != null)
                Instantiate(endPointParticle, transform.position, endPointParticle.transform.rotation);
        }
            
    }

    public void DrawConnections()
    {
        if (currentConnections.north)
        {
            if(pieces[0].transform.childCount > 0 && pieces[0].transform.GetChild(0).gameObject.activeInHierarchy == false)
            {
                pieces[0].transform.GetChild(0).gameObject.SetActive(true);

                if (connectSound != null)
                    AudioSource.PlayClipAtPoint(connectSound, transform.position);
            }
        }
        else 
        {
            if (pieces[0].transform.childCount > 0 && pieces[0].transform.GetChild(0).gameObject.activeInHierarchy)
                pieces[0].transform.GetChild(0).gameObject.SetActive(false);
        }


        if (currentConnections.east)
        {
            if (pieces[1].transform.childCount > 0 && pieces[1].transform.GetChild(0).gameObject.activeInHierarchy == false)
            {
                pieces[1].transform.GetChild(0).gameObject.SetActive(true);

                if (connectSound != null)
                    AudioSource.PlayClipAtPoint(connectSound, transform.position);
            }
        }
        else
        {
            if (pieces[1].transform.childCount > 0 && pieces[1].transform.GetChild(0).gameObject.activeInHierarchy)
                pieces[1].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (currentConnections.south)
        {
            if (pieces[2].transform.childCount > 0 && pieces[2].transform.GetChild(0).gameObject.activeInHierarchy == false)
            {
                pieces[2].transform.GetChild(0).gameObject.SetActive(true);

                if(connectSound != null)
                    AudioSource.PlayClipAtPoint(connectSound, transform.position);
            }
        }
        else
        {
            if (pieces[2].transform.childCount > 0 && pieces[2].transform.GetChild(0).gameObject.activeInHierarchy)
                pieces[2].transform.GetChild(0).gameObject.SetActive(false);
        }

        if (currentConnections.west)
        {
            if (pieces[3].transform.childCount > 0 && pieces[3].transform.GetChild(0).gameObject.activeInHierarchy == false)
            {
                pieces[3].transform.GetChild(0).gameObject.SetActive(true);

                if (connectSound != null)
                    AudioSource.PlayClipAtPoint(connectSound, transform.position);
            }
        }
        else
        {
            if (pieces[3].transform.childCount > 0 && pieces[3].transform.GetChild(0).gameObject.activeInHierarchy)
                pieces[3].transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void DeactivateConnection(Direction direction)
    {
        switch (direction)
        {
            case Direction.NORTH:
                if (pieces[0].transform.childCount > 0)
                    pieces[0].transform.GetChild(0).gameObject.SetActive(false);

                break;
            case Direction.EAST:
                if (pieces[1].transform.childCount > 0)
                    pieces[1].transform.GetChild(0).gameObject.SetActive(false);

                break;
            case Direction.SOUTH:
                if (pieces[2].transform.childCount > 0)
                    pieces[2].transform.GetChild(0).gameObject.SetActive(false);

                break;
            case Direction.WEST:
                if (pieces[3].transform.childCount > 0)
                    pieces[3].transform.GetChild(0).gameObject.SetActive(false);

                break;
            default:
                break;
        }
    }

    public void DeactivateConnection(Transform parent)
    {
        parent.GetChild(0).gameObject.SetActive(false);
    }

    protected void DeactivateConnections()
    {
        if (connections.north)
        {
            DeactivateConnection(Direction.NORTH);

            RaycastHit hit;
            Ray ray = new Ray(pieces[0].transform.position + pieces[0].transform.forward, pieces[0].transform.forward);

            if(Physics.Raycast(ray, out hit, map.nodeSpacing, transmitterMask))
            {
                hit.transform.GetComponentInParent<Transmitter>().DeactivateConnection(hit.transform);
            }
        }

        if (connections.east)
        {
            DeactivateConnection(Direction.EAST);

            RaycastHit hit;
            Ray ray = new Ray(pieces[1].transform.position + pieces[1].transform.forward, pieces[1].transform.forward);

            if (Physics.Raycast(ray, out hit, map.nodeSpacing, transmitterMask))
            {
                hit.transform.GetComponentInParent<Transmitter>().DeactivateConnection(hit.transform);
            }
        }

        if (connections.south)
        {
            DeactivateConnection(Direction.SOUTH);

            RaycastHit hit;
            Ray ray = new Ray(pieces[2].transform.position + pieces[2].transform.forward, pieces[2].transform.forward);

            if (Physics.Raycast(ray, out hit, map.nodeSpacing, transmitterMask))
            {
                hit.transform.GetComponentInParent<Transmitter>().DeactivateConnection(hit.transform);
            }
        }

        if (connections.west)
        {
            DeactivateConnection(Direction.WEST);

            RaycastHit hit;
            Ray ray = new Ray(pieces[3].transform.position + pieces[3].transform.forward, pieces[3].transform.forward);

            if (Physics.Raycast(ray, out hit, map.nodeSpacing, transmitterMask))
            {
                hit.transform.GetComponentInParent<Transmitter>().DeactivateConnection(hit.transform);
            }
        }
    }
}
