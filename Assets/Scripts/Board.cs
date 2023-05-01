using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    
     public int width ;
     public int height ;
    private string gemColors;

    public GameObject backgroundTilePrefab;

    public Gem[] gems;

    public Gem[,] allGems;

    public float gemSpeed;

    private MatchFinder matchFind;

    private RoundManager roundMan; // sil

    private CameraPositioner cameraPositioner;

    public LevelLoader levelLoader;
    


    private void Awake()
    {
        matchFind = FindObjectOfType<MatchFinder>();
        roundMan = FindObjectOfType<RoundManager>();
        cameraPositioner = FindObjectOfType<CameraPositioner>();

        
       // levelLoader = FindObjectOfType<LevelLoader>();




    }
    void Start()
    {
        // Start a coroutine to wait for the levelLoader object to be found
        StartCoroutine(WaitForLevelLoader());
    }

    private IEnumerator WaitForLevelLoader()
    {
        // Wait until the levelLoader object is found
        while (levelLoader == null)
        {
            levelLoader = FindObjectOfType<LevelLoader>();
            yield return new WaitForSeconds(0.1f); // Wait for 0.1 seconds before checking again
        }

        // Load level data
        width = levelLoader.GetWidth();
        height = levelLoader.GetHeight();
        gemColors = levelLoader.GetColors();
        roundMan.remaininMoves = levelLoader.GetMoveCount();

        allGems = new Gem[width, height];
        Setup();
    }
    


    private void Update()
    {
        levelLoader = FindObjectOfType<LevelLoader>();
         matchFind.FindAllMatches();
    }
    private void Setup()
    {
        

        cameraPositioner.CameraMove(width, height); // Moves camera
        BgParentLocater bgparentlocater = FindObjectOfType<BgParentLocater>(); // Moves bg squares
        bgparentlocater.MoveBackgroundSquares(width, height);
        int a = 0;
        for (int i = 0; i < width; i++) // Creating the board
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 pos = new Vector2(i,j);
                GameObject bgTile = Instantiate(backgroundTilePrefab, pos, Quaternion.identity);
                bgTile.transform.parent = transform;
                bgTile.name = "BackGround Tile - " + i + " " + j;

                //int gemToUse = Random.Range(0, gems.Length); // Gives a random number to choose the gem
                int gemToUse = 0;
                
                if(gemColors[a] == 'g')
                {
                    gemToUse = 0;
                }
                else if (gemColors[a] == 'b')
                {
                    gemToUse = 1;
                }
                else if (gemColors[a] == 'r')
                {
                    gemToUse = 2;
                }
                else if (gemColors[a] == 'y')
                {
                    gemToUse = 3;
                }
                
                //Debug.Log(gemToUse);
                a += 2;

                SpawnGem(new Vector2Int(i, j), gems[gemToUse]);
            }
        }

    }
    private void SpawnGem(Vector2Int pos,Gem gemToSpawn)
    {
        Gem spawnedGem = Instantiate(gemToSpawn, new Vector3(pos.x, pos.y, 0f), Quaternion.identity);
        spawnedGem.transform.parent = this.transform;
        spawnedGem.name = "Gem - " + pos.x + " " + pos.y;
        allGems[pos.x, pos.y] = spawnedGem;

        spawnedGem.SetupGem(pos, this);
    }

    public void matchesComplete() // ? ? ? ? 
    {
        for(int i = 0; i < matchFind.currentMatches.Count; i++)
        {
            if (matchFind.currentMatches[i] != null)
            {
                ScoreCheck(matchFind.currentMatches[i]);
            }
        }
    }

    public void ScoreCheck(Gem gemToCheck)
    {
        if (!gemToCheck.isMatched)
        {
            roundMan.currentScore += gemToCheck.scoreValue; // I first add the value then change the ismatched to true so it's added once
        }
        
    }
}
