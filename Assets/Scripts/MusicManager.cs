using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Timer timer;

    public int SpawnCount => spawns;

    public SpawnTile[] spawnTile;
    public float bpm = 120f;
    public int spawnEveryNBeats = 2;
    public float travelTime = 1.0f;


    private int beatCount = 0;
    private double startTime;
    private double nextBeatTime = 0;
    private double beatInterval;
    private int spawns;

    void Start()
    {
        startTime = AudioSettings.dspTime; //mark the start of a song
        audioSource.Play();
        beatInterval = 60.0 / bpm; //convert bpm to seconds per beat
    }

    private void Update()
    {
        if (timer.TimesUP)
        {
            Debug.Log("times up!");
            return;
        }

        double songTime = AudioSettings.dspTime - startTime;
        while (songTime >= nextBeatTime - travelTime)
        {
            if (beatCount % spawnEveryNBeats == 0) //every n beats spawn
            {
                spawnTile[Random.Range(0, spawnTile.Length)].Spawn();
                spawns++;
            }
            beatCount++;
            nextBeatTime += beatInterval;
        }
    }
}
