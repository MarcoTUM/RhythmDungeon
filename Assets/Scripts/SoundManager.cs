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

    public void PlayBGM(int sel)
    {
        if (sel < BGMClips.Count)
        {
            BGM.clip = BGMClips[sel];
            BGM.Play();
        }
        else
            Debug.LogError("BGM selection too high!");
        
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
