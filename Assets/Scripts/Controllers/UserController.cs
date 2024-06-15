using SharedLibrary;
using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
	public async Task<List<RecordView>> GetRecords(RecordType recordType)
	{
		switch (recordType)
		{
			case RecordType.DAY:
				return await HttpClient.Get<List<RecordView>>("https://localhost:7232/getdayrecords");
			case RecordType.WEEK:
				return await HttpClient.Get<List<RecordView>>("https://localhost:7232/getweekrecords");
			case RecordType.MONTH:
				return await HttpClient.Get<List<RecordView>>("https://localhost:7232/getmonthrecords");
			default: 
				return null;
		}
	}
}
