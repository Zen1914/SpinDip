using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] SpawnTile[] spawnTiles; // spawn points
    [SerializeField] SongBeats beatsData; //beats
    [SerializeField] AudioSource audioSource; //song
    [SerializeField] Difficulty difficulty;

    private double startTime;
    private int index = 0;
    private bool spawnedMidBeat;

    public int BeatCount
    {
        get {return beatsData.beats.Length; }
    }

    void Start()
    {
        startTime = AudioSettings.dspTime;
        audioSource.Play();
    }

    void Update()
    {
        double songTime = AudioSettings.dspTime - startTime;

        if (index >= beatsData.beats.Length)
        {
            Debug.Log("no more beats left!");
            return;
        }

        if (songTime >= beatsData.beats[index].time - 3.5f)
        {
            //1. gets teh spawner index in data
            int spawner = beatsData.beats[index].spawnerObjIndex;

            //2. check if it is more than spawner length
            if(spawner >= spawnTiles.Length)
            {
                spawner = 0;
            }
            //3. spawn 
            Debug.Log($"spawning: {index}");
            spawnTiles[spawner].Spawn(beatsData.beats[index].isRed);

            //4. increase index
            index++;

            //5. mid
            if (Random.value < 0.3f && difficulty == Difficulty.normal)
            {
                StartCoroutine(SpawnExtraNote(beatsData.beats[index]));
            }
        }
    }

    private IEnumerator SpawnExtraNote(BeatEvent beat)
    {
        yield return new WaitForSeconds(0.25f);

        int spawner = beat.spawnerObjIndex;
        bool isRed = beat.isRed;
        spawnTiles[spawner].Spawn(isRed);
    }

}

public enum Difficulty
{
    easy,
    normal
}
