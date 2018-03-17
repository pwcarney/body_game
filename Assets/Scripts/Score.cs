using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;

    float bounce_speed = 0.002f;

    bool stretch = false;
    float max_stretch = 1.3f;

    bool compress = false;
    float max_compress = 1f;

    public void Add()
    {
        score++;
        GetComponent<Text>().text = score.ToString();

        stretch = true;
    }

    void Update()
    {
        if (stretch)
        {
            Vector3 scale = GetComponent<RectTransform>().localScale;
            scale.y += bounce_speed;
            GetComponent<RectTransform>().localScale = scale;
            if (scale.y > max_stretch)
            {
                stretch = false;
                compress = true;
            }
        }
        else if (compress)
        {
            Vector3 scale = GetComponent<RectTransform>().localScale;
            scale.y -= bounce_speed;
            GetComponent<RectTransform>().localScale = scale;
            if (scale.y < max_compress)
            {
                compress = false;
            }
        }
    }
}
