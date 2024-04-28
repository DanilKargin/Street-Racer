using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
    public class InputManager : MonoBehaviour
    {
		public static InputManager Instance;
		private bool _moveRight = false;
		private bool _moveLeft = false;

		public bool MoveRight { get { return _moveRight; } }
		public bool MoveLeft { get { return _moveLeft; } }

		public void RightButtonDown()
		{
			_moveRight = true;
		}
		public void LeftButtonDown()
		{
			_moveLeft = true;
		}

		public void RightButtonUp()
		{
			_moveRight = false;
		}
		public void LeftButtonUp()
		{
			_moveLeft = false;
		}

		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
