using StreetRacer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
	public int Id { get; private set; }
	public int PlayerCash { get; private set; } = 0;
	public List<int> Cars { get; private set; } = new List<int>() { 0 };
	public int CurrentCarIndex { get; set; } = 0;
	public Dictionary<MapType, int> Records { get; private set; }
	public string Token { get; private set; }
	
	public Player()
	{
	}
	public Player(int id, int playerCash, List<int> cars, Dictionary<MapType, int> records, int currentCarIndex, string token)
	{
		Id = id;
		PlayerCash = playerCash;
		Cars = cars;
		Records = records;
		CurrentCarIndex = currentCarIndex;
		Token = token;
	}
	public bool SetCurrentCar(int id)
	{
		if (Cars.Contains(id))
		{
			CurrentCarIndex = id;
			return true;
		}
		else
			return false;
	}
	public bool BuyCar(int id)
	{
		if (!Cars.Contains(id)) 
		{
			CarData data = GameManager.Singleton.Cars[id].GetComponent<Car>().CarData;
			if (data?.Price <= PlayerCash)
			{
				Cars.Add(id);
				PlayerCash -= data.Price;
				return true;
			}
		}
		return false;
	}
	public void AddPlayerCash(int cash)
	{
		PlayerCash += cash;
	}
}
