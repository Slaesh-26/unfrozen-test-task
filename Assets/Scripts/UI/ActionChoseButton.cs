using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionChoseButton : MonoBehaviour
{
    public event Action<UnitAction> onClick;
    
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;

    private UnitAction action;

    public void Init(UnitAction action)
    {
        text.text = action.GetText();
        this.action = action;
        
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        onClick?.Invoke(action);
    }
}
