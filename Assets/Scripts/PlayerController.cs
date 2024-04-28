using System;
using UnityEngine;

namespace StreetRacer
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private float _endXpos = 0f;
        private Rigidbody _carObject;
		private Collider _colliderComponent;

		[SerializeField]
		private float Control = 0.03f;

		private void Start()
		{
			_carObject = GetComponent<Rigidbody>();
			_carObject.isKinematic = true;
			_carObject.useGravity = false;
			SpawnVehicle(GameManager.Singleton.CurrentCarIndex);
		}
		private void Update()
		{
			if (InputManager.Instance.MoveRight)
			{
				MoveCar(Control);
			}
			if(InputManager.Instance.MoveLeft)
			{
				MoveCar(-Control);
			}
		}
		public void GameStarted()
		{

		}

		public void SpawnVehicle(int index)
		{
			if (transform.childCount > 0)
			{
				Destroy(transform.GetChild(0).gameObject);
			}

			GameObject child = Instantiate(LevelManager.Instance.VehiclePrefabs[index], transform);
			_colliderComponent = child.GetComponent<Collider>();
			_colliderComponent.isTrigger = true;
		}

		private void MoveCar(float direction)
		{
			_endXpos = transform.position.x + direction;
			_endXpos = Mathf.Clamp(_endXpos, -12, 12);

			var nextPosition = transform.position;
			nextPosition.x = _endXpos;
			transform.position = nextPosition;
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.GetComponent<EnemyController>())
			{
				if(GameManager.Singleton.GameStatus == GameStatus.PLAYING)
				{
					Destroy(this);
					LevelManager.Instance.GameOver();
					//_carObject.isKinematic = false;
					_carObject.useGravity = true;
					_carObject.AddForce(UnityEngine.Random.insideUnitCircle.normalized * 100f);
					_colliderComponent.isTrigger = false;
				}

			}
		}
	}
}
