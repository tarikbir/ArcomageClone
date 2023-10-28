using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ArcomageClone.Cards
{
    public class CardMB : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Card Card { get; private set; }

        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _graphicsObject;

        [Header("Settings")]
        [SerializeField] private float _normalScale = 1f;
        [SerializeField] private float _hoverScale = 1.12f;
        [SerializeField] private float _scaleTweenDuration = .33f;
        private Sequence _scalerSequence;

        public void SetCard(Card card)
        {
            Card = card;

            UpdateCard();
        }

        private void UpdateCard()
        {
            _title.text = Card.Name;
            _description.text = Card.Description;
            _icon.sprite = Card.Icon;
        }

        

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Card.CanOwnerPayCost())
            {
                Card.PayCost();
                Card.Effect?.Invoke();
                LevelManager.Instance.PlayCard(Card);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Can't play card: " + Card.Name);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _scalerSequence = DOTween.Sequence();
            _scalerSequence.Append(_graphicsObject.transform.DOScale(_hoverScale*1.1f, _scaleTweenDuration/3))
                .Join(_graphicsObject.transform.DOScaleY(_hoverScale*1.2f, _scaleTweenDuration/3))
                .Join(_graphicsObject.transform.DOScaleX(_hoverScale*0.8f, _scaleTweenDuration/3))
                .Append(_graphicsObject.transform.DOScale(_hoverScale, _scaleTweenDuration/3));

            _scalerSequence.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _graphicsObject.transform.DOScale(_normalScale, _scaleTweenDuration).SetEase(Ease.OutBounce).SetAutoKill(true);
        }
    }
}