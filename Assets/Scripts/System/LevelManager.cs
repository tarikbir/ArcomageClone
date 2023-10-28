using ArcomageClone.Cards;
using ArcomageClone.Units;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ArcomageClone
{
    public class LevelManager : SingletonMB<LevelManager>
    {
        public Castle CastlePrefab;
        public Wall WallPrefab;

        private List<CardSO> _levelDeckData;

        [SerializeField] private HorizontalLayoutGroup _cardHolder;

        [Header("Player References")]
        [SerializeField] private Vector2 _playerCastleSpawnPoint;
        [SerializeField] private TextMeshProUGUI _playerNameUI;
        [SerializeField] private TextMeshProUGUI _playerCastleHealthUI;
        [SerializeField] private TextMeshProUGUI _playerWallHealthUI;
        [SerializeField] private TextMeshProUGUI _playerBuilderGainUI;
        [SerializeField] private TextMeshProUGUI _playerBuilderUI;
        [SerializeField] private TextMeshProUGUI _playerMightGainUI;
        [SerializeField] private TextMeshProUGUI _playerMightUI;
        [SerializeField] private TextMeshProUGUI _playerMagicGainUI;
        [SerializeField] private TextMeshProUGUI _playerMagicUI;

        [Header("Enemy References")]
        [SerializeField] private Vector2 _enemyCastleSpawnPoint;
        [SerializeField] private TextMeshProUGUI _enemyNameUI;
        [SerializeField] private TextMeshProUGUI _enemyCastleHealthUI;
        [SerializeField] private TextMeshProUGUI _enemyWallHealthUI;
        [SerializeField] private TextMeshProUGUI _enemyBuilderGainUI;
        [SerializeField] private TextMeshProUGUI _enemyBuilderUI;
        [SerializeField] private TextMeshProUGUI _enemyMightGainUI;
        [SerializeField] private TextMeshProUGUI _enemyMightUI;
        [SerializeField] private TextMeshProUGUI _enemyMagicGainUI;
        [SerializeField] private TextMeshProUGUI _enemyMagicUI;

        private void Start()
        {
            if (GameManager.Instance.IsTestBuild)
            {
                Player player = new GameObject("PlayerSide").AddComponent<Player>();
                player.Castle = SpawnCastle(SideTargeter.Owner, "PlayerCastle");
                player.Wall = SpawnWall(SideTargeter.Owner, "PlayerWall");
                PlayerManager.Instance.Player = player;

                Player enemy = new GameObject("EnemySide").AddComponent<Player>();
                enemy.Castle = SpawnCastle(SideTargeter.Enemy, "EnemyCastle");
                enemy.Wall = SpawnWall(SideTargeter.Enemy, "EnemyWall");
                enemy.Castle.GetComponent<SpriteRenderer>().sprite = ResourceManager.Instance.GetSprite("evils");
                PlayerManager.Instance.Enemy = enemy;

                CreateLevelDeckAndShuffle();

                DrawCard(player);
                DrawCard(player);
                DrawCard(player);
                DrawCard(player);
                DrawCard(player);

                UpdateUIValues();
            }
        }

        public Castle SpawnCastle(SideTargeter side, string name = "Castle")
        {
            var castle = Instantiate(CastlePrefab, side == SideTargeter.Owner ? _playerCastleSpawnPoint : _enemyCastleSpawnPoint, Quaternion.identity);
            castle.name = name;
            return castle;
        }

        public Wall SpawnWall(SideTargeter side, string name = "Wall")
        {
            var wall = Instantiate(WallPrefab, side == SideTargeter.Owner ? _playerCastleSpawnPoint : _enemyCastleSpawnPoint, Quaternion.identity);
            wall.name = name;
            return wall;
        }

        public Card DrawCard(Player player)
        {
            if (_levelDeckData.Count == 0) throw new System.Exception("Deck ran out");
            CardSO firstCardData = _levelDeckData.First();
            Card card = new(firstCardData, player);
            CardMB cardMb = Instantiate(GameManager.Instance.CardMBPrefab, _cardHolder.transform);
            cardMb.SetCard(card);
            _levelDeckData.Remove(firstCardData);
            return card;
        }

        public void PlayCard(Card card)
        {
            _levelDeckData.Remove(card.Data);
            UpdateUIValues();
        }

        private void UpdateUIValues()
        {
            //Test string concat, remove later
            _playerCastleHealthUI.text = "Castle: " + PlayerManager.Instance.Player.Castle.Health.ToString();
            _playerWallHealthUI.text = "Wall: " + PlayerManager.Instance.Player.Wall.Health.ToString();
            _playerBuilderGainUI.text = PlayerManager.Instance.Player.BuilderGain.ToString();
            _playerBuilderUI.text = PlayerManager.Instance.Player.Builder.ToString();
            _playerMightGainUI.text = PlayerManager.Instance.Player.MightGain.ToString();
            _playerMightUI.text = PlayerManager.Instance.Player.Might.ToString();
            _playerMagicGainUI.text = PlayerManager.Instance.Player.MagicGain.ToString();
            _playerMagicUI.text = PlayerManager.Instance.Player.Magic.ToString();

            _enemyCastleHealthUI.text = "Castle: " + PlayerManager.Instance.Enemy.Castle.Health.ToString();
            _enemyWallHealthUI.text = "Wall: " + PlayerManager.Instance.Enemy.Wall.Health.ToString();
            _enemyBuilderGainUI.text = PlayerManager.Instance.Enemy.BuilderGain.ToString();
            _enemyBuilderUI.text = PlayerManager.Instance.Enemy.Builder.ToString();
            _enemyMightGainUI.text = PlayerManager.Instance.Enemy.MightGain.ToString();
            _enemyMightUI.text = PlayerManager.Instance.Enemy.Might.ToString();
            _enemyMagicGainUI.text = PlayerManager.Instance.Enemy.MagicGain.ToString();
            _enemyMagicUI.text = PlayerManager.Instance.Enemy.Magic.ToString();
        }

        private void CreateLevelDeckAndShuffle()
        {
            _levelDeckData = new();
            foreach (var card in ResourceManager.Instance.GetAllCards())
            {
                for (int i = 0; i < 5; i++)
                {
                    _levelDeckData.Add(card);
                }
            }

            _levelDeckData = _levelDeckData.OrderBy(c => Random.Range(0, _levelDeckData.Count)).ToList();

            foreach (var item in _levelDeckData)
            {
                Debug.Log(item.Name);
            }
        }
    }
}
