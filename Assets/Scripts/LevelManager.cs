using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

namespace StreetRacer
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance;

		public Vector3 _nextRoadPosition = Vector3.zero;

		[SerializeField]
		private GameObject[] _vehaclePrefabs;
		[SerializeField]
		private GameObject[] _maps;
		[SerializeField]
		private TMP_Text _scoreText;
		[SerializeField]
		private TMP_Text _gameOverScoreText;
		

		private MapManager _mapManager;
		private PlayerController _playerController;
		private MapType _mapType;
		private RideType _rideType;

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
			_enemyManager = new EnemyManager(new Vector3(_nextRoadPosition.x, _nextRoadPosition.y, _nextRoadPosition.z), 5);
			GameObject map = new GameObject("MapHolder");
			
			SpawnPlayer();

			_enemyManager.SpawnEnemies(_vehaclePrefabs);

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
				if (_gameTimer > 0.1f)
				{
					_playerScore += 2;
					_scoreText.text = _playerScore.ToString();
					_gameTimer = 0;
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
		public void GameOver()
		{
			_gameOverScoreText.text = "Количество очков: " + _playerScore.ToString();
			GameManager.Singleton.GameStatus = GameStatus.FAILED;
			//UINavigator.Instance.GameOver();
		}

		public void GameStarted()
		{
			GameManager.Singleton.GameStatus = GameStatus.PLAYING;
			_playerScore = 0;
			_enemyManager.ActivateEnemy();
			_playerController.GameStarted();
		}
	}
}
