using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Music[] music;
    public SFX[] sfx;
    public AudioSource mSource, sSource;

    public static AudioManager instance;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MainMenu")
        {
            PlayM("MainMenu");
        }else if(scene.name == "GameScene")
        {
            PlayM("BattleTheme");
        }else if(scene.name == "Cutscene")
        {
            mSource.Stop();
        }else if(scene.name == "Endscene")
        {
            mSource.Stop();
        }
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Music m in music)
        {
            m.source = mSource;
        }

        foreach (SFX s in sfx)
        {
            s.source = sSource;
        }
    }

    public void SetVolume()
    {
        mSource.volume = currentBaseVolM * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("musicVolume", 1f);
        sSource.volume = currentBaseVolS * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
    }

    float currentBaseVolM, currentBaseVolS;

    public void PlayM(string name)
    {
        Music m = Array.Find(music, music => music.name == name);
        currentBaseVolM = m.volume;
        m.source.loop = m.loop;
        m.source.volume = currentBaseVolM * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("musicVolume", 1f);
        m.source.clip = m.clip;
        m.source.Play();
    }

    public void PlayS(string name)
    {
        SFX s = Array.Find(sfx, sfx => sfx.name == name);
        currentBaseVolS = s.volume;
        s.source.volume = currentBaseVolS * PlayerPrefs.GetFloat("masterVolume", 1f) * PlayerPrefs.GetFloat("sfxVolume", 1f);
        s.source.clip = s.clip;
        s.source.PlayOneShot(s.clip, s.source.volume);
    }
}
