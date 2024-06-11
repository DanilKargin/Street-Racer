using System.Collections.Generic;
using UnityEngine;

namespace StreetRacer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Singleton;

		[HideInInspector]
		public GameStatus GameStatus = GameStatus.NONE;
		[HideInInspector]
		public Player Player = null;

		public List<GameObject> Cars;
		private void Awake()
		{
			if(Singleton == null)
			{
				Singleton = this;
				DontDestroyOnLoad(gameObject);
			}
			else
			{
				Destroy(gameObject);
			}
		}
		private void Start()
		{
			GameManager.Singleton.GameStatus = GameStatus.NONE;
			GameManager.Singleton.Player = UserController.Instance.LoadPlayerData();
		}
		public void OnApplicationQuit()
		{
			UserController.Instance.SavePlayerData(Player);
		}
	}
}
