using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
	public class EnemyManager
	{
		private List<EnemyController> _deactiveEnemyList;
		private Vector3[] _enemySpawnPosition = new Vector3[4];

		private GameObject _enemyHolder;
		private float _moveSpeed;

		public EnemyManager(Vector3 spawnPos, float moveSpeed)
		{
			_moveSpeed = moveSpeed;
			_deactiveEnemyList = new List<EnemyController>();

			_enemySpawnPosition[0] = spawnPos - Vector3.right * 10;
			_enemySpawnPosition[1] = spawnPos - Vector3.right * 4;
			_enemySpawnPosition[2] = spawnPos + Vector3.right * 4;
			_enemySpawnPosition[3] = spawnPos + Vector3.right * 10;

			_enemyHolder = new GameObject("EnemyHolder");
		}

		public void SpawnEnemies(GameObject[] vehiclePrefabs)
		{
			for(int i = 0; i < vehiclePrefabs.Length; i++)
			{
				GameObject enemy = Object.Instantiate(vehiclePrefabs[i], _enemySpawnPosition[1], Quaternion.identity);
				enemy.transform.localScale = new Vector3(240.0f, 240.0f, 240.0f);
				enemy.transform.Rotate(0, -90, 0);
				enemy.SetActive(false);
				enemy.transform.SetParent(_enemyHolder.transform);
				enemy.name = "Enemy";
				enemy.AddComponent<EnemyController>();
				enemy.GetComponent<EnemyController>().SetDefault(this, _moveSpeed);
				_deactiveEnemyList.Add(enemy.GetComponent<EnemyController>());
			}
		}

		public void ActivateEnemy()
		{
			if(_deactiveEnemyList.Count > 0)
			{
				EnemyController enemy = _deactiveEnemyList[Random.Range(0, _deactiveEnemyList.Count)].gameObject.GetComponent<EnemyController>();
				_deactiveEnemyList.Remove(enemy);
				enemy.transform.position = _enemySpawnPosition[Random.Range(0, _enemySpawnPosition.Length)];
				enemy.ActivateEnemy();
			}
		}

		public void DeactivateEnemy(EnemyController enemy)
		{
			enemy.gameObject.SetActive(false);
			_deactiveEnemyList.Add(enemy);
		}
	}
}
