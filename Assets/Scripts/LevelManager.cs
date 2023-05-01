using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int roundScore;
    Scene thisScene;
    private string levelName;

    public GameObject highestScoreBG;
    public TextMeshProUGUI highScoreText;

    private int[] levelHighScores;


    private void Awake()
    {

        GameObject[] objs = GameObject.FindGameObjectsWithTag("LevelManager");

            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);
       
    }
    private void Start()
    {
        thisScene = SceneManager.GetActiveScene();
        levelName = thisScene.name;
        

    }
    private void Update()
    {
        thisScene = SceneManager.GetActiveScene(); // Change after ? ? ? ? ? ??
        levelName = thisScene.name;

    }
    public void saveRoundScore(int score)
    {
        
        roundScore = score;
        if(roundScore > PlayerPrefs.GetInt(levelName + "highscore", 0)  )
        {
            
            PlayerPrefs.SetInt(levelName+"highscore", roundScore); // sets the new high record
            PlayerPrefs.Save();
            SceneManager.LoadScene("MainMenu");
 
                highScoreText.text = score.ToString();
                StartCoroutine(ShowHighScore());
            

        }
        
    }

    IEnumerator ShowHighScore()
    {
        
        highestScoreBG.SetActive(true);
        
        yield return new WaitForSeconds(5);
        highestScoreBG.SetActive(false);

    }

    public void goMainScreen()
    {
        
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadSceneByButtons(int index)
    {
        if(index == 1)
        {
            SceneManager.LoadScene(index);
        }
        else if(index > 1 && index < 26)
        {
            if (PlayerPrefs.GetInt("Level" + (index - 1) + "highscore", 0) != 0)
            {
                SceneManager.LoadScene(index);
            }
        }
        else
        {
            Debug.LogError("Index of the level for button is outside the array");
        }
        
    }

    public bool IsLevelPlayed(string levelname)
    {
        //Debug.Log(levelname);
        if( PlayerPrefs.GetInt(levelname + "highscore", 0) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    public int getHighScoreByIndex(string levelname)
    {

        return PlayerPrefs.GetInt(levelname + "highscore", 0);
    }
}
