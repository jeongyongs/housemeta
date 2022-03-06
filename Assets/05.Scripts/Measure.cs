using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Measure : MonoBehaviour
{
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject end;
    [SerializeField] private Transform canvas;
    [SerializeField] private float distance;
    [SerializeField] private TMP_Text text;
    private LineRenderer line;

    void Start()
    {
        line = start.GetComponent<LineRenderer>();
    }

    void Update()
    {
        canvas.localPosition = (end.transform.localPosition - start.transform.localPosition) / 2;
        line.SetPosition(0, start.transform.position);
        line.SetPosition(1, end.transform.position);
        distance = Vector3.Distance(start.transform.position, end.transform.position);
        text.text = distance.ToString("N2") + " m";
    }
}
