using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCtr : MonoBehaviour
{
    [SerializeField] private GameObject panel_1;
    [SerializeField] private GameObject panel_2;
    [SerializeField] private GameObject panel_3;
    [SerializeField] private GameObject panel_4;
    [SerializeField] private GameObject compass_1;
    [SerializeField] private GameObject compass_2;
    private int page = 1;

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            if (page == 1)
            {
                page = 2;
                panel_1.SetActive(false);
                panel_2.SetActive(true);
                compass_1.SetActive(false);
                compass_2.SetActive(false);
            }
            else if (page == 2)
            {
                page = 3;
                panel_1.SetActive(true);
                panel_2.SetActive(false);
                compass_1.SetActive(true);
                compass_2.SetActive(true);
            }
            else if (page == 3)
            {
                page = 4;
                panel_1.SetActive(false);
                panel_3.SetActive(true);
                compass_1.SetActive(false);
                compass_2.SetActive(false);
            }
            else if (page == 4)
            {
                page = 5;
                panel_4.SetActive(true);
                panel_3.SetActive(false);
            }
            else if (page == 5)
            {
                page = 1;
                panel_1.SetActive(true);
                panel_4.SetActive(false);
                compass_1.SetActive(true);
                compass_2.SetActive(true);
            }
        }
    }
}
