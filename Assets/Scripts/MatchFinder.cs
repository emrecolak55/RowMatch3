using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchFinder : MonoBehaviour
{
    private Board board;
    public List<Gem> currentMatches = new List<Gem>();
    private bool rowCompleteCheck = false;
    private void Awake()
    {
        board = FindObjectOfType<Board>();
    }
    public void FindAllMatches()
    {
        //currentMatches.Clear();
        
            for (int y = 0; y < board.height; y++)
            {
            rowCompleteCheck = true;
            Gem currentGem = board.allGems[0, y];
                for( int x = 0; x < board.width; x++)
                {
                    Gem nextGem = board.allGems[x, y];
                    if( nextGem.type != currentGem.type)
                    {
                    rowCompleteCheck = false; // in case
                    }
                    else if(x == board.width-1 && nextGem.type == currentGem.type && rowCompleteCheck == true)
                    {
                    
                    for(int i = 0; i < board.width; i++)
                    {
                        board.allGems[i, y].changeGemSprite(); // change all the row's sprites                        
                        board.ScoreCheck(board.allGems[i, y]);
                        board.allGems[i, y].isMatched = true;
                    }
                    rowCompleteCheck = false;
                    }
                    
                     

                }

            }
        
        if(currentMatches.Count > 0)
        {
            currentMatches = currentMatches.Distinct().ToList();
        }
    }

}
