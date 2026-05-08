using System;
using _Project.Scripts.Services.DataPersistence;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.UI
{
    public class SaveView : MonoBehaviour, IPointerClickHandler
    {
        public event Action Clicked;

        [SerializeField] private TMP_Text dateText;

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke();
        }

        public void Initialize(SaveData save)
        {
            dateText.text = GetDateString(save.Date);
        }

        private string GetDateString(DateTime dateTime) =>
            $"{dateTime.Year}.{dateTime.Month:00}.{dateTime.Day:00} {dateTime.Hour:00}:{dateTime.Minute:00}:{dateTime.Second:00}";
    }
}