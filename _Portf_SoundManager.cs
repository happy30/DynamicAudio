//Change the intensity of the music
public void SetLevel(AudioLevel level)
{
    if (level == AudioLevel.Quiet)
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
    while (source.volume > 0)
    {
        source.volume -= Time.deltaTime * fadeSpeed;
        yield return new WaitForEndOfFrame();
    }
    yield break;
}