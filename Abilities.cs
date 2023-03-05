using UnityEngine;
using System;
using SpaceShooter;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefence
{
    public class Abilities : MonoSingleton<Abilities>
    {
        [Serializable]
        public class FireAbility
        {
            [SerializeField] public int m_FireCost = 5;
            [SerializeField] private float m_Cooldown = 5f;
            [SerializeField] public int m_Damage = 5;
            [SerializeField] private Color m_TargetingColor;
            
            public void Use() 
            {
                
                TDPlayer.Instance.UpdateMana(-m_FireCost);

                
                    ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, 5))
                    {
                        if (collider.transform.parent.TryGetComponent<Enemy>(out var enemy))
                        {
                            enemy.TakeDamage(m_Damage, TDProjectile.DamageType.Magic);
                        }
                    }
;
                });
                IEnumerator FireAbilityButton()
                {
                    Instance.m_FireButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_FireButton.interactable = true;
                }
                Instance.StartCoroutine(FireAbilityButton());
            }
        }
        [Serializable]
        public class SlowAbility
        {
            [SerializeField] public int m_SlowCost = 10;
            [SerializeField] private float m_Cooldown = 15f;
            [SerializeField] private float m_Duration = 5;
            public void Use() 
            {
                TDPlayer.Instance.UpdateMana(-m_SlowCost);

                void Slow(Enemy ship)
                {
                    ship.GetComponent<SpaceShip>().HalfedMaxLinearVelocity();
                }
                
                foreach (var ship in FindObjectsOfType<SpaceShip>())
                    ship.HalfedMaxLinearVelocity();
                EnemyWavesManager.OnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_Duration);
                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                        ship.RestoreMaxLinearVelocity();
                    EnemyWavesManager.OnEnemySpawn -= Slow;
                }
                Instance.StartCoroutine(Restore());

                IEnumerator SlowAbilityButton()
                {
                    Instance.m_SlowButton.interactable = false;
                    yield return new WaitForSeconds(m_Cooldown);
                    Instance.m_SlowButton.interactable = true;
                }
                Instance.StartCoroutine(SlowAbilityButton());
            }
        }
        [SerializeField] private Image m_TargetingCircle;
        [SerializeField] private Button m_SlowButton;
        [SerializeField] private Button m_FireButton;
        [SerializeField] private FireAbility m_fireAbility;
        public void UseFireAbility() => m_fireAbility.Use();

        [SerializeField] private SlowAbility m_slowAbility;
        public void UseSlowAbility() => m_slowAbility.Use();



        [SerializeField] private UpgradeAsset m_FireSkillUpgrade;
        [SerializeField] private UpgradeAsset m_SlowSkillUpgrade;
        private new void Awake()
        {
            base.Awake();

            Instance.m_SlowButton.interactable = false;

            var FireUpgradelevel = Upgrades.GetUpgradeLevel(m_FireSkillUpgrade);
            var SlowUpgradelevel = Upgrades.GetUpgradeLevel(m_SlowSkillUpgrade);

            if (SlowUpgradelevel > 0)
            {
                Instance.m_SlowButton.interactable = true;
            } else
            {
                Instance.m_SlowButton.interactable = false;
            }

            if (FireUpgradelevel > 0)
            {
                Instance.m_FireButton.interactable = true;
            }
            else
            {
                Instance.m_FireButton.interactable = false;
            }

        }
    }
}