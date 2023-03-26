using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyStar : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private SettingsPanel _settingsPanel;

    public int id;
    public bool isChecked;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _settingsPanel.ShowStarsVisualFeedback(id);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _settingsPanel.HideStarsVisualFeedback(id);
    }
}
