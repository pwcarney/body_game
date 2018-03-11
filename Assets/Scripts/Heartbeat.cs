using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    float scale = 0.5f;
    float min_scale = 0.45f;
    float max_scale = 0.55f;
    float grow_speed = 0.005f;
    bool growing = true;

    void FixedUpdate ()
    {
        if (growing && scale < max_scale)
        {
            scale += grow_speed;
        }
        else if (!growing && scale > min_scale)
        {
            scale -= grow_speed;
        }

        if (growing && scale >= max_scale)
        {
            growing = false;
        }
        else if (!growing && scale <= min_scale)
        {
            growing = true;
        }

        Vector3 heart_scale = transform.localScale;

        heart_scale.x = scale;
        heart_scale.y = scale;

        transform.localScale = heart_scale;
    }
}
