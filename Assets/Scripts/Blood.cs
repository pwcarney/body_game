using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    public int oxygenation = 0;
    int max_oxygenation = 3;

    GameObject current_destination;

    float blood_speed = 2f;

    public Color empty_color;
    public Color full_color;

    void Start()
    {
        Oxygenation = 0;
    }

    public void SetBirthLocation(GameObject mother)
    {
        current_destination = mother;
    }

    void FixedUpdate()
    {
        if (!GameOver.IsGameOver)
        {
            if (Vector3.Distance(transform.position, current_destination.transform.position) < 0.1f)
            {
                current_destination = FindNextDestination();
            }

            transform.position = Vector3.MoveTowards(transform.position, current_destination.transform.position, blood_speed * Time.deltaTime);
        }
    }

    GameObject FindNextDestination()
    {
        if (Oxygenation == 0)
        {
            foreach (GameObject connection in current_destination.GetComponent<BloodNetwork>().Connections)
            {
                if (connection.GetComponent<Oxygenator>() != null)
                {
                    return connection;
                }
            }
        }
        else if (Oxygenation > 0)
        {
            List<GameObject> valid_options = new List<GameObject>();
            List<GameObject> all_options = current_destination.GetComponent<BloodNetwork>().Connections;
            for (int i = 1; i < all_options.Count; i++)
            {
                if (all_options[i].GetComponent<Oxygenator>() == null)
                {
                    valid_options.Add(all_options[i]);
                }
            }
            if (valid_options.Count == 0)
            {
                return DefaultDestinationFind();
            }

            return valid_options[Random.Range(0, valid_options.Count)];
        }
        else
        {
            Debug.LogError("Negative oxygenation???");
        }

        return DefaultDestinationFind();
    }

    GameObject DefaultDestinationFind()
    {
        return current_destination.GetComponent<BloodNetwork>().Connections[Random.Range(1, current_destination.GetComponent<BloodNetwork>().Connections.Count)];
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
