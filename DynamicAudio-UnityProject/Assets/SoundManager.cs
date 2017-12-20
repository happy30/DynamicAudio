//SoundManager by Jordi
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource _sound;
    public float bgmVolume;
    public float fadeSpeed;

    public AudioSource quietSource;
    public AudioSource mediumSource;
    public AudioSource dynamicSource;

    //Different areas have different BGM
    public AudioClip[] island;
    public AudioClip[] intro;
    public AudioClip[] conversation;
    public AudioClip[] house;

    public AudioClip[] currentlyPlaying;

    public enum AudioLevel
    {
        Quiet,
        Calm,
        Medium,
        Dynamic
    };

    public AudioLevel audioLevel;

    void Awake()
    {
        _sound = GetComponent<AudioSource>();
    }

    void Start()
    {
        bgmVolume = 0.5f;
        fadeSpeed = 0.4f;
        //ChangeBGM(island);
        SetLevel(AudioLevel.Calm);
    }


    void Update()
    {
        //Hotkeys for debugging
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
    public void ChangeBGM(AudioClip[] clips)
    {
        if(clips != currentlyPlaying)
        {
            StartCoroutine(FadeOutClips(clips));
        }
        else
        {
            //If we have a total new track to play, change all intensity levels
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

            SetLevel(AudioLevel.Quiet);
        }
    }

    //Change the intensity of the music
    public void SetLevel(AudioLevel level)
    {
        switch (level)
        {
            case AudioLevel.Quiet:
                StartCoroutine("FadeOut", mediumSource);
                StartCoroutine("FadeOut", dynamicSource);
                StartCoroutine("FadeIn", quietSource);
                break;

            case AudioLevel.Calm:
                StartCoroutine("FadeIn", mediumSource);
                StartCoroutine("FadeOut", dynamicSource);
                StartCoroutine("FadeOut", quietSource);
                break;

            case AudioLevel.Medium:
                StartCoroutine("FadeIn", mediumSource);
                StartCoroutine("FadeOut", dynamicSource);
                StartCoroutine("FadeIn", quietSource);
                break;

            case AudioLevel.Dynamic:
                StartCoroutine("FadeIn", mediumSource);
                StartCoroutine("FadeIn", dynamicSource);
                StartCoroutine("FadeIn", quietSource);
                break;


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
        //Fade everything out.
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
        SetLevel(AudioLevel.Quiet);
        yield break;
    }

    //Fade in new intensity level
    public IEnumerator FadeIn(AudioSource source)
    {
        while (source.volume < bgmVolume)
        {
            source.volume += Time.deltaTime * fadeSpeed;
            yield return new WaitForEndOfFrame();
        }
        yield break;
    }

    //Fade out one intensity level
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
