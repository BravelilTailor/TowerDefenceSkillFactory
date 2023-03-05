using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : MonoSingleton<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "LevelMap";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            //сбрасываем статы перед началом епизода

           // LevelStatistics = new PlayerStatistics();
           // LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
            
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            SceneManager.LoadScene(0);
        }
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            //CalculateLevelStatistics();

            LevelResultController.Instance.Show(success);
        }

        public void AdvanceLevel()
        {
            SceneManager.LoadScene(MainMenuSceneNickname);

            /*CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }*/
        }

        private void CalculateLevelStatistics()
        {
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.numKills = Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
        }
    }
}

