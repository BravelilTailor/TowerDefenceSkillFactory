using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{

    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {

        [SerializeField] private MapLevel rootlevel;
        [SerializeField] private Text PointText;
        [SerializeField] private int PointsNeeded = 3;

        
        internal void TryActivate()
        {
            gameObject.SetActive(rootlevel.IsComplete);

            if (PointsNeeded > MapComplition.Instance.TotalScore)
            {
                PointText.text = PointsNeeded.ToString();
                
            } else
            {
                PointText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialise();
            }
            
        }
    }
}

