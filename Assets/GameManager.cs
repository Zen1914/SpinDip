using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitTextUI;
    [SerializeField] TextMeshProUGUI missTextUI;
    [SerializeField] GameObject lostPanel;

    public int missLost = 6;

    private int hit = 0;
    private int miss = 0;

    private void Awake()
    {
        UpdateUI();
    }

    public void AddMiss()
    {
        miss++;
        if(miss >= missLost)
        {
            HandleLost();
        }
        UpdateUI();
    }

    public void AddHit()
    {
        hit++;
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
        if(hitTextUI == null || missTextUI == null)
        {
            Debug.Log("missing UI!");
            return;
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
