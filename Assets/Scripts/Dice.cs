using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoardGame
{
    public static class Dice
    {
        // Use this for initialization
        public static int Roll()
        {
            return Random.Range(1, 6);
        }

        public static int Roll(int NumberOfDice)
        {
            return Random.Range(1, (6 * NumberOfDice));
        }
    }
}

