using System.Linq;
using System.Collections;
using System.Collections.Generic;
using BoardGame;
using UnityEngine;


public class GameManager : MonoBehaviour {

    public Board GameBoard;
    public int CurrentPlayerIndex;
    public List<Player> Players;
    public GameObject PlayerGameObject;
    public UIManager UIMan;

    private void Awake()
    {
        Players = new List<Player>();
        CurrentPlayerIndex = 0;
    }

    private void Start()
    {
        GameBoard = FindObjectOfType<Board>();
        UIMan = FindObjectOfType<UIManager>();
    }

    public Player CurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }

    public void StartGame(int BoardWidth, int BoardHeight, int NumberOfPlayers)
    {
        GameBoard.BoardHeight = BoardHeight;
        GameBoard.BoardWidth = BoardWidth;
        GameBoard.Init();

        InitPlayers(NumberOfPlayers);
        UIMan.RefreshCurrentPlayerText();
    }

    private void InitPlayers(int NumberOfPlayers)
    {
        for(int i = 0; i < NumberOfPlayers; i++)
        {
            GameObject p = Instantiate(PlayerGameObject);
            Player pl = p.GetComponent<Player>();
            pl.CurrentSpaceIndex = 1;
            pl.CurrentSpace = GameBoard.GetSpaceByIndex(pl.CurrentSpaceIndex); //set current space to first
            pl.SetColorAndPosition(i+1);

            Players.Add(pl);
        }
    }

    public void RollDiceAndMovePlayer()
    {
        int DiceRoll = Dice.Roll();
        UIMan.RefreshDiceRollText(DiceRoll);

        Player pl = CurrentPlayer();
        pl.CurrentSpaceIndex += DiceRoll;
        pl.CurrentSpace = GameBoard.GetSpaceByIndex(pl.CurrentSpaceIndex);
        pl.SetPosition();
        
        NextPlayer();
    }

    private void NextPlayer()
    {
        if (CurrentPlayerIndex >= Players.Count - 1)
        {
            CurrentPlayerIndex = 0;
        }
        else
        {
            CurrentPlayerIndex += 1;
        }

        UIMan.RefreshCurrentPlayerText();
    }
}
