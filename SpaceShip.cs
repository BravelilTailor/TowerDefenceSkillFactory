using System;
using TowerDefence;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Масса для установки
        /// </summary>
        [Header("Space Ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// Сила движения вперед
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Сила вращения
        /// </summary>
        [SerializeField] private float m_Mobility;

       

        /// <summary>
        /// Максимальная скорость движения
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        private float m_MaxVelocityBackup;
        public void HalfedMaxLinearVelocity()
        {
            m_MaxVelocityBackup = m_MaxLinearVelocity;
            m_MaxLinearVelocity /= 2;
        }
        public void RestoreMaxLinearVelocity()
        {
            m_MaxLinearVelocity = m_MaxVelocityBackup;
        }

        /// <summary>
        /// Torque speed
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        

        /// <summary>
        /// Сохраненая ссылка на физ модель
        /// </summary>
        private Rigidbody2D m_Rigid;

        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        #region Public API
        /// <summary>
        /// Управление линейной тягой. От -1.0 до +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Управление вращением. От -1.0 до +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        #endregion

        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;

           // InitOffensive();
        }


        private void FixedUpdate()

        {
            UpdateRigidBody();

           // UpdateEnergyRegen();
        }

        #endregion
        /// <summary>
        /// Метод добавления сил кораблю
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }

        public bool DrawEnergy(int count)
        {
             return true;
        }

        // Временный методб исп турелями
        public bool DrawAmmo(int count)
        {
            return true;
        }

        public void Fire(TurretMode mode)
        {
            return;
        }

        /*
        #region Offensive

                [SerializeField] private Turret[] m_Turrets;
                public void Fire(TurretMode mode)
                {
                    for (int i = 0; i < m_Turrets.Length; i++)
                    {
                        if (m_Turrets[i].Mode == mode)
                        {
                            m_Turrets[i].Fire();
                        }
                    }
                }

                [SerializeField] private int m_MaxEnergy;
                [SerializeField] private int m_MaxAmmo;
                [SerializeField] private int m_EnergyRegenPerSecond;

                private float m_PrimaryEnergy;
                private int m_SecondaryAmmo;

                public void AddEnergy(int e)
                {
                    m_PrimaryEnergy += Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);

                }

                public void AddAmmo(int ammo)
                {
                    m_SecondaryAmmo += Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
                }

                private void InitOffensive()
                {
                    m_PrimaryEnergy = m_MaxEnergy;
                    m_SecondaryAmmo = m_MaxAmmo;
                }

                private void UpdateEnergyRegen()
                {
                    m_PrimaryEnergy += (float) m_EnergyRegenPerSecond * Time.fixedDeltaTime;
                    m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);

                }

                public bool DrawEnergy(int count)
                {
                    if (count == 0)
                        return true;

                    if (m_PrimaryEnergy >= count)
                    {
                        m_PrimaryEnergy -= count;
                        return false;
                    }

                    return false;
                }

                public bool DrawAmmo(int count)
                {
                    if (count == 0)
                        return true;

                    if (m_SecondaryAmmo >= count)
                    {
                        m_SecondaryAmmo -= count;
                        return false;
                    }

                    return false;
                }
         #endregion

                public void AssignWeapon(TurretProperties props)
                {
                    for (int i = 0; i < m_Turrets.Length; i++)
                    {
                        m_Turrets[i].AssignLoadout(props);
                    }
                }
         */
        public new void Use(EnemyAssets asset)
        {
            m_MaxLinearVelocity = asset.moveSpeed;
            base.Use(asset);
        }
    }

}