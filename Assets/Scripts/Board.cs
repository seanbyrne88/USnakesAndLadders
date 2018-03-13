using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame
{
    public class Board : MonoBehaviour
    {

        [HideInInspector]
        public List<GameObject> BoardSpaces;

        public Material SnakeMaterial;
        public Material LadderMaterial;

        public int BoardHeight;
        public int BoardWidth;

        public GameObject SpaceGameObject;

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
            //TODO: Move these, make UI driven, possibly to a board generation screen
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
                int Index = Random.Range(20, BoardSpaces.Count - 1); //start at 20 so nothing on first line
                Debug.Log(string.Format("placing snake at {0}", Index));

                var Space = GetSpaceByIndex(Index).GetComponent<BoardSpace>();
                var SpaceProp = Space.SpaceProperty;

                if(SpaceProp.Type != BoardSpacePropertyType.None)
                {
                    continue; // skip iteration if space already has property
                }

                SpaceProp.Type = BoardSpacePropertyType.Snake;
                SpaceProp.ConnectedIndex = Random.Range(0, Index - 10); //max is set to (Index - 10) so we go at least one row down on the board

                DrawSnake(Space.gameObject);
                CurrentSnakes++;
            }
        }

        void InitLadders(int Min, int Max, int MinLength, int MaxLength)
        {
            int CurrentLadders = 0;
            int LadderCount = Random.Range(Min, Max);

            while (CurrentLadders < LadderCount)
            {
                //choose spot at random on board, check if no property already exists, if not place a property
                int Index = Random.Range(20, BoardSpaces.Count - 1); //start at 20 so nothing on first line
                Debug.Log(string.Format("placing ladder at {0}", Index));

                var Space = GetSpaceByIndex(Index).GetComponent<BoardSpace>();
                var SpaceProp = Space.SpaceProperty;

                if (SpaceProp.Type != BoardSpacePropertyType.None)
                {
                    continue; // skip iteration if space already has property
                }

                SpaceProp.Type = BoardSpacePropertyType.Ladder;
                SpaceProp.ConnectedIndex = Random.Range(Index + 10, BoardHeight * BoardWidth); //mine is set to (Index + 10) so we go at least one row up on the board

                DrawLadder(Space.gameObject);
                CurrentLadders++;
            }
        }

        /// <summary>
        /// Draw line renderer for snake or ladder
        /// </summary>
        /// <param name="Space"></param>
        /// <returns></returns>
        LineRenderer DrawBoardProp(GameObject Space)
        {
            int ConnectedIndex = Space.GetComponent<BoardSpace>().SpaceProperty.ConnectedIndex;
            GameObject ConnectedSpace = GetSpaceByIndex(ConnectedIndex);

            var lr = Space.AddComponent<LineRenderer>();
            var pos0 = Space.transform.position;
            var pos1 = ConnectedSpace.transform.position;

            lr.SetPosition(0, pos0);
            lr.SetPosition(1, pos1);

            lr.startWidth = 0.1f;
            lr.endWidth = 0.1f;

            return lr;
        }

        void DrawSnake(GameObject SnakeSpace)
        {
            var lr = DrawBoardProp(SnakeSpace);

            lr.startColor = Color.red;
            lr.endColor = Color.red;
            lr.material = SnakeMaterial;
        }

        void DrawLadder(GameObject LadderSpace)
        {
            var lr = DrawBoardProp(LadderSpace);

            lr.startColor = Color.green;
            lr.endColor = Color.green;
            lr.material = LadderMaterial;
        }
        
        /// <summary>
        /// Draws Space on board object, sets Index based on x,y. Adds Space script, TextMesh for rendering position on screen
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        void CreateAndDrawSpace(int x, int y)
        {
            int SpaceIndex = GetSpaceIndexByPos(x, y);
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

        /// <summary>
        /// Because Snakes and Ladders goes left to right, up, right to left in sequence, the index will not identical to the list index, for references by board index, use Space.Index or this method
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        int GetSpaceIndexByPos(int x, int y)
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

        public GameObject GetSpaceByIndex(int SpaceIndex)
        {
            return BoardSpaces.Where(x => x.GetComponent<BoardSpace>().Index == SpaceIndex).First();
        }
    }
}
