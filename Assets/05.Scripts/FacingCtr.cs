using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacingCtr : MonoBehaviour
{
    private Transform camTr;

    void Start()
    {
        camTr = Camera.main.transform;
    }

    void LateUpdate() // 모든 업데이트 함수 후 호출
    {
        transform.LookAt(camTr.position);
    }
}
