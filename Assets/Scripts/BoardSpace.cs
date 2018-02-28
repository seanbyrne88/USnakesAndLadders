using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public class BoardSpace : MonoBehaviour
    {
        public int Index;
        public BoardSpaceProperty SpaceProperty;
    }


    public class BoardSpaceProperty
    {
        public int ConnectedIndex;
        public BoardSpacePropertyType Type;
    }

    public enum BoardSpacePropertyType
    {
        Snake,
        Ladder
    }
}
