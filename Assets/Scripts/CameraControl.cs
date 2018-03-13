using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    float mouse_speed = 0.003f;

    float game_over_zoom = 0f;
    Vector3 game_over_offset = new Vector3(0f, 0.5f);

	void Update()
    {
        // Game Over zoom
        if (GameOver.IsGameOver)
        {
            Vector3 new_position = Vector3.Lerp(transform.position, GameOver.DeadCell.transform.position + game_over_offset, game_over_zoom);
            game_over_zoom += 0.01f;
            new_position.z = -10;
            transform.position = new_position;

            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 0.1f, 2);
            return;
        }

        // Move
        if (Input.GetMouseButton(1) && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
        {
            Vector3 current_camera_pos = Camera.main.transform.position;
            current_camera_pos.x = Mathf.Clamp(current_camera_pos.x - Input.GetAxis("Horizontal") * mouse_speed * Camera.main.orthographicSize, -7.25f, 7.25f);
            current_camera_pos.y = Mathf.Clamp(current_camera_pos.y - Input.GetAxis("Vertical") * mouse_speed * Camera.main.orthographicSize, -8.25f, 4.25f);
            Camera.main.transform.position = current_camera_pos;
        }

        // Zoom in/out
        if (Input.GetAxis("Mouse ScrollWheel") < 0) 
        {
            Camera.main.orthographicSize = Mathf.Min(Camera.main.orthographicSize + 1, 10);
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        {
            Camera.main.orthographicSize = Mathf.Max(Camera.main.orthographicSize - 1, 2);
        }
    }
}
