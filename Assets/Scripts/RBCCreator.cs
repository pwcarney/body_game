using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBCCreator : MonoBehaviour
{
    GameObject rbc_prefab;

    float creation_cooldown = 2f;
    float last_creation = 0f;
    int rbc_created = 0;

    CellSpawner spawner;

    void Start()
    {
        rbc_prefab = Resources.Load("Blood") as GameObject;

        spawner = FindObjectOfType<CellSpawner>();
    }

    void Update()
    {
		if (Time.timeSinceLevelLoad > last_creation + creation_cooldown &&
            rbc_created < spawner.TotalCells &&
            GetComponent<BloodNetwork>().Connections.Count > 1)
        {
            rbc_created++;
            last_creation = Time.timeSinceLevelLoad;

            GameObject new_rbc = Instantiate(rbc_prefab, transform.position, Quaternion.identity);
            new_rbc.GetComponent<Blood>().FindNextLocation(gameObject);
        }
	}
}
