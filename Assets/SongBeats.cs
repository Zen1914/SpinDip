using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rhythm/Song")]
public class SongBeats : ScriptableObject
{
    public string songName;
    public BeatEvent[] beats;
}

[System.Serializable]
public class BeatEvent
{
    public int spawnerObjIndex;
    public float time;
    public bool special;
    public bool isRed;
}