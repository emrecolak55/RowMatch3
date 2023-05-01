using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    private Animator myAnimator;
    public bool isUnlocked = false;
    public string previouslevelnameofbutton;
    public string thislevelname;
    private LevelManager levelManager;
    public int x = 1;

    public TextMeshProUGUI highestScoreText;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        myAnimator = GetComponent<Animator>();
        //Debug.Log(PlayerPrefs.GetInt(previouslevelnameofbutton + "highscore", 0));

        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager.IsLevelPlayed(previouslevelnameofbutton) && isUnlocked == false)
        {
            // If the level is played already, play the animation
            
            ChangeLockAnim();

        }
        //Debug.Log(levelManager.getHighScoreByIndex(thislevelname));
        highestScoreText.text = levelManager.getHighScoreByIndex(thislevelname).ToString();

    }
    
    public void ChangeLockAnim()
    {
        

        if(x == 1)
        {
            
            myAnimator.SetTrigger("ChangeLock");
        }
        
        x++;
        isUnlocked = true;

        
    }

    

    
}
