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

    void Start()
    {
        ChangeColor();
    }

    void FixedUpdate()
    {
        if (Time.timeSinceLevelLoad > last_decay_time + health_decay_time && !GameOver.IsGameOver)
        {
            health = Mathf.Clamp01(health - 0.005f);
            last_decay_time = Time.timeSinceLevelLoad;

            if (health == 0)
            {
                GameOver.EndGame(gameObject);
            }
        }

        ChangeColor();
    }

    void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.Lerp(healthy_color, unhealthy_color, 1 - health); 
    }

    public void ReceiveBlood()
    {
        health = 1;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Blood>() != null)
        {
            if (collision.gameObject.GetComponent<Blood>().Oxygenation > 0 && collision.gameObject.GetComponent<Blood>().CanOxygenate(gameObject))
            {
                ReceiveBlood();
                collision.gameObject.GetComponent<Blood>().FindNextLocation(gameObject);
            }
        }
    }
}
