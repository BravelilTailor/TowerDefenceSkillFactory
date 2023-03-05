using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        public void SetFromOtherProjectile(Projectile other)
        {
            other.GetData(out m_Velocity, out m_Lifetime, out m_Damage, out m_ImpactEffectPrefab);
        }

        private void GetData(out float m_Velocity, out float m_Lifetime, out int m_Damage, out ImpactEffect m_ImpactEffectPrefab)
        {
            m_Velocity = this.m_Velocity; 
            m_Lifetime = this.m_Lifetime; 
            m_Damage = this.m_Damage; 
            m_ImpactEffectPrefab = this.m_ImpactEffectPrefab;
        }

        [SerializeField] private float m_Velocity;

        [SerializeField] private float m_Lifetime;

        [SerializeField] protected int m_Damage;

        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        private float m_Timer;

        private void Update()
        {
            float stepLength = Time.deltaTime * m_Velocity;
            Vector2 step = transform.up * stepLength;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            //Выключить в свойствах проекта, вкладка Physics 2D

            if (hit)
            {
                OnHit(hit);
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            m_Timer += Time.deltaTime;

            if (m_Timer > m_Lifetime) 
                Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

       

        protected virtual void OnHit(RaycastHit2D hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_Parent)
            {
                dest.ApplyDamage(m_Damage);

                ///if (m_Parent == Player.Instance.ActiveShip)
                /// {
                ///  Player.Instance.AddScore(dest.ScoreValue);
                ///}
            }
        }   

        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }

        private Destructible m_Parent;

        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }

        public void SetTarget(Destructible tarhet)
        {

        }


    }

}

#if UNITY_EDITOR
namespace TowerDefence
{
    [CustomEditor(typeof(SpaceShooter.Projectile))]
    public class ProjectileInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Create TD Projectile"))
            {
                var target = this.target as SpaceShooter.Projectile;
                var TDProj = target.gameObject.AddComponent<TDProjectile>();
                TDProj.SetFromOtherProjectile(target);
            }
        }
    }
}
#endif