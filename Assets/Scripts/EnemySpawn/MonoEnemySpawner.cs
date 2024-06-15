using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoEnemySpawner : EnemyManager
{
	public MonoEnemySpawner(Vector3 spawnPos, float moveSpeed) : base(spawnPos, moveSpeed)
	{
		
	}
	public override void ActivateEnemy()
	{
		if (_deactiveEnemyList.Count > 0)
		{
			GameObject enemy = _deactiveEnemyList[Random.Range(0, _deactiveEnemyList.Count)];
			_deactiveEnemyList.Remove(enemy);
			int index = Random.Range(0, _enemySpawnPosition.Length);
			enemy.transform.position = _enemySpawnPosition[index];
			enemy.transform.Rotate(0, -90, 0);
			
			enemy.GetComponent<EnemyController>().ActivateEnemy();
		}
	}
}
