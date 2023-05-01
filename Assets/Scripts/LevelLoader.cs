using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
public class LevelLoader : MonoBehaviour
{
    public int[] levelNumber ;
    public int[] gridWidth;
    public int[] gridHeight;
    public int[] moveCount ;
    public string[] gridData ;
    private int sceneIndex;
    private int totalSceneNumber = 25;


    public string dataURL;
    public int[] grid;

    
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("LevelLoader");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        gridWidth = new int[25];
        levelNumber = new int[25];
        gridHeight = new int[25];
        moveCount = new int[25];
        gridData = new string[25];
    }
    private async void Start()
    {
        await ReadAllData();
    }

    /* private void Start()
     {


         //sceneIndex = SceneManager.GetActiveScene().buildIndex;
         //dataURL = "https://row-match.s3.amazonaws.com/levels/RM_A" + sceneIndex;
          if (sceneIndex <= 10)
          {
              ReadDataFromFile();


          }
          else
          {
              ReadDataOnline();
          }
          ReadAllData();

          }*/
    private void Update()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private async Task ReadAllData()
    {
        for(int i = 0;  i < totalSceneNumber; i++)
        {
            
            //dataURL = "https://row-match.s3.amazonaws.com/levels/RM_A" + (i+1); // For every scene data

            if (i < 10)
            {
                ReadDataFromFile(i);
                //dataURL = "https://row-match.s3.amazonaws.com/levels/RM_A" + (i + 1);

            }
            else if ( i >= 10 && i <= 14)
            {
                //Debug.Log("HERE IM HERE IM HERE IM HERE IM HERE");
               // dataURL = "https://row-match.s3.amazonaws.com/levels/RM_A" + (i + 1);
                await ReadDataOnline(i);
            }
            else
            {
                //dataURL = "https://row-match.s3.amazonaws.com/levels/RM_B" + (i - 14);
               await ReadDataOnline(i);
            }
        }
        
    }
    

    private async Task ReadDataOnline(int index)
    {
        await DownloadData(index);

    }


    private async Task DownloadData(int index)
    {
        dataURL = "https://row-match.s3.amazonaws.com/levels/RM_A" + (index + 1);
        if(index > 14)
        {
            dataURL = "https://row-match.s3.amazonaws.com/levels/RM_B" + (index - 14);
        }
        using (UnityWebRequest request = UnityWebRequest.Get(dataURL))
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }
            //await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to download data: {request.error}");
            }
            else
            {
                // Split the data by newline characters and extract the values
                string[] lines = request.downloadHandler.text.Split('\n');
                int levelNumber2 = int.Parse(lines[0].Split(':')[1].Trim());
                levelNumber[index] = levelNumber2;
                int gridWidth2 = int.Parse(lines[1].Split(':')[1].Trim());
                gridWidth[index] = gridWidth2;
                int gridHeight2 = int.Parse(lines[2].Split(':')[1].Trim());
                gridHeight[index] = gridHeight2;
                int maxMoves = int.Parse(lines[3].Split(':')[1].Trim());
                moveCount[index] = maxMoves;
                string[] gridData = lines[4].Split(':');
                string[] gridValues = gridData[1].Split(',');
               // Debug.Log(dataURL);
                //Debug.Log("WIDTH:::::::::::::::" + gridWidth2);
                // Convert the grid data to a 1D int array
                string cellValue = "";
                for (int i = 0; i < gridHeight2; i++)
                {
                    for (int j = 0; j < gridWidth2; j++)
                    {
                        
                        cellValue += gridValues[i * gridWidth2 + j].Trim() + ",";
                    }
                }
                //Debug.Log(cellValue);
                // Assign the values to the class variables
                this.gridData[index] = cellValue;
                this.moveCount[index] = maxMoves;
            }
        }
    }


    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////////////////
    /// </summary>

    private void ReadDataFromFile(int i)
    {

        // Read the file contents into a string
        string fileContent = System.IO.File.ReadAllText("Assets/LevelInputs/RM_A" + (i + 1 ));

        // Split the string into lines
        string[] lines = fileContent.Split('\n');

        // Parse the data from the lines
        foreach (string line in lines)
        {
            string[] parts = line.Split(':');
            string key = parts[0].Trim();
            string value = parts[1].Trim();

            switch (key)
            {
                case "level_number":
                    levelNumber[i] = int.Parse(value);
                    break;
                case "grid_width":
                    gridWidth[i] = int.Parse(value);
                    break;
                case "grid_height":
                    gridHeight[i] = int.Parse(value);
                    break;
                case "move_count":
                    moveCount[i] = int.Parse(value);
                    break;
                case "grid":
                    gridData[i] = value;
                   // Debug.Log(gridData[i]);
                    break;
            }
        }
    }

    public int GetLevelNumber()
    {
        return levelNumber[sceneIndex-1];
    }
    
    public int GetWidth()
    {
       
        return gridWidth[sceneIndex - 1];

    }

    
    public int GetHeight()
    {
        return gridHeight[sceneIndex - 1];
    }
    public int GetMoveCount()
    {
        return moveCount[sceneIndex - 1];
    }
    public string GetColors()
    {
       // Debug.Log("I GIVE THIS : " + gridData[sceneIndex - 1]);
        return gridData[sceneIndex - 1];
    }
}
