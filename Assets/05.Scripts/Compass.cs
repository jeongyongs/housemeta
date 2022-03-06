using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField] private Transform tablet;
    [SerializeField] private Transform cam;

    private Vector3 vec;

    void Update()
    {
        vec.z = tablet.localEulerAngles.y + 90 + cam.localEulerAngles.y;
        transform.localEulerAngles = vec;
    }
}
