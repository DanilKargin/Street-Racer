using StreetRacer;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
	[SerializeField]
	private GameObject _itemHolder;
	[SerializeField]
	private GameObject _itemPrefab;
	
	public void DayToggle_OnChanged(bool flag)
	{
		Toogle_Changer(flag, RecordType.DAY);
	}
	public void WeekToggle_OnChanged(bool flag)
	{
		Toogle_Changer(flag, RecordType.WEEK);
	}
	public void MonthToggle_OnChanged(bool flag)
	{
		Toogle_Changer(flag, RecordType.MONTH);
	}
	private void ClearItemHolder()
	{
		while (_itemHolder.transform.childCount > 0)
		{
			DestroyImmediate(_itemHolder.transform.GetChild(0).gameObject);
		}
	}
	private async void Toogle_Changer(bool flag, RecordType recordType)
	{
		if (flag)
		{
			List<RecordView> list = await UserController.Instance.GetRecords(recordType);
			CreateItems(list);
		}
		else
		{
			ClearItemHolder();
		}
	}
	private void CreateItems(List<RecordView> list)
	{
		for (int i = 0; i < list.Count; i++)
		{
			GameObject itemObj = Instantiate(_itemPrefab);
			itemObj.transform.SetParent(_itemHolder.transform);
			itemObj.transform.Find("RankText").GetComponent<TMP_Text>().text = (i + 1).ToString();
			itemObj.transform.Find("NicknameText").GetComponent<TMP_Text>().text = list[i].PlayerName;
			itemObj.transform.Find("RideTypeText").GetComponent<TMP_Text>().text = list[i].RideType;
			itemObj.transform.Find("ScoreText").GetComponent<TMP_Text>().text = list[i].Score.ToString();
		}
	}
}
