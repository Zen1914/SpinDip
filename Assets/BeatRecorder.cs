using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatRecorder : MonoBehaviour
{
    public AudioSource audioSource;

    private double startTime;
    private bool isPlaying;

    private void Start()
    {
        startTime = AudioSettings.dspTime;
        audioSource.Play();
        isPlaying = true;
        Debug.Log("Song started");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            double songTime = AudioSettings.dspTime - startTime;
            Debug.Log("Beat at: " + songTime);   
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("change pos!");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("piano!");
        }
    }
}