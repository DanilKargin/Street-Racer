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
		private float _gameTimer2;

		private MapManager _mapManager;
		private PlayerController _playerController;
		private MapType _mapType = MapType.SUMMER;
		private RideType _rideType = RideType.MONO;

		private EnemyManager _enemyManager;
		private int _playerScore = 0;
		private float _gameTimer;

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
			_mapManager = new SummerMap(_summerPrefab);

			SpawnPlayer();
		}
		private void SpawnPlayer()
		{
			GameObject player = new GameObject("Player");
			player.transform.position = new Vector3(0, 0, 13);
			player.AddComponent<PlayerController>();
			_playerController = player.GetComponent<PlayerController>();
		}

		private void Update()
		{
			if (GameManager.Singleton.GameStatus == GameStatus.PLAYING)
			{
				_gameTimer += Time.deltaTime;
				_mapManager.MoveRoad(this.gameObject);
				_gameTimer2 += Time.deltaTime;
				if (_gameTimer > 0.1f)
				{
					_playerScore += 2;
					GameUI.Instance.SetDistanceText(_playerScore.ToString());
					_gameTimer = 0;
				}
				if(_gameTimer2 > 5)
				{
					_enemyManager.ActivateEnemy();
					_gameTimer2 = 0;
				}
			}
		}


		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<EnemyController>())
			{
				_enemyManager.ActivateEnemy();
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
				_mapManager = new SummerMap(_summerPrefab);
			}
			else if(_mapType == MapType.WINTER)
			{
				_mapManager = new WinterMap(_winterPrefab);
			}
		}
		private void SetupEnemyManager()
		{
			if (_rideType == RideType.MONO)
			{
				_enemyManager = new MonoEnemySpawner(_nextRoadPosition, 30);
			}
			else if (_rideType == RideType.DUO)
			{
				_enemyManager = new DuoEnemySpawner(_nextRoadPosition, 30);
			}
		}
		public void GameOver()
		{
			_gameOverScoreText.text = "Количество очков: " + _playerScore.ToString();
			GameManager.Singleton.GameStatus = GameStatus.FAILED;
			//UINavigator.Instance.GameOver();
		}

		public void GameStarted()
		{
			SetupMapManager();
			SetupEnemyManager();
			_enemyManager.SpawnEnemies(_vehaclePrefabs);
			_mapManager.DrawMap();

			GameManager.Singleton.GameStatus = GameStatus.PLAYING;
			_playerScore = 0;
			_playerController.GameStarted();
		}
	}
}
