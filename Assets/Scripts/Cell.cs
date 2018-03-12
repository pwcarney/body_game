using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Color healthy_color;
    public Color unhealthy_color;

    float health = 1;
    float health_decay_time = 0.25f;
    float last_decay_time = 0f;
    float need_dist_multiplier;

    float receive_cooldown = 1;
    float last_receive = 0;

    void Start()
    {
        need_dist_multiplier = Vector3.Distance(transform.position, FindObjectOfType<Oxygenator>().transform.position);
    }

	void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > last_decay_time + health_decay_time)
        {
            health = Mathf.Clamp01(health - 0.0001f);
            last_decay_time = Time.timeSinceLevelLoad;
        }

        GetComponent<SpriteRenderer>().color = Color.Lerp(healthy_color, unhealthy_color, 1 - health);
    }

    public void ReceiveBlood()
    {
        health += 0.05f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Blood>() != null)
        {
            if (collision.gameObject.GetComponent<Blood>().Oxygenation > 0)
            {
                last_receive = Time.timeSinceLevelLoad;

                collision.gameObject.GetComponent<Blood>().Oxygenation--;

                ReceiveBlood();
            }

            collision.gameObject.GetComponent<Blood>().FindNextLocation(gameObject);
        }
    }
}
