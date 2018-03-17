using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    float spawn_rate = 5f;
    float last_spawn = 0f;

    int total_cells;

    GameObject cell_prefab;

    public int TotalCells
    {
        get
        {
            return total_cells;
        }
    }    

	void Start ()
    {
        cell_prefab = Resources.Load("Cell") as GameObject;
	}
	
	void Update ()
    {
		if (Time.timeSinceLevelLoad > last_spawn + spawn_rate && !GameOver.IsGameOver)
        {
            SpawnNewCell();
            last_spawn = Time.timeSinceLevelLoad;
        }
	}

    void SpawnNewCell()
    {
        Vector3 spawn_position = new Vector3();
        int spawn_tries = 0;
        while (spawn_tries < 5)
        {
            GameObject body_part = transform.parent.GetChild(Random.Range(1, 8)).gameObject;

            Vector3 extents = body_part.GetComponent<Renderer>().bounds.extents;
            spawn_position = new Vector3(
                Random.Range(-extents.x + 0.2f, extents.x - 0.2f),
                Random.Range(-extents.y + 0.2f, extents.y - 0.2f),
                -0.01f) +
                body_part.transform.position;

            Collider2D[] test_location_colliders = Physics2D.OverlapCircleAll(spawn_position, 0.1f);
            if (test_location_colliders.Length == 1)
            {
                total_cells++;
                Instantiate(cell_prefab, spawn_position, Quaternion.identity);
                break;
            }
            else
            {
                spawn_tries++;
                Debug.LogWarning("Cell location blocked, trying again");
            }
        }
    }
}
