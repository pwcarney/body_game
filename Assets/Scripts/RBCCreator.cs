using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBCCreator : MonoBehaviour
{
    GameObject rbc_prefab;

    float creation_cooldown = 1f;
    float last_creation = 0f;
    int pool = 5;

    void Start()
    {
        rbc_prefab = Resources.Load("Blood") as GameObject;
    }

    void Update()
    {
		if (Time.timeSinceLevelLoad > last_creation + creation_cooldown && 
            pool > 0 &&
            GetComponent<BloodNetwork>().Connections.Count > 1)
        {
            pool--;
            last_creation = Time.timeSinceLevelLoad;

            GameObject new_rbc = Instantiate(rbc_prefab, transform.position, Quaternion.identity);
            new_rbc.GetComponent<Blood>().SetLocation(gameObject);
        }
	}
}
