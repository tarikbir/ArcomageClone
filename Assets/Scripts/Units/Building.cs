using DG.Tweening;
using System;
using UnityEngine;

namespace ArcomageClone.Units
{
    public class Building : MonoBehaviour
    {
        [SerializeField] protected float _minimumYPosition = -4.5f;
        [SerializeField] protected float _maximumYPosition = -0.5f;
        [SerializeField] protected int _maximumHealth = 100;
        [SerializeField] protected int _startingHealth = 25;
        [SerializeField] protected float _tweenDuration = 0.33f;
        [SerializeField] protected Ease _easeType = Ease.InOutBack;

        public Action OnChange;
        public Action OnDestroyed;

        public int Health
        {
            get
            {
                return _health;
            }

            set
            {
                _health = Mathf.Min(value, _maximumHealth);
                if (_health > 0)
                {
                    SetYPosition(GetBuildingHeightForHealth(_health));
                    OnChange?.Invoke();
                }
                else
                {
                    OnDestroyed?.Invoke();
                }
            }
        }
        protected int _health;

        protected virtual void Awake()
        {
            _health = _startingHealth;
        }

        protected virtual void Start()
        {
            Health = _health; //Just to set correct height
        }

        private void SetYPosition(float y)
        {
            transform.DOMoveY(y, _tweenDuration).SetEase(_easeType).SetAutoKill(true);
        }

        private float GetBuildingHeightForHealth(int health)
        {
            float currentPercentage = (float)health / _maximumHealth;
            return (currentPercentage * (_maximumYPosition - _minimumYPosition)) + _minimumYPosition;
        }
    }
}