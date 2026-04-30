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
    [SerializeField] GameObject winPanel;
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameObject[] stars;


    public int missLost = 15;

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

        StartCoroutine(PopUI(missTextUI.transform));
        UpdateUI();
    }

    public void AddHit()
    {
        hit++;
        streak++;

        if (streak >= 2)
        {
            StartCoroutine(PopUI(comboUIText.transform));
            combo++;
        }

        StartCoroutine(PopUI(hitTextUI.transform));
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

    public void HandleWin()
    {
        float accuracy = (float)hit / musicManager.SpawnCount;
        int star = 0;

        if (accuracy >= 0.95f)
        {
            star = 5;
        }
        else if (accuracy >= 0.85f)
        {
            star = 4;
        }
        else if (accuracy >= 0.75f)
        {
            star = 3;
        }
        else if (accuracy >= 0.65f)
        {
            star = 2;
        }
        else if (accuracy >= 0.60f)
        {
            star = 1;
        }

        if (star > stars.Length)
        {
            Debug.Log("star is more than stars UI!");
            return;
        }

        for (int x = 0; x < stars.Length; x++)
        {
            stars[x].SetActive(false);
        }
        for (int x = 0; x < star; x++)
        {
            stars[x].SetActive(true);
        }

        winPanel.SetActive(true);
        Debug.Log("accuracy: " + accuracy);
    }


    public IEnumerator PopUI(Transform target)
    {
        Vector3 original = target.localScale;
        Vector3 bigger = original * 1.2f;

        target.localScale = bigger;
        yield return new WaitForSeconds(0.1f);
        target.localScale = original;
    }

    
}
