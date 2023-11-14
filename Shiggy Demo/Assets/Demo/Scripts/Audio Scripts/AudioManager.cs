using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public bool isEnd = false;
    public AudioSource AudioSource;
    private void Awake()
    {
        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;

            s.source.volume = s.volume;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Background");
    }

    private void Update()
    {
        if (!FindObjectOfType<AudioManager>().IsPlaying("Background"))
        {
            FindObjectOfType<AudioManager>().Play("Background");
        }
    }

    public void KillAllSound()
    {
        isEnd = true;
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (!isEnd)
        {
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }

    public bool IsPlaying(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.isPlaying;
    }
}
