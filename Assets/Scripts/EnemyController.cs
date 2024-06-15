using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
	public class EnemyController : MonoBehaviour
	{
		private float _moveSpeed;
		private EnemyManager _enemyManager;

		public void SetDefault(EnemyManager enemyManager, float moveSpeed)
		{
			_enemyManager = enemyManager;
			_moveSpeed = moveSpeed;
		}
		public void ActivateEnemy()
		{
			gameObject.SetActive(true);
		}

		private void Update()
		{
			if (GameManager.Singleton.GameStatus == GameStatus.PLAYING)
			{
				transform.Translate(transform.forward * _moveSpeed * Random.Range(0.4f, 0.8f) * Time.deltaTime);

				if (transform.position.z <= -10)
				{
					_enemyManager.DeactivateEnemy(gameObject);
				}
			}
		}
	}
}
