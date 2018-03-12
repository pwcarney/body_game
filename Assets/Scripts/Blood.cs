using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public int oxygenation = 0;
    int max_oxygenation = 3;

    GameObject current_location;
    GameObject current_destination;

    float blood_speed = 2f;

    public Color empty_color;
    public Color full_color;

    void Start()
    {
        Oxygenation = 0;
    }

    public void FindNextLocation(GameObject other)
    {
        current_location = other;
        FindTarget();
    }

    void FindTarget()
    {
        if (oxygenation == 0)
        {
            foreach (GameObject connection in current_location.GetComponent<BloodNetwork>().Connections)
            {
                if (connection.GetComponent<Oxygenator>() != null)
                {
                    current_destination = connection;
                    return;
                }
            }
        }
        else
        {
            current_destination =
                current_location.GetComponent<BloodNetwork>().Connections[Random.Range(1, current_location.GetComponent<BloodNetwork>().Connections.Count)];
        }
    }

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, current_destination.transform.position, blood_speed * Time.deltaTime);
    }

    public int Oxygenation
    {
        get
        {
            return oxygenation;
        }

        set
        {
            oxygenation = value;

            GetComponent<SpriteRenderer>().color = Color.Lerp(empty_color, full_color, oxygenation / (float)max_oxygenation);
        }
    }

    public int MaxOxygenation
    {
        get
        {
            return max_oxygenation;
        }
    }
}
