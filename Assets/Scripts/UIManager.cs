using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace StreetRacer
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField] 
        private GameObject MainMenuPanel, GameMenuPanel, GameOverPanel;
        [SerializeField]
        private TMP_Text _distanceText;
        public TMP_Text DistanceText { get { return _distanceText; } }

		private void Awake()
		{
	        if (Instance == null)
            {
                Instance = this;
            }
			else
			{
				Destroy(gameObject);
			}
		}
        public void PlayButton()
        {
            MainMenuPanel.SetActive(false);
            GameMenuPanel.SetActive(true);
            LevelManager.Instance.GameStarted();
        }
        public void RetryButton()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void GameOver() 
        { 
            GameOverPanel.SetActive(true);
        }
	}
}
