using UnityEngine;
using SpaceShooter;


namespace TowerDefence
{
    public class TimeLevelCondition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float timeLimit = 4f;
        private void Start()
        {
            timeLimit += Time.time;
        }
        public bool IsComplete => Time.time > timeLimit;
    }
}

