using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast2D : MonoBehaviour
{
    Vector3 Mouseposition;
    public Camera camera;
    Transform CurrentTouch;
    void Start()
    {
        
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Mouseposition = camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(Mouseposition, transform.forward, 15f);

            if (hit.collider != null)
            {
                CurrentTouch = hit.transform;
            }
        }

    }
}
