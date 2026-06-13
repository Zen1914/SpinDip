using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rankings
{
    public static void OnSongFinished(int songId, Ranks rank)
    {
        RankingsData data = SaveManager.Load();

        if (data == null)
            data = new RankingsData();

        var existing = data.ranks.Find(x => x.id == songId);

        if (existing != null)
        {
            existing.rank = rank;
        }
        else
        {
            data.ranks.Add(new RankEachSong
            {
                id = songId,
                rank = rank
            });
        }

        SaveManager.Save(data);
    }
}

[Serializable]
public class RankEachSong
{
    public int id = -1;
    public Ranks rank = Ranks.U;
}

[Serializable]
public class RankingsData
{
    public List<RankEachSong> ranks = new List<RankEachSong>();
}

public enum Ranks
{
    S,
    A,
    B,
    C,
    D,
    U
}
