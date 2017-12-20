//UIManager by Jordi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text[] intensityText;
    public Image[] intensityBackground;

    public Text calmSourceText;
    public Text quietSourceText;
    public Text dynamicSourceText;

    public float fillSpeed;

    public void OnButtonClick(int button)
    {
        //Play all sounds at the same time.
        if(!Camera.main.GetComponent<SoundManager>().quietSource.isPlaying)
        {
            Camera.main.GetComponent<SoundManager>().quietSource.Play();
            Camera.main.GetComponent<SoundManager>().mediumSource.Play();
            Camera.main.GetComponent<SoundManager>().dynamicSource.Play();
        }

        //Set the intensity.
        Camera.main.GetComponent<SoundManager>().SetLevel((SoundManager.AudioLevel)button);

        //Fill the button of the currently playing intensity
        foreach (Image intbg in intensityBackground)
        {
            if(intbg.fillAmount > 0.2f)
            {
                StartCoroutine(FillBackground(intbg, false));
            }
        }

        //Light up the text of the sources that are playing.
        foreach (Text intText in intensityText)
        {
            intText.color = new Color(0.5f, 0.5f, 0.5f);
        }
        intensityText[button].color = new Color(0.1f, 0.1f, 0.1f);

        StartCoroutine(FillBackground(intensityBackground[button], true));

        //Change textcolor based on the intensity that is playing.
        switch (button)
        {
            case 0:
                quietSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                calmSourceText.color = new Color(0.25f, 0.25f, 0.25f);
                dynamicSourceText.color = new Color(0.25f, 0.25f, 0.25f);
                break;

            case 1:
                quietSourceText.color = new Color(0.25f, 0.25f, 0.25f);
                calmSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                dynamicSourceText.color = new Color(0.25f, 0.25f, 0.25f);
                break;

            case 2:
                quietSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                calmSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                dynamicSourceText.color = new Color(0.25f, 0.25f, 0.25f);
                break;

            case 3:
                quietSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                calmSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                dynamicSourceText.color = new Color(0.9f, 0.9f, 0.9f);
                break;

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

    //Fill the buttons' backgorunds.
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
