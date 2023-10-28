using ArcomageClone.Cards;
using ArcomageClone.Units;
using UnityEngine;

namespace ArcomageClone
{
    public class PlayerManager : SingletonMB<PlayerManager>
    {
        public Player Player;
        public Player Enemy;

        public Player TestSidePrefab;

        internal Player GetEnemy(Player owner)
        {
            return owner == Player ? Enemy : Player;
        }
    }
}