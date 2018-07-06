using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;
    public AudioSource BGM;
    public AudioSource SFX;

    public List<AudioClip> BGMClips= new List<AudioClip>();
    public List<AudioClip> SFXClips = new List<AudioClip>();
    // Use this for initialization
    void Awake ()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        BGM.loop = true;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator BGMStart()
    {
        for (int i = 100; i > 0; i--)
        {
            BGM.volume += 0.008f;
            yield return new WaitForSeconds(0.03f);
        }
    }
    public void PlayBGM(int sel)
    {
        BGM.volume = 0.2f;

        if (sel < BGMClips.Count)
        {
            StopBGM();
            BGM.clip = BGMClips[sel];
            BGM.Play();
            StartCoroutine(BGMStart());
        }
        else
            Debug.LogError("BGM selection too high!");
    }
    public void StopBGM()
    {
        BGM.Stop();
    }

    public void PlaySFX(int sel)
    {
        if (sel < SFXClips.Count)
        {
            SFX.clip = SFXClips[sel];
            SFX.Play();
        }
        else
            Debug.LogError("SFX selection too high!");

    }
}
