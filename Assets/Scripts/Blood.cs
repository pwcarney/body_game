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

    public void SetLocation(GameObject other)
    {
        current_location = other;
        FindTarget();
    }

    void FindTarget()
    {
        foreach (GameObject connected in current_location.GetComponent<BloodNetwork>().Connections)
        {
            if (oxygenation < max_oxygenation && connected.GetComponent<Oxygenator>() != null)
            {
                current_destination = connected;
                break;
            }
            else if (oxygenation > 0 && connected.GetComponent<Cell>() != null)
            {
                current_destination = connected;
                break;
            }
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
            GetComponent<SpriteRenderer>().color = Color.Lerp(empty_color, full_color, oxygenation / (float)max_oxygenation);

            oxygenation = value;

            if (oxygenation == 0 || oxygenation == max_oxygenation)
            {
                FindTarget();
            }
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
