using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuoEnemySpawner : EnemyManager
{
	public DuoEnemySpawner(Vector3 spawnPos, float moveSpeed) : base(spawnPos, moveSpeed)
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
			EnemyController controller = enemy.GetComponent<EnemyController>();
			if (index <= 1)
			{
				enemy.transform.Rotate(0, 90, 0);
				controller.SetDefault(this, _moveSpeed * 4);
			}
			else
			{
				enemy.transform.Rotate(0, -90, 0);
			}
			controller.ActivateEnemy();
		}
	}
}
