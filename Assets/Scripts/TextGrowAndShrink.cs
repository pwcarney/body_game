using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextGrowAndShrink : MonoBehaviour
{
    float scale = 1;
    float min_scale = 0.9f;
    float max_scale = 1.1f;
    float grow_speed = 0.02f;
    bool growing = true;

    void FixedUpdate()
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

        Vector3 heart_scale = GetComponent<RectTransform>().localScale;

        heart_scale.x = scale;
        heart_scale.y = scale;

        GetComponent<RectTransform>().localScale = heart_scale;
    }
}
