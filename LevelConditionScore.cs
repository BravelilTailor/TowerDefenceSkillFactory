using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int score;
        [SerializeField] private int LevelCompleteTime;

        private bool m_Reached;

        bool ILevelCondition.IsComplete
        { 
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.Score >= score)
                    {
                        
                        m_Reached = true;
                        
                    }
                }
                return m_Reached;
            }

        }


    }
}

