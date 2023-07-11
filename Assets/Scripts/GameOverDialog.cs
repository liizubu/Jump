using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverDialog : Dialog
{
    public Text totalScoreTxt;
    public Text bestScoreTxt;
    public override void Show(bool isShow)
    {
        base.Show(isShow);
        Time.timeScale = 0;

        if (totalScoreTxt && GameManager.Ins)
        {
            totalScoreTxt.text = GameManager.Ins.Score.ToString();
        }

        if (bestScoreTxt)
        {
            bestScoreTxt.text = Pref.bestScore.ToString();
        }

        Debug.Log("Best score " + bestScoreTxt.text);
    }
    //public override void Show(bool isShow)
    //{
    //    base.Show(isShow);
    //    Time.timeScale = 0f;
    //}
    public void RePlay()
    {

        Time.timeScale = 1;
        SceneManager.sceneLoaded += OnSceneLoadEvent;
        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }

    private void OnSceneLoadEvent(Scene scene, LoadSceneMode mode)
    {
        if (GameManager.Ins)
        {
            GameManager.Ins.PlayGame();
        }
        SceneManager.sceneLoaded -= OnSceneLoadEvent;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;

        if (SceneController.Ins)
        {
            SceneController.Ins.LoadCurrentScene();
        }
    }
}

