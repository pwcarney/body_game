using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygenator : MonoBehaviour
{
    float load_speed = 0.1f;
    float last_load = 0f;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Blood>() != null)
        {
            if (collision.gameObject.GetComponent<Blood>().Oxygenation < collision.gameObject.GetComponent<Blood>().MaxOxygenation &&
                Time.timeSinceLevelLoad > load_speed + last_load)
            {
                collision.gameObject.GetComponent<Blood>().Oxygenation++;
                last_load = Time.timeSinceLevelLoad;
            }
        }
    }
}
