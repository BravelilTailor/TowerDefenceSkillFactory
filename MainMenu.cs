using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefence
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button continueButton;
        [SerializeField] private GameObject SaveDeleteObj;

        private void Start()
        {
            continueButton.interactable = FileHandler.HasFile(MapComplition.filename);
        }

        public void NewGame()
        {
            if (FileHandler.HasFile(MapComplition.filename))
            {
                SaveDeleteObj.gameObject.SetActive(true);
            }
            else
            
            SceneManager.LoadScene(1);
        }

        public void SaveDeletePanelYes()
        {
            FileHandler.Reset(MapComplition.filename);
            FileHandler.Reset(Upgrades.filename);
            SceneManager.LoadScene(1);
        }

        public void SaveDeletePanelNo()
        {
            SaveDeleteObj.gameObject.SetActive(false);
        }

        public void Continue()
        {
            SceneManager.LoadScene(1);
        }

        public void Quit()
        {
            Application.Quit();
        }
    }

}
