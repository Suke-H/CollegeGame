using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    GameObject GameResult;
    public bool GameOverFlag = false;
    public bool GameClearFlag = false;

    void Start()
    {
        this.GameResult = GameObject.Find("GameResult");
    }

    void Update()
    {
        
    }

    //ゲームオーバー処理
    public void GameOver()
    {
        this.GameResult.GetComponent<Text>().text = "GAME OVER...";
        this.GameOverFlag = true;
    }

    //ゲームオーバー処理
    public void GameClear()
    {
        this.GameResult.GetComponent<Text>().text = "GAME CLEAR!!!";
        this.GameClearFlag = true;
    }
}
