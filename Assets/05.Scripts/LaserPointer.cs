using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    private bool isJumped = false;
    private bool isMeasure = false;
    private RaycastHit hit;
    private LineRenderer line;
    public float maxDistance = 30.0f;
    public Transform laserMarker;
    [SerializeField] private GameObject prefeb;
    [SerializeField] private List<GameObject> Prefebs;
    [SerializeField] private Material laser;
    private int prefebs_num = 0;
    private int measureStage = 1;

    void Start()
    {
        CreateLineRenderer();
        line.enabled = false;
    }

    void CreateLineRenderer()
    {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.receiveShadows = false;

        line.positionCount = 2;
        line.SetPosition(0, Vector3.zero);
        line.SetPosition(1, Vector3.forward * maxDistance);

        line.startWidth = 0.01f;
        line.endWidth = 0.005f;
        line.numCapVertices = 10;

        line.material = laser;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (isJumped)
            {
                isJumped = false;
                line.enabled = false;
                laserMarker.position = new Vector3(0, -10, 0);
            }
            else if (!isJumped && !isMeasure)
            {
                isJumped = true;
                line.enabled = true;
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            if (isMeasure)
            {
                isMeasure = false;
                line.enabled = false;
                for (int i = 0; i < prefebs_num; i++)
                {
                    Destroy(Prefebs[i], 0.0f);
                }
            }
            else if (!isJumped && !isMeasure)
            {
                isMeasure = true;
                line.enabled = true;
            }
        }

        if (isMeasure)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                line.SetPosition(1, Vector3.forward * hit.distance);

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                {
                    if (measureStage == 1) // 치수 시작
                    {
                        GameObject _obj = Instantiate(prefeb) as GameObject;
                        Prefebs.Add(_obj);
                        Prefebs[prefebs_num].transform.position = hit.point;
                        measureStage = 2;
                    }
                    else if (measureStage == 2)
                    {
                        measureStage = 1;
                        prefebs_num++;
                    }
                }

                if (measureStage == 2)
                {
                    Prefebs[prefebs_num].transform.GetChild(1).transform.position = hit.point;
                }

            }
            else
            {
                line.SetPosition(1, Vector3.forward * maxDistance);
            }
        }

        if (isJumped)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
            {
                line.SetPosition(1, Vector3.forward * hit.distance);
                laserMarker.position = hit.point + laserMarker.forward * 0.01f;
                laserMarker.rotation = Quaternion.LookRotation(hit.normal);

                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
                {
                    StartCoroutine(Teleport(hit.point));
                }
#if UNITY_EDITOR
                if (Input.GetMouseButtonDown(0))
                {
                    StartCoroutine(Teleport(hit.point));
                }
#endif
            }
            else
            {
                line.SetPosition(1, Vector3.forward * maxDistance);
                laserMarker.position = transform.position + (transform.forward * maxDistance);
                laserMarker.rotation = Quaternion.LookRotation(transform.forward);
            }
        }
    }

    IEnumerator Teleport(Vector3 pos)
    {
        OVRScreenFade.instance.fadeTime = 0.0f;
        OVRScreenFade.instance.FadeOut();

        transform.root.position = pos + (Vector3.up * 1.6f);
        yield return new WaitForSeconds(0.01f);
        OVRScreenFade.instance.fadeTime = 0.2f;
        OVRScreenFade.instance.FadeIn();
    }

    IEnumerator destroy()
    {
        yield return null;
    }
}
