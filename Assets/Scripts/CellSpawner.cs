using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpawner : MonoBehaviour
{
    float spawn_rate = 5f;
    float last_spawn = 0f;

    GameObject cell_prefab;

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
        GameObject body_part = transform.parent.GetChild(Random.Range(1, 8)).gameObject;

        Vector3 extents = body_part.GetComponent<Renderer>().bounds.extents;
        Vector3 spawn_position = new Vector3(
            Random.Range(-extents.x + 0.2f, extents.x - 0.2f),
            Random.Range(-extents.y + 0.2f, extents.y - 0.2f)) +
            body_part.transform.position;

        Instantiate(cell_prefab, spawn_position, Quaternion.identity);
    }
}
