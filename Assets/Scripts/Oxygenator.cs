using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oxygenator : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Blood>() != null)
        {
            if (collision.gameObject.GetComponent<Blood>().Oxygenated == false)
            {
                collision.gameObject.GetComponent<Blood>().Oxygenated = true;
            }
        }
    }
}
