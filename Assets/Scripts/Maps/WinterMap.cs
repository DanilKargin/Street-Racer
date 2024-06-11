using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinterMap : MapManager
{
	private GameObject _roadPrefab;

	private Vector3 _nextRoadPosition = new Vector3(0, -2.2f, 0);

	public WinterMap(GameObject mapPrefab, float speed)
	{
		_roadPrefab = mapPrefab;
		_roadHolder = new GameObject("RoadHolder");
		base._moveSpeed = speed;
	}
	public override void DrawMap()
	{
		base._roadList = new List<GameObject>();
		for (int i = 0; i < 15; i++)
		{
			GameObject road = Object.Instantiate(_roadPrefab, _nextRoadPosition, Quaternion.identity);
			road.name = "Road " + i.ToString();
			road.transform.SetParent(_roadHolder.transform);
			_nextRoadPosition += Vector3.forward * 10;
			base._roadList.Add(road);
		}
	}
}
