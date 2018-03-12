using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    bool oxygenated;

    GameObject current_location;
    GameObject current_destination;
    float blood_speed = 2f;

    public Sprite empty_sprite;
    public Sprite full_sprite;

    void Start()
    {
        Oxygenated = false;
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
            if (oxygenated && connected.GetComponent<Cell>() != null)
            {
                current_destination = connected;
                break;
            }
            else if (!oxygenated && connected.GetComponent<Oxygenator>() != null)
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

    public bool Oxygenated
    {
        get
        {
            return oxygenated;
        }

        set
        {
            if (value == true)
            {
                GetComponent<SpriteRenderer>().sprite = full_sprite;
            }
            else if (value == false)
            {
                GetComponent<SpriteRenderer>().sprite = empty_sprite;
            }
            oxygenated = value;

            FindTarget();
        }
    }
}
