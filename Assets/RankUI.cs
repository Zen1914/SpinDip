using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankUI : MonoBehaviour
{
    public int songId;
    public TextMeshProUGUI rankText;

    void Start()
    {
        RankingsData data = SaveManager.Load();

        if (data == null)
        {
            rankText.text = "U";
            return;
        }

        RankEachSong entry = data.ranks.Find(x => x.id == songId);

        if (entry != null)
        {
            rankText.text = entry.rank.ToString();

            if (entry.rank == Ranks.S)
                rankText.color = Color.yellow; // gold-ish

            else if (entry.rank == Ranks.A)
                rankText.color = Color.green;

            else if (entry.rank == Ranks.B)
                rankText.color = Color.cyan;

            else if (entry.rank == Ranks.C)
                rankText.color = Color.white;

            else if (entry.rank == Ranks.D)
                rankText.color = Color.gray;
        }
        else
        {
            rankText.text = "U";
            rankText.color = Color.white;
        }

        if (entry != null)
        {
            rankText.text = entry.rank.ToString();
        }
        else
        {
            rankText.text = "U";
        }
    }
}
