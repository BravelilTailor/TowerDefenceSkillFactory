using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TowerDefence;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый обьект на сцене. То что может иметь хитпоинты
    /// </summary>
    public class Destructible : Entity
    {

        #region Properties
        /// <summary>
        /// Обьект игнорирует урон
        /// </summary>
        [SerializeField] private bool m_Indestructible;
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Стартовое ХП
        /// </summary>
        [SerializeField] private int m_HitPoints;
        public float HitPoints => m_HitPoints;

        /// <summary>
        /// Текущие ХП
        /// </summary>
        [SerializeField] private int m_CurrentHitPoints;
        public int Hitpoints => m_CurrentHitPoints;
        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region Public API

        /// <summary>
        /// Применение урона к обьекту
        /// </summary>
        /// <param name="damage">Урон</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0)
                OnDeath();


        }

        #endregion
        /// <summary>
        /// Переопределение события уничтожения, если ХП ниже нуля
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);

            m_EventonDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestructibles;

        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;

        [SerializeField] private UnityEvent m_EventonDeath;
        public UnityEvent EventonDeath => m_EventonDeath;

        #region Score

        [SerializeField] private int m_ScoreValue;
        public int ScoreValue => m_ScoreValue;

        #endregion

        protected void Use(EnemyAssets asset)
        {
            m_HitPoints = asset.hp;
            m_ScoreValue = asset.score;
        }
    }
}