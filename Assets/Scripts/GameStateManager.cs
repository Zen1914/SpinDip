using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameStates CurrentGameState => currentGameState;
    public GameStateTimeScale[] gameStateTimeScale;
    private GameStates currentGameState;

    private void Start()
    {
        ChangeGameState(GameStateHolder.NextState);
    }

    public void PausedGame() { ChangeGameState(GameStates.PausedGame); }
    public void InGame() { ChangeGameState(GameStates.InGame); }
    public void LevelUp() { ChangeGameState(GameStates.LevelUp); }
    public void EndGame() { ChangeGameState(GameStates.EndGame); }
    public void InTutorial() { ChangeGameState(GameStates.InTutorial); }


    public void ChangeGameState(GameStates newState)
    {
        foreach (var item in gameStateTimeScale)
        {
            if (item.gameState == newState)
            {
                currentGameState = newState;
                Time.timeScale = item.timeScale;
                Debug.LogWarning("GameState: " + currentGameState + " TimeScale: " + Time.timeScale);
                return;
            }
        }
        Time.timeScale = 1f;
    }
}

public class GameStateHolder
{
    public static GameStates NextState = GameStates.InGame;
}

public enum GameStates
{
    InGame,
    InTutorial,
    PausedGame,
    LevelUp,
    EndGame
}

[System.Serializable]
public class GameStateTimeScale
{
    public GameStates gameState;
    public float timeScale;
}