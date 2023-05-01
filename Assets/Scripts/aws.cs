using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

public class aws : MonoBehaviour
{
    public string dataURL = "https://row-match.s3.amazonaws.com/levels/RM_A11";

    // Variables to hold the downloaded data
    public int[] grid;
    public int maxMoves;

    private void Start()
    {
        StartCoroutine(DownloadData());
    }


    IEnumerator DownloadData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(dataURL))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to download data: {request.error}");
            }
            else
            {
                // Split the data by newline characters and extract the values
                string[] lines = request.downloadHandler.text.Split('\n');
                int levelNumber = int.Parse(lines[0].Split(':')[1].Trim());
                int gridWidth = int.Parse(lines[1].Split(':')[1].Trim());
                int gridHeight = int.Parse(lines[2].Split(':')[1].Trim());
                int maxMoves = int.Parse(lines[3].Split(':')[1].Trim());
                string[] gridData = lines[4].Split(':');
                string[] gridValues = gridData[1].Split(',');


                // Convert the grid data to a 1D int array
                int[] grid = new int[gridWidth * gridHeight];
                for (int i = 0; i < gridHeight; i++)
                {
                    for (int j = 0; j < gridWidth; j++)
                    {
                        string cellValue = gridValues[i * gridWidth + j].Trim();
                        if (cellValue == "r")
                        {
                            grid[i * gridWidth + j] = 1;
                        }
                        else if (cellValue == "g")
                        {
                            grid[i * gridWidth + j] = 2;
                        }
                        else if (cellValue == "y")
                        {
                            grid[i * gridWidth + j] = 3;
                        }
                        else if (cellValue == "b")
                        {
                            grid[i * gridWidth + j] = 4;
                        }
                    }
                }

                // Assign the values to the class variables
                this.grid = grid;
                this.maxMoves = maxMoves;

                Debug.Log(grid[0]);
                Debug.Log(maxMoves);
            }
        }
    }

}
