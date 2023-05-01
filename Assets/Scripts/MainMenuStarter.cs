using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuStarter : MonoBehaviour
{
    public GameObject highestScoreBGImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void displayHighestScoreBG()
    {
        highestScoreBGImage.SetActive(true);
    }
}
