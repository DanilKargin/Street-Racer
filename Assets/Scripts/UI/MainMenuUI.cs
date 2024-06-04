using StreetRacer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField]
	private GameObject _settingsPanel;


	public void OpenSettings()
	{
		_settingsPanel.SetActive(true);
	}
	public void CloseSettingPanel()
	{
		_settingsPanel.SetActive(false);
	}
}
