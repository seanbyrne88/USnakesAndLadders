using System.Collections;
using System.Collections.Generic;
using BoardGame;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public Board B;
    private int NumberOfPlayers;
    public List<Player> Players;
    public GameObject PlayerGameObject;

    private void Start()
    {
        B = FindObjectOfType<Board>();
    }

    public void StartGame(int BoardWidth, int BoardHeight, int NumberOfPlayers)
    {
        B.BoardHeight = BoardHeight;
        B.BoardWidth = BoardWidth;
        this.NumberOfPlayers = NumberOfPlayers;
        B.Init();
        InitPlayers();
        
    }

    private void InitPlayers()
    {
        print("Initializing Players");
        for(int i = 0; i < NumberOfPlayers; i++)
        {
            GameObject p = Instantiate(PlayerGameObject);
            Player pl = p.GetComponent<Player>();
            pl.CurrentSpace = B.GetSpaceByIndex(1);
            pl.InitPlayer(i+1);
        }
    }
}
