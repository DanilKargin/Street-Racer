using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

namespace StreetRacer
{
    public class UINavigator : MonoBehaviour
    {
        public static UINavigator Instance;
        [SerializeField]
        private GameObject MainMenuPanel;

        private GameObject _currentPanel;
        private Stack<GameObject> _openPanels;

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
		public void Start()
		{
            _currentPanel = MainMenuPanel;
            _openPanels = new Stack<GameObject>();
		}
        public void Back()
        {
            if(_openPanels.Count >= 0) {
                var panel = _openPanels.Pop();
                panel.SetActive(true);
                _currentPanel.SetActive(false);
                _currentPanel = panel;
			}
        }
        public void OpenPanel(GameObject panel)
        {
            panel.SetActive(true);
            _openPanels.Push(_currentPanel);
            _currentPanel.SetActive(false);
            _currentPanel = panel;
        }
        //public void PlayButton()
        //{

        //    LevelManager.Instance.GameStarted();
        //}
        //public void RetryButton()
        //{
        //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //}
        //public void GameOver()
        //{
        //    GameOverPanel.SetActive(true);
        //}
    }
}
