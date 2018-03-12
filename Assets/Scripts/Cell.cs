using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Color healthy_color;
    public Color unhealthy_color;

    public float health = 1;
    float health_decay_time = 0.5f;
    float last_decay_time = 0f;

    float unload_speed = 1f;
    float last_unload = 0f;

	void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > last_decay_time + health_decay_time)
        {
            health = Mathf.Clamp01(health - 0.025f);
            last_decay_time = Time.timeSinceLevelLoad;
        }

        GetComponent<SpriteRenderer>().color = Color.Lerp(healthy_color, unhealthy_color, 1 - health);
    }

    public void ReceiveBlood()
    {
        health += 0.05f;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Blood>() != null)
        {
            if (collision.gameObject.GetComponent<Blood>().Oxygenation > 0 &&
                Time.timeSinceLevelLoad > last_unload + unload_speed) 
            {
                collision.gameObject.GetComponent<Blood>().Oxygenation--;
                last_unload = Time.timeSinceLevelLoad;

                ReceiveBlood();
            }
        }
    }
}
