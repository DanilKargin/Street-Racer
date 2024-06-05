using System.Collections.Generic;
using UnityEngine;


namespace StreetRacer
{
	public abstract class MapManager
	{
		[SerializeField]
		protected GameObject _roadHolder;
		protected List<GameObject> _roadList;
		private int _roadAtLastIndex, _roadAtTopIndex;

		[SerializeField]
		private float _moveSpeed = 30;

		public abstract void DrawMap();
		public void MoveRoad(GameObject gm)
		{
			for (int i = 0; i < _roadList.Count; i++)
			{
				_roadList[i].transform.Translate(-gm.transform.forward * _moveSpeed * Time.deltaTime);
			}

			if (_roadList[_roadAtLastIndex].transform.position.z <= -10)
			{
				_roadAtTopIndex = _roadAtLastIndex - 1;

				if (_roadAtTopIndex < 0)
				{
					_roadAtTopIndex = _roadList.Count - 1;
				}

				_roadList[_roadAtLastIndex].transform.position = _roadList[_roadAtTopIndex].transform.position + Vector3.forward * 15;

				_roadAtLastIndex++;
				if (_roadAtLastIndex >= _roadList.Count)
				{
					_roadAtLastIndex = 0;
				}
			}
		}
		~MapManager()
		{
			Object.Destroy(_roadHolder);
		}
	}
}
