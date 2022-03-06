using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private GameObject Sun;
    [SerializeField] private GameObject dayIcon;
    [SerializeField] private GameObject nightIcon;
    [SerializeField] private GameObject eveningIcon;
    [SerializeField] private AudioSource BirdSound;
    private Transform sunTrans;
    private Light sunLight;
    [SerializeField] private Material day;
    [SerializeField] private Material evening;
    [SerializeField] private Material night;
    private int Time = 1;
    private int page = 1;

    void Start()
    {
        sunTrans = Sun.GetComponent<Transform>();
        sunLight = Sun.GetComponent<Light>();
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            if (page == 1)
            {
                page = 2;
                dayIcon.SetActive(false);
                eveningIcon.SetActive(false);
                nightIcon.SetActive(false);
            }
            else if (page == 2)
            {
                page = 3;
                if (Time == 1)
                {
                    dayIcon.SetActive(true);
                }
                else if (Time == 2)
                {
                    eveningIcon.SetActive(true);
                }
                else if (Time == 3)
                {
                    nightIcon.SetActive(true);
                }
            }
            else if (page == 3)
            {
                page = 4;
                dayIcon.SetActive(false);
                eveningIcon.SetActive(false);
                nightIcon.SetActive(false);
            }
            else if (page == 4)
            {
                page = 5;
            }
            else if (page == 5)
            {
                page = 1;
                if (Time == 1)
                {
                    dayIcon.SetActive(true);
                }
                else if (Time == 2)
                {
                    eveningIcon.SetActive(true);
                }
                else if (Time == 3)
                {
                    nightIcon.SetActive(true);
                }
            }
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            if (Time == 1)      // 저녁
            {
                Time = 2;
                RenderSettings.ambientIntensity = 0.3f;
                RenderSettings.reflectionIntensity = 0.3f;
                sunLight.intensity = 0.5f;
                sunLight.color = new Color32(255, 75, 0, 255);
                RenderSettings.skybox = evening;
                BirdSound.mute = true;
                sunTrans.localEulerAngles = new Vector3(32, -45, 0);
                dayIcon.SetActive(false);
                eveningIcon.SetActive(true);
            }
            else if (Time == 2) // 밤
            {
                Time = 3;
                RenderSettings.ambientIntensity = 0.05f;
                RenderSettings.reflectionIntensity = 0.05f;
                sunLight.intensity = 0.1f;
                sunLight.color = new Color32(255, 244, 214, 255);
                RenderSettings.skybox = night;
                // BirdSound.mute = true;
                sunTrans.localEulerAngles = new Vector3(25, -30, 0);
                eveningIcon.SetActive(false);
                nightIcon.SetActive(true);
            }
            else if (Time == 3) // 아침
            {
                Time = 1;
                RenderSettings.ambientIntensity = 1.0f;
                RenderSettings.reflectionIntensity = 1.0f;
                sunLight.intensity = 1.0f;
                RenderSettings.skybox = day;
                BirdSound.mute = false;
                sunTrans.localEulerAngles = new Vector3(45, -80, 0);
                dayIcon.SetActive(true);
                nightIcon.SetActive(false);
            }
        }
    }
}
