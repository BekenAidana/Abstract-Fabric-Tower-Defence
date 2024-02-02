using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _camera;
    public GameObject Pointer;
    public LayerMask LayerMask;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!Pointer) return;

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit, 100, ~LayerMask);

            if (hit.collider)
            {
                Pointer.transform.position = hit.point;
                Pointer = null;
            }
        }
    }
}