using UnityEngine;
using SpaceShooter;

namespace TowerDefence
{
    public class LevelWaveCondition : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;
        private void Start()
        {
            FindObjectOfType<EnemyWavesManager>().OnAllWavesDone += () =>
            {
                isCompleted = true;
            };
        }
        public bool IsComplete { get { return isCompleted; } }
    }
}

