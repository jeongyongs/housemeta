using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OpeningManager : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller leftController = OVRInput.Controller.LTouch;
    [SerializeField] private OVRInput.Controller rightController = OVRInput.Controller.RTouch;
    [SerializeField] private bool trigger_1 = false;
    [SerializeField] private bool trigger_2 = false;
    [SerializeField] private bool trigger_3 = false;
    [SerializeField] private bool trigger_4 = false;
    [SerializeField] private bool trigger_5 = false;
    [SerializeField] private int page = 1;
    [SerializeField] private Transform charic;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject canvas_2;
    [SerializeField] private GameObject canvas_3;
    [SerializeField] private GameObject canvas_4;
    [SerializeField] private GameObject canvas_5;
    [SerializeField] private GameObject canvas_6;
    [SerializeField] private Animator anim;
    [SerializeField] private TMP_Text text;

    void Start()
    {
        text.color = new Color32(255, 255, 255, 0);
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && !trigger_1)
        {
            StartCoroutine("isClicked");
            StartCoroutine("fadeOut");
        }

        if (trigger_2)
        {
            if (charic.position.z > 2)
            {
                charic.Translate(Vector3.forward * Time.deltaTime);
            }
            else
            {
                if (!trigger_3)
                {
                    StartCoroutine("introduce");
                }
            }

            if (charic.position.z < 2.5 && !trigger_3)
            {
                anim.SetBool("Walk", false);
            }
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && trigger_4 && page == 1)
        {
            canvas_2.SetActive(false);
            canvas_3.SetActive(true);
            page = 2;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && trigger_4 && page == 2)
        {
            canvas_3.SetActive(false);
            canvas_4.SetActive(true);
            page = 3;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && trigger_4 && page == 2)
        {
            canvas_2.SetActive(true);
            canvas_3.SetActive(false);
            page = 1;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && trigger_4 && page == 3)
        {
            canvas_5.SetActive(true);
            canvas_4.SetActive(false);
            page = 4;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && trigger_4 && page == 3)
        {
            canvas_3.SetActive(true);
            canvas_4.SetActive(false);
            page = 2;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && trigger_4 && page == 4)
        {
            canvas_6.SetActive(true);
            canvas_5.SetActive(false);
            page = 5;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && trigger_4 && page == 4)
        {
            canvas_4.SetActive(true);
            canvas_5.SetActive(false);
            page = 3;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && trigger_4 && page == 5)
        {
            page = 6;
            trigger_5 = true;
        }
        else if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch) && trigger_4 && page == 5)
        {
            canvas_5.SetActive(true);
            canvas_6.SetActive(false);
            page = 4;
        }
        if (trigger_5)
        {
            StartCoroutine("nextScene");
            trigger_5 = false;
        }
    }

    IEnumerator nextScene()
    {
        OVRScreenFade.instance.fadeTime = 0.0f;
        OVRScreenFade.instance.FadeOut();
        canvas_6.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        OVRScreenFade.instance.fadeTime = 0.2f;
        OVRScreenFade.instance.FadeIn();
        OVRInput.SetControllerVibration(0.8f, 0.9f, rightController);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0, 0, rightController);
        yield return new WaitForSeconds(1f);
        text.text = "선택하신 방으로 이동할게요!";
        StartCoroutine("FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(3f);
        OVRScreenFade.instance.fadeTime = 2.0f;
        OVRScreenFade.instance.FadeOut();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("02.Room");
    }

    IEnumerator isClicked()
    {
        trigger_1 = true;
        OVRInput.SetControllerVibration(0.8f, 0.9f, rightController);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(0, 0, rightController);
    }

    IEnumerator fadeOut()
    {
        OVRScreenFade.instance.fadeTime = 0.0f;
        OVRScreenFade.instance.FadeOut();
        canvas.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        OVRScreenFade.instance.fadeTime = 0.2f;
        OVRScreenFade.instance.FadeIn();
        yield return new WaitForSeconds(1f);
        anim.SetBool("Walk", true);
        trigger_2 = true;
    }

    IEnumerator introduce()
    {
        trigger_3 = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine("FadeIn");
        anim.SetBool("Hi", true);
        yield return new WaitForSeconds(2f);
        anim.SetBool("Hi", false);
        yield return new WaitForSeconds(1f);
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(3f);
        text.text = "저는 체험도우미 메타뇽이에요.";
        StartCoroutine("FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(3f);
        text.text = "그럼 먼저 방을 찾아 볼까요?";
        StartCoroutine("FadeIn");
        yield return new WaitForSeconds(3f);
        StartCoroutine("FadeOut");
        yield return new WaitForSeconds(2f);
        OVRScreenFade.instance.fadeTime = 0.0f;
        OVRScreenFade.instance.FadeOut();
        canvas_2.SetActive(true);
        yield return new WaitForSeconds(0.01f);
        OVRScreenFade.instance.fadeTime = 0.2f;
        OVRScreenFade.instance.FadeIn();
        trigger_4 = true;
    }

    IEnumerator FadeIn()
    {
        int i = 0;
        while (i < 255)
        {
            text.color = new Color32(255, 255, 255, (byte)(i));
            i += 2;
            yield return new WaitForSeconds(0.0001f);
        }
        yield return null;
    }

    IEnumerator FadeOut()
    {
        int i = 255;
        while (i > 0)
        {
            text.color = new Color32(255, 255, 255, (byte)(i));
            i -= 2;
            yield return new WaitForSeconds(0.0001f);
        }
        yield return null;
    }
}