using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
	[SerializeField]
	private CarData _carData;

	public CarData CarData { get { return _carData; } }
}
