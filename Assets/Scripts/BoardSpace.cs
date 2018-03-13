using System;
using UnityEngine;

namespace BoardGame
{
    public class BoardSpace : MonoBehaviour
    {
        public int Index;
        public BoardSpaceProperty SpaceProperty;
    }


    [Serializable]
    public class BoardSpaceProperty
    {
        public int ConnectedIndex;
        public BoardSpacePropertyType Type;
    }

    public enum BoardSpacePropertyType
    {
        None,
        Snake,
        Ladder
    }
}
