using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SoundSys : MonoBehaviour
{
    public static SoundSys Instance { get; private set; }

    [Header("Audio Scource")]
    public AudioSource musicSource;
    public AudioSource sfxSource;


    [Header("BackGround Music")]
    public AudioClip BGMClip;

    [Header("Sound Effects")]
    public AudioClip cardFlip;
    public AudioClip cardMatch;
    public AudioClip cardMismatch;
    public AudioClip levelFin;
    public AudioClip gameOver;


    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // to be persisting between scenes
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayMusic()
    {
        if (BGMClip != null && musicSource != null)
        {
            musicSource.clip = BGMClip;
            musicSource.loop = true;
            musicSource.Play();
        }

    }


    // for sound effects

    public void PlayCardFlip()
    {
        PlaySFX(cardFlip);
    }
    public void PlayCardMatch()
    {
        PlaySFX(cardMatch);
    }

    public void PlayCardMismatch()
    {
        PlaySFX(cardMismatch);
    }
    
    public void PlayLevelFin()
    {
        PlaySFX(levelFin);
    }
    
    public void PlayGameOVer()
    {
        PlaySFX(gameOver);
    }

    public void PlaySFX(AudioClip audioclip)
    {
        if(BGMClip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(audioclip);
        }
    }
}
