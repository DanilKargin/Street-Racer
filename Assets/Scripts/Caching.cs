using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using UnityEngine;

public class Caching : MonoBehaviour
{
	public static Caching Instance;
	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	public Player LoadData()
    {
		if (File.Exists(Application.persistentDataPath + "/SaveData.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath
			  + "/SaveData.dat", FileMode.Open);
			Player data = (Player)bf.Deserialize(file);
			file.Close();
			Debug.Log("Game data loaded!");
			return data;
		}
		else
		{
			Debug.Log("There is no save data!");
			return new Player();
		}
	}

    public void SaveData(Player player)
    {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath
		  + "/SaveData.dat");
		Player data = new Player(player.Id, player.PlayerCash, player.Cars, player.Records, player.CurrentCarIndex, "");
		bf.Serialize(file, data);
		file.Close();
		Debug.Log("Game data saved!");
	}
	public void ResetData()
	{
		if (File.Exists(Application.persistentDataPath
		  + "/SaveData.dat"))
		{
			File.Delete(Application.persistentDataPath
			  + "/SaveData.dat");
			Debug.Log("Data reset complete!");
		}
		else
			Debug.Log("No save data to delete.");
	}
}
