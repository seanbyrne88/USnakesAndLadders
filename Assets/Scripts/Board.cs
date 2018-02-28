using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {

        [HideInInspector] //no need to display this
        public List<GameObject> BoardSpaces;

        public int BoardHeight;
        public int BoardWidth;

        public GameObject SpaceGameObject;

        public GameObject GetSpaceByIndex(int SpaceIndex)
        {
            return BoardSpaces.Where(x => x.GetComponent<BoardSpace>().Index == SpaceIndex).First();
        }

        /// <summary>
        /// Initializes gameboard based on public members BoardWidth and BoardHeight
        /// </summary>
        public void Init()
        {
            BoardSpaces = new List<GameObject>();
            DrawBoard();
            InitSnakesAndLadders();
        }

        void InitSnakesAndLadders()
        {
            //TODO: Move these, possibly to a board generation screen
            int MinSnakes = 3;
            int MaxSnakes = 5;
            int MinSnakeLength = 15;
            int MaxSnakeLength = 20;

            int MinLadders = 1;
            int MaxLadders = 3;
            int MinLadderLength = 15;
            int MaxLadderLength = 20;
            
            InitSnakes(MinSnakes, MaxSnakes, MinSnakeLength, MaxSnakeLength);
            InitLadders(MinLadders, MaxLadders, MinLadderLength, MaxLadderLength);
        }

        void InitSnakes(int Min, int Max, int MinLength, int MaxLength)
        {
            int CurrentSnakes = 0;
            int SnakeCount = Random.Range(Min, Max);

            while (CurrentSnakes < SnakeCount)
            {
                //choose spot at random on board, check if no property already exists, if not place a property
                int Index = Random.Range(20, BoardSpaces.Count - 1);
                print("placing snake at " + Index);

                CurrentSnakes++;
            }
        }

        void InitLadders(int Min, int Max, int MinLength, int MaxLength)
        {
            
        }

        /// <summary>
        /// Draws Space on board object, sets Index based on x,y. Adds Space script, TextMesh for rendering position on screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void CreateAndDrawSpace(int x, int y)
        {
            int SpaceIndex = GetSpaceIndex(x, y);
            GameObject Space = Instantiate(SpaceGameObject, transform);
            Space.name = string.Format("BoardSpace{0}", SpaceIndex);
            Space.GetComponent<BoardSpace>().Index = SpaceIndex;
            Space.transform.position = new Vector2(x, y);
            Space.GetComponent<SpriteRenderer>().color = (y + x) % 2 == 0 ? Color.black : Color.white;
            TextMesh tm = Space.GetComponentInChildren<TextMesh>();
            tm.text = SpaceIndex.ToString();
            tm.color = (y + x) % 2 == 0 ? Color.white : Color.black;
            BoardSpaces.Add(Space);
        }


        /// <summary>
        /// Loops through width and height and draws spaces
        /// </summary>
        void DrawBoard()
        {
            for (int y = 0; y < BoardHeight; y++)
            {
                for (int x = 0; x < BoardWidth; x++)
                {
                    CreateAndDrawSpace(x, y);
                }
            }
        }

        void DrawSnakesAndLadders()
        {

        }

        /// <summary>
        /// Because Snakes and Ladders goes left to right, up, right to left in sequence, the index will not identical to the list index, for references by board index, use Space.Index or this method
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int GetSpaceIndex(int x, int y)
        {
            if (y % 2 == 0)
            {
                return x + (y * BoardWidth) + 1;
            }
            else
            {
                return BoardWidth - x + (y * BoardWidth);
            }

        }


    }

}
