using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Color healthy_color;
    public Color unhealthy_color;

    float health = 1;
    float health_decay_time = 0.5f;
    float last_decay_time = 0f;

	void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > last_decay_time + health_decay_time)
        {
            health = Mathf.Clamp01(health - 0.05f);
            last_decay_time = Time.timeSinceLevelLoad;
        }

        GetComponent<SpriteRenderer>().color = Color.Lerp(healthy_color, unhealthy_color, 1 - health);
    }
}
