using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    
    private UImanager uiMan;

    public int currentScore;
    public float displayScore;
    public float scoreSpeed = 5;

    public int remaininMoves = 10;

    public LevelManager levelManager;

    private void Awake()
    {
        uiMan = FindObjectOfType<UImanager>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {

          if(remaininMoves <= 0)
            {
                remaininMoves = 0;
                WinCheck();
            }
            

        
        uiMan.movesText.text = remaininMoves.ToString();
      
        uiMan.scoreText.text = currentScore.ToString("0");
    }

    private void WinCheck()
    {
        // If it's the highest score, it goes to main screen
        levelManager.saveRoundScore(currentScore);
        StartCoroutine(EndRound()); // Finishes the round without a high score, displays the score for some seconds
        

    }
    IEnumerator EndRound()
    {
        
        
        uiMan.updateRoundScore(currentScore);
        uiMan.endScreenImage.SetActive(true);
        yield return new WaitForSeconds(3);
        levelManager.goMainScreen();
    }
    public void DecreaseRemainingMoves()
    {
        remaininMoves--;
    }

    public void highestScoreScreen()
    {
        
    }
}
