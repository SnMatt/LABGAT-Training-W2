using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private Camera _cam;
    private SphereCollider _collider;
    private TrailRenderer _trail;

    public Vector3 Direction { get; private set; }

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        _cam = Camera.main;
        _trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        EndSlicing();
    }
    private void OnDisable()
    {
        EndSlicing();
    }

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                StartSlicing(touch);
            }
            else if(touch.phase == TouchPhase.Moved)
            {
                Slice(touch);
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                EndSlicing();
            }

        }
    }

    private void StartSlicing(Touch touch)
    {
        _collider.enabled = true;
        _trail.enabled = true;

        Vector3 newPos = _cam.ScreenToWorldPoint(touch.position);
        newPos.z = 0;
        transform.position = newPos;
    }
    private void EndSlicing()
    {
        _trail.Clear();
        _trail.enabled = false;
        _collider.enabled = false;
    }
    private void Slice(Touch touch)
    {
        Vector3 newPos = _cam.ScreenToWorldPoint(touch.position);
        newPos.z = 0;

        Direction = newPos - transform.position;

        transform.position = newPos;
    }
}
