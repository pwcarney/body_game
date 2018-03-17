using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public int oxygenation = 0;
    int max_oxygenation = 3;

    GameObject current_location;
    GameObject next_destination;
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
        FindTargetFrom(other);

        if (current_destination == null)
        {
            current_destination = next_destination;
        }
    }

    void FindTargetFrom(GameObject other)
    {
        if (oxygenation == 0)
        {
            foreach (GameObject connection in other.GetComponent<BloodNetwork>().Connections)
            {
                if (connection.GetComponent<Oxygenator>() != null)
                {
                    next_destination = connection;
                    return;
                }
            }
        }

        next_destination =
            other.GetComponent<BloodNetwork>().Connections[Random.Range(1, other.GetComponent<BloodNetwork>().Connections.Count)];
    }

    void FixedUpdate()
    {
        if (!GameOver.IsGameOver)
        {
            if (Vector3.Distance(transform.position, current_destination.transform.position) < 0.1f)
            {
                current_destination = next_destination;
            }

            transform.position = Vector3.MoveTowards(transform.position, current_destination.transform.position, blood_speed * Time.deltaTime);
        }
    }

    public bool CanOxygenate(GameObject requester)
    {
        if (requester == current_destination)
        {
            Oxygenation--;
            return true;
        }
        else
        {
            return false;
        }
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
