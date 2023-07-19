using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public static class Sounds
{
    public static readonly string WinMusic = "WinMusic";
    public static readonly string LoseMusic = "LoseMusic";
    public static readonly string MainMusic = "MainMusic";
    public static readonly string GetPointSFX = "GetPoint";
    public static readonly string LostLifeSFX = "LostLife";
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] private MusicDate[] musicDates;
    [SerializeField] private MusicDate[] SFXDates;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;
  

    public void ChangeMusic(string name)
    {
        MusicDate s = Array.Find(musicDates, x => x.musicName == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.musicClip;
            musicSource.Play();
        }
    }


    public void ChangeSFX(string name)
    {
        MusicDate s = Array.Find(SFXDates, x => x.musicName == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            SFXSource.PlayOneShot(s.musicClip);
        }
    }
}
