using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
	public class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance;
		[SerializeField]
		private GameObject _roadPrefab;
		[SerializeField]
		private GameObject[] _vehaclePrefabs;
		[SerializeField] 
		private float _moveSpeed = 10;

		private List<GameObject> _roadList;
		private Vector3 _nextRoadPosition = Vector3.zero;
		private GameObject _roadHolder;
		private PlayerController _playerController;
		private int _roadAtLastIndex, _roadAtTopIndex;
		private EnemyManager _enemyManager;

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
			_roadHolder = new GameObject("RoadHolder");
			_roadList = new List<GameObject>();

			for(int i = 0; i < 15; i++)
			{
				GameObject road = Instantiate(_roadPrefab, _nextRoadPosition, Quaternion.identity);
				road.name = "Road " + i.ToString();
				road.transform.SetParent(_roadHolder.transform);
				_nextRoadPosition += Vector3.forward * 10;
				_roadList.Add(road);
			}

			_enemyManager = new EnemyManager(new Vector3(_nextRoadPosition.x, _nextRoadPosition.y - 2.1f, _nextRoadPosition.z), _moveSpeed);

			SpawnPlayer();

			_enemyManager.SpawnEnemies(_vehaclePrefabs);

		}
		private void SpawnPlayer()
		{
			GameObject player = new GameObject("Player");
			player.transform.position = new Vector3(0, -2.1f, 13);
			player.AddComponent<PlayerController>();
			_playerController = player.GetComponent<PlayerController>();
		}

		private void Update()
		{
			if (GameManager.Singleton.GameStatus == GameStatus.PLAYING)
			{
				MoveRoad();
			}
		}

		private void MoveRoad()
		{
			for(int i =0; i < _roadList.Count; i++)
			{
				_roadList[i].transform.Translate(-transform.forward * _moveSpeed * Time.deltaTime);
			}

			if (_roadList[_roadAtLastIndex].transform.position.z <= -10) 
			{
				_roadAtTopIndex = _roadAtLastIndex - 1;

				if(_roadAtTopIndex < 0)
				{
					_roadAtTopIndex = _roadList.Count - 1;
				}

				_roadList[_roadAtLastIndex].transform.position = _roadList[_roadAtTopIndex].transform.position + Vector3.forward * 15;

				_roadAtLastIndex++;
				if(_roadAtLastIndex >= _roadList.Count)
				{
					_roadAtLastIndex = 0;
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
		
		public void GameOver()
		{
			GameManager.Singleton.GameStatus = GameStatus.FAILED;
			UIManager.Instance.GameOver();
		}

		public void GameStarted()
		{
			GameManager.Singleton.GameStatus = GameStatus.PLAYING;
			_enemyManager.ActivateEnemy();
			_playerController.GameStarted();
		}
	}
}
