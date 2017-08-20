//SoundManager by Jordi vd Hulst

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //The state of intensity
    public enum AudioLevel
    {
        Quiet,
        Calm,
        Medium,
        Dynamic
    };

    public AudioLevel audioLevel;

    //Global source
    AudioSource _sound;

    //Settings
    public float bgmVolume;
    public float fadeSpeed;

    //Source for the intensities
    public AudioSource quietSource;
    public AudioSource mediumSource;
    public AudioSource dynamicSource;

    //What to play
    public AudioClip[] currentlyPlaying;


    //  --- Audio Tracks ---
    public AudioClip[] island;
    public AudioClip[] intro;
    public AudioClip[] conversation;
    public AudioClip[] house;

    // ----------------------


    void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    //General settings for this project
    void Start()
    {
        bgmVolume = 0.5f;
        fadeSpeed = 0.4f;
        SetLevel(AudioLevel.Calm);
    }


    void Update()
    {
        //Hotkeys for testing
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetLevel(AudioLevel.Calm);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetLevel(AudioLevel.Quiet);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetLevel(AudioLevel.Medium);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetLevel(AudioLevel.Dynamic);
        }
    }

    //Change the currently playing BGM with a fade transition
    public void ChangeBGM(AudioClip[] clips, AudioLevel level)
    {
        //Fade out the current audio and start fading in the new.
        if(clips != currentlyPlaying)
        {
            StartCoroutine(FadeOutClips(clips));
        }
        else
        {
            currentlyPlaying = clips;
            for (int i = 0; i < currentlyPlaying.Length; i++)
            {
                if (i == 0)
                {
                    quietSource.clip = currentlyPlaying[0];
                }
                if (i == 1)
                {
                    mediumSource.clip = currentlyPlaying[1];
                }
                if (i == 2)
                {
                    dynamicSource.clip = currentlyPlaying[2];
                }
            }

            SetLevel(level);
        }
    }

    //Change the intensity of the music
    public void SetLevel(AudioLevel level)
    {
        if(level == AudioLevel.Quiet)
        {
            StartCoroutine("FadeOut", mediumSource);
            StartCoroutine("FadeOut", dynamicSource);
            StartCoroutine("FadeIn", quietSource);
        }
        if (level == AudioLevel.Calm)
        {
            StartCoroutine("FadeIn", mediumSource);
            StartCoroutine("FadeOut", dynamicSource);
            StartCoroutine("FadeOut", quietSource);
        }
        if (level == AudioLevel.Medium)
        {
            StartCoroutine("FadeIn", mediumSource);
            StartCoroutine("FadeOut", dynamicSource);
            StartCoroutine("FadeIn", quietSource);
        }
        if (level == AudioLevel.Dynamic)
        {
            StartCoroutine("FadeIn", mediumSource);
            StartCoroutine("FadeIn", dynamicSource);
            StartCoroutine("FadeIn", quietSource);
        }
        
    }

    //Play voice acting
    public void PlaySpeech(string speech)
    {
        _sound.Stop();
        AudioClip speechToPlay;
        speechToPlay = Resources.Load<AudioClip>(speech);
        _sound.PlayOneShot(speechToPlay);
    }

    //Fade out all music to play new music
    public IEnumerator FadeOutClips(AudioClip[] newClips)
    {
        while (quietSource.volume > 0 && mediumSource.volume > 0 && dynamicSource.volume > 0)
        {
            quietSource.volume -= Time.deltaTime * fadeSpeed; ;
            mediumSource.volume -= Time.deltaTime * fadeSpeed; ;
            dynamicSource.volume -= Time.deltaTime * fadeSpeed; ;
            yield return new WaitForEndOfFrame();
        }
        
        currentlyPlaying = newClips;

        for(int i = 0; i < currentlyPlaying.Length; i++)
        {
            if(i == 0)
            {
                quietSource.clip = currentlyPlaying[0];
            }
            if(i == 1)
            {
                mediumSource.clip = currentlyPlaying[1];
            }
            if(i == 2)
            {
                dynamicSource.clip = currentlyPlaying[2];
            }
        }
        quietSource.Play();
        mediumSource.Play();
        dynamicSource.Play();

        Debug.Log("set quiet now");
        SetLevel(AudioLevel.Quiet);
        yield break;
    }

    //Fade in new source
    public IEnumerator FadeIn(AudioSource source)
    {
        while (source.volume < bgmVolume)
        {
            source.volume += Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    //Fade out only one source
    public IEnumerator FadeOut(AudioSource source)
    {
        while(source.volume > 0)
        {
            source.volume -= Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }
}
