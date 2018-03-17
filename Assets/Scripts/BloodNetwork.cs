using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodNetwork : MonoBehaviour
{
    List<GameObject> connections = new List<GameObject>();

    void Start()
    {
        AddConnection(gameObject);
    }

    public void AddConnection(GameObject other)
    {
        connections.Add(other);
    }

    public List<GameObject> Connections
    {
        get
        {
            return connections;
        }
    }
}
