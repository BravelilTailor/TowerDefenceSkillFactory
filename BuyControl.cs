using UnityEngine;
using System.Collections.Generic;

namespace TowerDefence
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_TowerBuyPrefab;
        
        private List<TowerBuyControl> m_ActivateControl;
        private RectTransform m_RectTransform;

        //  Unity Events
        private void Awake()
        {
            m_RectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += BuildSiteTransform;
            gameObject.SetActive(false);
            
        }
        
        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= BuildSiteTransform;
        }
        //  Events end

        private void BuildSiteTransform(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                m_RectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
                m_ActivateControl = new List<TowerBuyControl>();
                foreach (var asset in buildSite.buildableTowers)
                {
                    if (asset.IsAvailable())
                    {
                        var NewControl = Instantiate(m_TowerBuyPrefab, transform);
                        m_ActivateControl.Add(NewControl);
                        NewControl.SetTowerAsset(asset);
                    }
                }
                if (m_ActivateControl.Count > 0)
                {
                    var angle = 360 / m_ActivateControl.Count;
                    for (int i = 0; i < m_ActivateControl.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.up * 140);
                        m_ActivateControl[i].transform.position += offset;
                    }
                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }
                }                   
            } 
            else
            {
                if (m_ActivateControl != null)
                {
                    foreach (var control in m_ActivateControl) Destroy(control.gameObject);
                    m_ActivateControl.Clear();
                }
                gameObject.SetActive(false);
            }  
        }
    }
}