using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UImanager : MonoBehaviour
{
    public TMP_Text movesText;
    public TMP_Text scoreText;

    public GameObject endScreenImage;
    public TextMeshProUGUI endScreenScoreText;
    
    public void updateRoundScore(int score)
    {
        endScreenScoreText.text = score.ToString();
    }
}
