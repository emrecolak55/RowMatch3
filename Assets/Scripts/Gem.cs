using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    [HideInInspector]
    public Vector2Int posIndex;
    [HideInInspector]  public Board board;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;

    private bool mousePressed;
    private float swipeAngle = 0;

    private Gem otherGem;

    public enum GemType { blue, green ,red, yellow}
    public GemType type;
    public bool isMatched = false;

    public int scoreValue = 10;
    

    //public SpriteRenderer gemSpriteRenderer; // dasdasdasdasdasds
    public Sprite matchedGemSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Vector2.Distance(transform.position, posIndex) > .01f) // to prevent infinite movement
        {
            transform.position = Vector2.Lerp(transform.position, posIndex, board.gemSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector3(posIndex.x, posIndex.y, 0f);
            board.allGems[posIndex.x, posIndex.y] = this;
        }
        
        if (mousePressed && Input.GetMouseButtonUp(0))
        {
            mousePressed = false;
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    public void SetupGem( Vector2Int pos, Board theBoard)
    {
        posIndex = pos;
        board = theBoard;

    }

    private void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePressed = true;

    }
    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x);
        swipeAngle = swipeAngle * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);
        
        if(Vector3.Distance(firstTouchPosition, finalTouchPosition) > .5f)
        {
            if (!isMatched)
            {
                MovePieces();
            }
            
        }
        
    }

    private void MovePieces()
    {
        if( swipeAngle < 45 && swipeAngle > -45 && posIndex.x < board.width - 1)
        {
            otherGem = board.allGems[posIndex.x + 1, posIndex.y];
            
            otherGem.posIndex.x--;
            posIndex.x++;
            FindObjectOfType<RoundManager>().DecreaseRemainingMoves();
        }
        else if(swipeAngle > 45 && swipeAngle <= 135 && posIndex.y < board.height - 1)  {
            
            otherGem = board.allGems[posIndex.x, posIndex.y + 1];
            if (!otherGem.isMatched) // if its not a completed row
            {
                otherGem.posIndex.y--;
                posIndex.y++;
            }
            FindObjectOfType<RoundManager>().DecreaseRemainingMoves();
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && posIndex.y > 0)
        {
            otherGem = board.allGems[posIndex.x, posIndex.y - 1];
            if (!otherGem.isMatched) // if its not a completed row
            {
                otherGem.posIndex.y++;
                posIndex.y--;
            }
            FindObjectOfType<RoundManager>().DecreaseRemainingMoves();
        }
        else if (swipeAngle > 135 || swipeAngle < -135 && posIndex.x > 0)
        {
            otherGem = board.allGems[posIndex.x - 1, posIndex.y];
            otherGem.posIndex.x++;
            posIndex.x--;
            FindObjectOfType<RoundManager>().DecreaseRemainingMoves();
        }
        board.allGems[posIndex.x, posIndex.y] = this;
        board.allGems[otherGem.posIndex.x, otherGem.posIndex.y] = otherGem;
    }

    public void changeGemSprite()
    {
       //gemSpriteRenderer = FindObjectOfType<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().sprite = matchedGemSprite;
        //gemSpriteRenderer.sprite = matchedGemSprite;

    }
}
