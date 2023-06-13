using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast2D : MonoBehaviour
{
    Vector3 Mouseposition;
    public Camera camera;
    Transform CurrentTouch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
