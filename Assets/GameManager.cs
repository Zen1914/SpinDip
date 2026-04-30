using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitTextUI;
    [SerializeField] TextMeshProUGUI missTextUI;
    [SerializeField] TextMeshProUGUI comboUIText;
    [SerializeField] GameObject lostPanel;

    public int missLost = 6;

    int streak = 0;
    int combo = 0;
    private int hit = 0;
    private int miss = 0;

    private void Awake()
    {
        UpdateUI();
    }

    public void AddMiss()
    {
        miss++;
        streak = 0;
        combo = 0;

        if (miss >= missLost)
        {
            HandleLost();
        }

        UpdateUI();
    }

    public void AddHit()
    {
        hit++;
        streak++;

        if (streak >= 2)
        {
            combo++;
        }

        UpdateUI();
    }

    public void ResetAll()
    {
        miss = 0;
        hit = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if(hitTextUI == null || missTextUI == null || comboUIText == null)
        {
            Debug.Log("missing UI!");
            return;
        }

        comboUIText.text = $"Combo: {combo}";
        if (combo <= 0)
        {
            comboUIText.gameObject.SetActive(false);
        }
        else
        {
            comboUIText.gameObject.SetActive(true);
        }

        hitTextUI.text = $"Hit: {hit}";
        missTextUI.text = $"Miss: {miss}";  
    }

    private void HandleLost()
    {
        if(lostPanel == null)
        {
            Debug.Log("empty lostPanel!");
            return;
        }

        lostPanel.SetActive(true);
    }
}
