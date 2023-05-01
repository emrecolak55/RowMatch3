using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBGSize : MonoBehaviour
{
    public float edge; // 0.1f 0.2f
    private Board board;
    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }



    // Update is called once per frame
    void Update()
    {
        
        this.GetComponent<SpriteRenderer>().size = new Vector2(board.width + edge, board.height + edge);
    }
}
