using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence
{
    public class TextUpdate : MonoBehaviour
    {
        public enum UpdateSource { Gold, Life, Mana }
        public UpdateSource source = UpdateSource.Gold;
        private Text m_text;

        void Start()
        {
            m_text = GetComponent<Text>();
            switch (source)
            {
                case UpdateSource.Gold:
                    TDPlayer.Instance.GoldUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Life:
                    TDPlayer.Instance.LifeUpdateSubscribe(UpdateText);
                    break;
                case UpdateSource.Mana:
                    TDPlayer.Instance.ManaUpdateSubscribe(UpdateText);
                    break;
            }
            
        }

        private void UpdateText(int money)
        {
            m_text.text = money.ToString();
        }
    }
}

