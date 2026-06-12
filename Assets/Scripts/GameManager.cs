using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTextUI;
    [SerializeField] TextMeshProUGUI missTextUI;
    [SerializeField] TextMeshProUGUI comboUIText;
    [SerializeField] GameObject lostPanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] MusicManager musicManager;
    [SerializeField] GameObject[] stars;
    [SerializeField] GameStateManager stateManager;

    [SerializeField] Image[] hp;


    public int missLost = 3;

    int streak = 0;
    int combo = 0;
    private int hit = 0;
    private int miss = 0;

    private int score;

    private void Awake()
    {
        UpdateUI();
    }

    public void AddMiss()
    {
        miss++;

        UpdateHPUI();

        streak = 0;
        combo = 0;

        if (miss >= missLost)
        {
            HandleLost();
        }

        StartCoroutine(PopUI(missTextUI.transform));
        UpdateUI();
    }

    private void UpdateHPUI()
    {
        foreach(var item in hp)
        {
            if (item.enabled)
            {
                item.enabled = false;
                return;
            }
        }
    }


    public void AddHit()
    {
        hit++;
        streak++;

        int points = 100;

        if (streak >= 2)
        {
            StartCoroutine(PopUI(comboUIText.transform));
            combo++;
            points *= 2;
        }

        score += points;

        StartCoroutine(PopUI(scoreTextUI.transform));
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
        if(scoreTextUI == null || missTextUI == null || comboUIText == null)
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

        scoreTextUI.text = $"Score: {score}";
        missTextUI.text = $"Miss: {miss}";  
    }

    private void HandleLost()
    {
        stateManager.ChangeGameState(GameStates.EndGame);
        if(lostPanel == null)
        {
            Debug.Log("empty lostPanel!");
            return;
        }

        lostPanel.SetActive(true);
    }

    public void HandleWin()
    {
        stateManager.ChangeGameState(GameStates.EndGame);
        float accuracy = (float)hit / (float)musicManager.BeatCount;
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
