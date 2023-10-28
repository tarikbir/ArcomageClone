using ArcomageClone.Cards;
using System;
using UnityEngine;

namespace ArcomageClone
{
    public class GameManager : SingletonMB<GameManager>
    {
        public bool IsTestBuild;

        public CardMB CardMBPrefab;
    }
}