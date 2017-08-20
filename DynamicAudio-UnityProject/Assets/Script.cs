using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script : MonoBehaviour
{
    public Text[] intensityText;
    public Image[] intensityBackground;

    public Text calmSourceText;
    public Text quietSourceText;
    public Text dynamicSourceText;

    public float fillSpeed;

    public void OnButtonClick(int button)
    {
        if(!Camera.main.GetComponent<SoundManager>().quietSource.isPlaying)
        {
            Camera.main.GetComponent<SoundManager>().quietSource.Play();
            Camera.main.GetComponent<SoundManager>().mediumSource.Play();
            Camera.main.GetComponent<SoundManager>().dynamicSource.Play();
        }

        Camera.main.GetComponent<SoundManager>().SetLevel((SoundManager.AudioLevel)button);


        foreach (Image intbg in intensityBackground)
        {
            if(intbg.fillAmount > 0.2f)
            {
                StartCoroutine(FillBackground(intbg, false));
            }
        }

        foreach (Text intText in intensityText)
        {
            intText.color = new Color(0.5f, 0.5f, 0.5f);
        }
        intensityText[button].color = new Color(0.1f, 0.1f, 0.1f);

        StartCoroutine(FillBackground(intensityBackground[button], true));

        if(button == 0)
        {
            quietSourceText.color = new Color(0.9f, 0.9f, 0.9f);
            calmSourceText.color = new Color(0.25f, 0.25f, 0.25f);
            dynamicSourceText.color = new Color(0.25f, 0.25f, 0.25f);
        }
        else if(button == 1)
        {
            quietSourceText.color = new Color(0.25f, 0.25f, 0.25f);
            calmSourceText.color = new Color(0.9f, 0.9f, 0.9f);
            dynamicSourceText.color = new Color(0.25f, 0.25f, 0.25f);
        }
        else if (button == 2)
        {
            quietSourceText.color = new Color(0.9f, 0.9f, 0.9f);
            calmSourceText.color = new Color(0.9f, 0.9f, 0.9f);
            dynamicSourceText.color = new Color(0.25f, 0.25f, 0.25f);
        }
        else if (button == 3)
        {
            quietSourceText.color = new Color(0.9f, 0.9f, 0.9f);
            calmSourceText.color = new Color(0.9f, 0.9f, 0.9f);
            dynamicSourceText.color = new Color(0.9f, 0.9f, 0.9f);
        }
    }

    /*
    public void DisableMusic()
    {
        quietSourceText.color = new Color(0.5f, 0.5f, 0.5f);
        calmSourceText.color = new Color(0.5f, 0.5f, 0.5f);
        dynamicSourceText.color = new Color(0.5f, 0.5f, 0.5f);

        foreach (Text intText in intensityText)
        {
            intText.color = new Color(0.5f, 0.5f, 0.5f);
        }

        foreach (Image intbg in intensityBackground)
        {
            if (intbg.fillAmount > 0.2f)
            {
                StartCoroutine(FillBackground(intbg, false));
            }
        }

        StartCoroutine(Camera.main.GetComponent<SoundManager>().FadeOut(Camera.main.GetComponent<SoundManager>().quietSource));
        StartCoroutine(Camera.main.GetComponent<SoundManager>().FadeOut(Camera.main.GetComponent<SoundManager>().mediumSource));
        StartCoroutine(Camera.main.GetComponent<SoundManager>().FadeOut(Camera.main.GetComponent<SoundManager>().dynamicSource));
    }
    */


    IEnumerator FillBackground(Image bg, bool fill)
    {
        if (fill)
        {
            while (bg.fillAmount < 1)
            {
                bg.fillAmount += fillSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            yield break;

        }
        else
        {
            while (bg.fillAmount > 0)
            {
                bg.fillAmount -= fillSpeed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            yield break;
        }
    }
}
