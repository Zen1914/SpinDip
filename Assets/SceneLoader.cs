using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PausedGame() { NextSceneGameState(GameStates.PausedGame); }
    public void InGame() { NextSceneGameState(GameStates.InGame); }
    public void LevelUp() { NextSceneGameState(GameStates.LevelUp); }
    public void EndGame() { NextSceneGameState(GameStates.EndGame); }
    public void InTutorial() { NextSceneGameState(GameStates.InTutorial); }

    private void NextSceneGameState(GameStates newState)
    {
        GameStateHolder.NextState = newState;
    }

    public void LoadScene(string scene)
    {
        string sceneNoSpace = scene.Replace(" ", "");
        SceneManager.LoadScene(sceneNoSpace);
    }
}
