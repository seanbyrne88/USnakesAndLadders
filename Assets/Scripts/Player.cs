using System.Collections;
using System.Collections.Generic;
using BoardGame;
using UnityEngine;


public class Player : MonoBehaviour {

    public string PlayerName;
    public bool IsCurrentTurn;
    public GameObject CurrentSpace;
    public int Index;
    public PlayerSnapTo SnapTo;
    public float SnapToOffset = .1f;

    public void InitPlayer(int PlayerIndex)
    {
        Index = PlayerIndex;
        GetComponent<SpriteRenderer>().color = GetColor();
        PlayerName = string.Format("Player{0}", PlayerIndex);
        SnapTo = GetSnapTo();
        SetPosition();
    }

    private void SetPosition()
    {
        Vector2 SpacePosition = CurrentSpace.transform.position;
        switch(SnapTo)
        {
            case PlayerSnapTo.TopLeft:
                {
                    transform.position = new Vector2(SpacePosition.x - SnapToOffset, SpacePosition.y + SnapToOffset);
                    break;
                }
            case PlayerSnapTo.TopRight:
                {
                    transform.position = new Vector2(SpacePosition.x + SnapToOffset, SpacePosition.y + SnapToOffset);
                    break;
                }
            case PlayerSnapTo.BottomLeft:
                {
                    transform.position = new Vector2(SpacePosition.x - SnapToOffset, SpacePosition.y - SnapToOffset);
                    break;
                }
            case PlayerSnapTo.BottomRight:
                {
                    transform.position = new Vector2(SpacePosition.x + SnapToOffset, SpacePosition.y - SnapToOffset);
                    break;
                }
        }
    }

    private Color GetColor()
    {
        switch(Index)
        {
            case 1: { return Color.red; }
            case 2: { return Color.blue; }
            case 3: { return Color.green; }
            case 4: { return Color.magenta; }
            default: { return Color.white; }
        }
    }

    private PlayerSnapTo GetSnapTo()
    {
        switch(Index)
        {
            case 1:
                {
                    return PlayerSnapTo.TopLeft;
                }
            case 2:
                {
                    return PlayerSnapTo.TopRight;
                }
            case 3:
                {
                    return PlayerSnapTo.BottomLeft;
                }
            case 4:
                {
                    return PlayerSnapTo.BottomRight;
                }
            default:
                {
                    return PlayerSnapTo.TopRight;
                }
        }
    }

    public enum PlayerSnapTo
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }
}
