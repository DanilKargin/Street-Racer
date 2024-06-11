using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StreetRacer
{
	public class GameUI : MonoBehaviour
	{
		public static GameUI Instance;
		[SerializeField]
		private GameObject _selectRideTypePanel;
		[SerializeField]
		private GameObject _gamePanel;
		[SerializeField]
		private TMP_Text _distanceText;
		[SerializeField]
		private GameObject _gameOverPanel;
		[SerializeField]
		private TMP_Text _gameOverText;

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
		public void BackToMenu()
		{
			_gameOverPanel.SetActive(false);
			UINavigator.Instance.OpenPanel(UINavigator.Instance.MainMenuPanel);
		}
		public void TryAgain()
		{
			_gameOverPanel.SetActive(false);
			LevelManager.Instance.GameStarted();
		}
		public void SelectWinterMap()
		{
			UINavigator.Instance.OpenPanel(_selectRideTypePanel);
			LevelManager.Instance.SetMapType(MapType.WINTER);
		}
		public void SelectSummerMap()
		{
			UINavigator.Instance.OpenPanel(_selectRideTypePanel);
			LevelManager.Instance.SetMapType(MapType.SUMMER);
		}
		public void SelectOneRide()
		{
			UINavigator.Instance.OpenPanel(_gamePanel);
			LevelManager.Instance.SetRideType(RideType.MONO);
			LevelManager.Instance.GameStarted();
		}
		public void SelectDuoRide()
		{
			UINavigator.Instance.OpenPanel(_gamePanel);
			LevelManager.Instance.SetRideType(RideType.DUO);
			LevelManager.Instance.GameStarted();
		}
		public void SetDistanceText(string text)
		{
			_distanceText.text = text;
		}
		public void GameOver(string playerScore)
		{
			_gameOverPanel.SetActive(true);
			_gameOverText.text = playerScore;
		}
	}
}
