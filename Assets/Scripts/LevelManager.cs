using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace StreetRacer
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance;

		private Vector3 _nextRoadPosition = (Vector3.forward * 10) * 15;

		[SerializeField]
		private GameObject[] _vehaclePrefabs;
		[SerializeField]
		private TMP_Text _gameOverScoreText;
		[SerializeField]
		private GameObject _winterPrefab, _summerPrefab;
		[SerializeField]
		private float _spawnDelay = 0;

		private MapManager _mapManager;
		private EnemyManager _enemyManager;
		private PlayerController _playerController;

		private MapType _mapType = MapType.SUMMER;
		private RideType _rideType = RideType.MONO;


		private int _playerScore = 0;
		private float _gameTimer;
		private GameObject player;

		public PlayerController PlayerController { get { return _playerController; } }
		public GameObject[] VehiclePrefabs { get { return _vehaclePrefabs; } }
		

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

		private void Start()
		{
			player = new GameObject("Player");
		}
		private void SpawnPlayer()
		{
			player.transform.position = new Vector3(0, 0, 13);
			player.transform.rotation = Quaternion.identity;
			player.AddComponent<PlayerController>();
			_playerController = player.GetComponent<PlayerController>();
		}

		private void Update()
		{
			if (GameManager.Singleton.GameStatus == GameStatus.PLAYING)
			{
				_gameTimer += Time.deltaTime;
				_mapManager.MoveRoad(this.gameObject);
				_spawnDelay += Time.deltaTime;
				if (_gameTimer > 0.1f)
				{
					_playerScore += 2;
					GameUI.Instance.SetDistanceText(_playerScore.ToString());
					_gameTimer = 0;
				}
				if(_spawnDelay > 3)
				{
					_enemyManager.ActivateEnemy();
					_spawnDelay = 0;
				}
			}
		}

		public void SetRideType(RideType type)
		{
			_rideType = type;
		}
		public void SetMapType(MapType type)
		{
			_mapType = type;
		}
		private void SetupMapManager()
		{
			if(_mapType == MapType.SUMMER)
			{
				_mapManager = new SummerMap(_summerPrefab, PlayerController.Speed);
			}
			else if(_mapType == MapType.WINTER)
			{
				_mapManager = new WinterMap(_winterPrefab, PlayerController.Speed);
			}
		}
		private void SetupEnemyManager()
		{
			if (_rideType == RideType.MONO)
			{
				_enemyManager = new MonoEnemySpawner(_nextRoadPosition, PlayerController.Speed - 8);
			}
			else if (_rideType == RideType.DUO)
			{
				_enemyManager = new DuoEnemySpawner(_nextRoadPosition, PlayerController.Speed - 8);
			}
		}
		private void ClearLevelSettings()
		{
			_mapManager?.Clear();
			_enemyManager?.Clear();
			PlayerController?.Clear();
		}
		public void GameOver()
		{
			GameManager.Singleton.GameStatus = GameStatus.FAILED;
			GameUI.Instance.GameOver("Количество очков: " + _playerScore.ToString());
			GameManager.Singleton.Player.AddPlayerCash(_playerScore / 2);
		}

		public void GameStarted()
		{
			ClearLevelSettings();

			SpawnPlayer();

			SetupMapManager();
			SetupEnemyManager();

			_enemyManager.SpawnEnemies(_vehaclePrefabs);
			_mapManager.DrawMap();

			GameManager.Singleton.GameStatus = GameStatus.PLAYING;
			_playerScore = 0;
		}
	}
}
