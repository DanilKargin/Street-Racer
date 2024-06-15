using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentTagPanel;
    public void OpenTagPanel(GameObject panel)
    {
        if(_currentTagPanel != null) 
        {
            _currentTagPanel.SetActive(false);
        }
        _currentTagPanel = panel;
		_currentTagPanel.SetActive(true);
    }

    public void LoadProfileTagPanel()
    {

    }
    public void VkAuthentication()
    {
        
    }
}
