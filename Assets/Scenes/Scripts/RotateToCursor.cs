using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCursor : MonoBehaviour
{
    Vector3 mousePos;
    Camera cam;
    Rigidbody2D rid;

    void Start()
    {
        rid = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        RotateToCamera();
    }

    void RotateToCamera()  // Removed the semicolon here
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - cam.transform.position.z));
        rid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((mousePos.y - transform.position.y), (mousePos.x - transform.position.x)) * Mathf.Rad2Deg);
    }
}
