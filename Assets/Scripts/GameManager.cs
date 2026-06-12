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
    //[SerializeField] GameObject[] stars;
    [SerializeField] GameStateManager stateManager;
    [SerializeField] Image[] hp;
    [SerializeField] TextMeshProUGUI multiplyerX;
    [SerializeField] Image onFire;

    //end
    [SerializeField] TextMeshProUGUI scoreEnd;
    [SerializeField] TextMeshProUGUI rank;

    public int missLost = 3;
    public int maxMult = 16;

    private int streak = 0;
    private int combo = 0;
    private int hit = 0;
    private int miss = 0;
    private int score;
    private int mult = 0;

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
        mult = 0;

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
        //1. add hit
        hit++;

        //2. add streak
        streak++;

        //3. points
        int points = 2;

        //4. if streak is more than 2 add combo
        if (streak >= 2)
        {
            StartCoroutine(PopUI(comboUIText.transform));

            mult = Mathf.Min(mult + 2, maxMult);
            combo++;
            points *= mult;
        }

        //5. add score
        score += points;

        //6. pop ui
        StartCoroutine(PopUI(scoreTextUI.transform));

        //7. update ui
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
        if(scoreTextUI == null || missTextUI == null || comboUIText == null || multiplyerX == null || onFire == null)
        {
            Debug.Log("missing UI!");
            return;
        }

        float t = mult / 16f;
        Color gold = new Color(1f, 0.84f, 0f);
        multiplyerX.color = Color.Lerp(Color.white, gold, t);

        if(mult >= 10)
        {
            onFire.gameObject.SetActive(true);
        }
        else
        {
            onFire.gameObject.SetActive(false);
        }

        comboUIText.text = $"Combo: {combo}";
        multiplyerX.text = $"X{mult}";

        if (combo <= 0)
        {
            comboUIText.gameObject.SetActive(false);
            multiplyerX.gameObject.SetActive(false);
        }
        else
        {
            comboUIText.gameObject.SetActive(true);
            multiplyerX.gameObject.SetActive(true);
        }

        scoreTextUI.text = $"Score: {score}";
        scoreEnd.text = $"Score: {score}";
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

        if (accuracy >= 0.95f)
        {
            rank.text = "Rank: S";
        }
        else if (accuracy >= 0.85f)
        {
            rank.text = "Rank: A";
        }
        else if (accuracy >= 0.75f)
        {
            rank.text = "Rank: B";
        }
        else if (accuracy >= 0.65f)
        {
            rank.text = "Rank: C";
        }
        else if (accuracy >= 0.60f)
        {
            rank.text = "Rank: D";
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
