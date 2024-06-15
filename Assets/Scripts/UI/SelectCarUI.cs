using System.Security.Cryptography.X509Certificates;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace StreetRacer {
    public class SelectCarUI : MonoBehaviour
    {
        [SerializeField]
        private GameObject _selectCarPanel, _selectionPanelHolder;
        [SerializeField]
        private GameObject _selectButton;
        [SerializeField]
        private GameObject _carHolder;
        [SerializeField]
        private GameObject _selectedButton;
        [SerializeField]
        private GameObject _buyButton;

        [SerializeField]
        private TMP_Text _carName;
		[SerializeField]
		private TMP_Text _carSpeed;
		[SerializeField]
		private TMP_Text _carControl;
		[SerializeField]
		private TMP_Text _carPrice;
        [SerializeField]
        private TMP_Text _playerCash;

		private int _carIndex = 0;
        private Vector3 _startCarHolderPosition;

		private void Start()
		{
			_carIndex = GameManager.Singleton.Player.CurrentCarIndex;
            _startCarHolderPosition = _carHolder.transform.position;
            _carHolder.transform.position -= Vector3.right * 30 * _carIndex;
		}
		public void OpenClosePanel(bool flag)
        {
            if (flag)
            {
                _selectionPanelHolder.SetActive(true);
				_carHolder.transform.position -= Vector3.right * 30 * _carIndex;
                SetPlayerCash();
				UpdateDisplayUI(GameManager.Singleton.Cars[_carIndex].GetComponent<Car>().CarData);
				SetButtonState();
			}
            else
            {
                _selectionPanelHolder.SetActive(false);
			}
        }
		public void UpdateDisplayUI(CarData carData)
		{
			_carName.text = carData.Name;
			_carSpeed.text = carData.Speed.ToString();
			_carControl.text = carData.Control.ToString();
			_carPrice.text = carData.Price.ToString();
		}
        public void ChangeCarIndex(bool direction)
        {
            switch (direction)
            {
                case true:
                    if (_carIndex < GameManager.Singleton.Cars.Count - 1)
                    {
                        _carIndex++;
                    }
                    break;
                case false:
                    if(_carIndex > 0)
                    {
                        _carIndex--;
                    }
                    break;
            }
            UpdateDisplayUI(GameManager.Singleton.Cars[_carIndex].GetComponent<Car>().CarData);
            SelectCarPos();
            SetButtonState();
        }
        private void SetPlayerCash()
        {
            _playerCash.text = GameManager.Singleton.Player.PlayerCash.ToString();
        }
        private void SelectCarPos()
        {
            _carHolder.transform.localPosition = new Vector3(-30 * _carIndex, 0, 0);
		}
		public void SelectCar()
        {
            if(GameManager.Singleton.Player.SetCurrentCar(_carIndex))
                SetButtonState();
        }
        public void BuyCar()
        {
            if (GameManager.Singleton.Player.BuyCar(_carIndex))
            {
                SetPlayerCash();
                SelectCar();
            }
        }
        private void SetButtonState()
        {
            if (GameManager.Singleton.Player.Cars.Contains(_carIndex))
            {
                if(_carIndex == GameManager.Singleton.Player.CurrentCarIndex)
                {
                    _selectedButton.SetActive(true);
                    _selectButton.SetActive(false);
                }
                else
				{
					_selectedButton.SetActive(false);
					_selectButton.SetActive(true);
				}
				_buyButton.SetActive(false);
			}
            else
            {
                _buyButton.SetActive(true);
                _selectButton.SetActive(false);
                _selectedButton.SetActive(false);
            }
        }
    }
}
