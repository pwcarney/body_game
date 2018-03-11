using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VeinDrawable : MonoBehaviour
{
    GameObject new_vein;
    GameObject vein_prefab;

    bool dragging_me;
    bool mouse_inside_me;

    List<GameObject> connections = new List<GameObject>();

    public bool MouseInsideMe
    {
        get
        {
            return mouse_inside_me;
        }
    }

    void Start()
    {
        vein_prefab = Resources.Load("Vein") as GameObject;
    }

    public void Strech()
    {
        Vector3 initial_position = transform.position;
        Vector3 final_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        final_position.z = 0;

        Vector3 centerPos = (initial_position + final_position) / 2f;
        new_vein.transform.position = centerPos;

        Vector3 direction = final_position - initial_position;
        direction = Vector3.Normalize(direction);
        new_vein.transform.right = direction;

        Vector3 scale = new_vein.transform.localScale;
        scale.x = Vector3.Distance(initial_position, final_position) / 3.4f;
        new_vein.transform.localScale = scale;
    }

    void Update()
    {
        if (dragging_me)
        {
            Strech();
        }
    }

    void OnMouseDrag()
    {
        if (!dragging_me)
        {
            dragging_me = true;
            new_vein = Instantiate(vein_prefab);
        }
    }

    void OnMouseEnter()
    {
        mouse_inside_me = true;
    }

    void OnMouseExit()
    {
        mouse_inside_me = false;
    }

    void OnMouseUp()
    {
        if (dragging_me)
        {
            dragging_me = false;

            VeinDrawable[] vein_drawables = FindObjectsOfType<VeinDrawable>();
            foreach (VeinDrawable vein_drawable in vein_drawables)
            {
                if (vein_drawable == this)
                {
                    continue;
                }

                if (vein_drawable.MouseInsideMe && !connections.Contains(vein_drawable.gameObject))
                {
                    connections.Add(vein_drawable.gameObject);
                    return;
                }
            }

            Destroy(new_vein);
        }
    }
}
