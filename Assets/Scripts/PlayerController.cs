using UnityEngine;

namespace StreetRacer
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        private float _endXpos = 0f;
        private Rigidbody _carObject;
		private Collider _colliderComponent;
		private GameObject child;

		private float _control;
		private float _speed;

		public float Speed { get { return _speed; } }

		private void Awake()
		{		
			_carObject = GetComponent<Rigidbody>();
			_carObject.isKinematic = true;
			_carObject.useGravity = false;
			int carIndex = GameManager.Singleton.Player.CurrentCarIndex;
			var carData = GameManager.Singleton.Cars[carIndex].GetComponent<Car>().CarData;
			SpawnVehicle(carIndex);
			_speed = carData.Speed;
			_control = carData.Control;
		}
		private void Update()
		{
			if (InputManager.Instance.MoveRight)
			{
				MoveCar(_control);
			}
			if(InputManager.Instance.MoveLeft)
			{
				MoveCar(-_control);
			}
		}

		public void SpawnVehicle(int index)
		{
			if (transform.childCount > 0)
			{
				Destroy(transform.GetChild(0).gameObject);
			}

			child = Instantiate(GameManager.Singleton.Cars[index], transform);
			child.transform.localScale = new Vector3(240.0f, 240.0f, 240.0f);
			child.transform.Rotate(0, -90, 0);
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
					_carObject.isKinematic = false;
					_carObject.useGravity = true;
					_carObject.AddForce(UnityEngine.Random.insideUnitCircle.normalized * 100f);
					_colliderComponent.isTrigger = false;
				}

			}
		}
		public void Clear()
		{
			Destroy(child);
		}
	}
}
