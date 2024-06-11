using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
	[CreateAssetMenu(fileName = "New car", menuName = "Car Data", order = 51)]
	public class CarData : ScriptableObject
	{
		[SerializeField]
		private int _id;
		[SerializeField]
		private string _name;
		[SerializeField]
		private int _speed;
		[SerializeField]
		private float _control;
		[SerializeField]
		private int _price;

		public int Id { get { return _id; } }
		public string Name { get { return _name; } }
		public int Speed { get { return _speed; } }
		public float Control { get { return _control; } }
		public int Price { get { return _price; } }
	}
}
