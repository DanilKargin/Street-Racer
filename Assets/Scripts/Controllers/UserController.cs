using SharedLibrary;
using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserController : MonoBehaviour
{
	public static UserController Instance;
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
	public void SavePlayerData(Player player)
	{
		if (string.IsNullOrEmpty(player.Token))
		{
			Caching.Instance.SaveData(player);
		}
		else
		{

		}
	}
	public Player LoadPlayerData()
	{
		return Caching.Instance.LoadData();
	}
}
