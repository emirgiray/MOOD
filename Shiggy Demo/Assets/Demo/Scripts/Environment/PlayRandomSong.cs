using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSong : MonoBehaviour
{
    public AudioClip[] song;
    AudioSource audioToPlay;

    private int pickedSong;
    private int previousSong;

    // Start is called before the first frame update
    void Start()
    {
        audioToPlay = gameObject.GetComponent<AudioSource>();
        Debug.Log(audioToPlay.clip.length);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{   
        //    audio.Stop();   
        //    audio.clip = song[0];
        //    audio.Play();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    audio.Stop();
        //    audio.clip = song[1];
        //    audio.Play();
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    SwitchSong();
        //}
        if (!audioToPlay.isPlaying)
        {
            SwitchSong();
        }
    }
    public void SwitchSong()
    {        
        audioToPlay.Stop();
        previousSong = pickedSong;
        pickedSong = Random.Range(0, song.Length);
        if(previousSong == pickedSong)
        {
            SwitchSong();
        }        
        audioToPlay.clip = song[pickedSong];
        audioToPlay.Play();
        Debug.Log(audioToPlay.clip.length);
        //Invoke("SwitchSong", audioToPlay.clip.length); //------- gives wrong times for songs
    }
}
