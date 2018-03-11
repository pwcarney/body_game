using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : MonoBehaviour
{
    bool oxygenated;

    public Sprite empty_sprite;
    public Sprite full_sprite;

    bool Oxygenated
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
        }
    }
}
