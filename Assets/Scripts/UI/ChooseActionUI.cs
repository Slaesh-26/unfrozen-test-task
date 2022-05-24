using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseActionUI : MonoBehaviour
{
    public event Action<UnitAction> unitActionChosen;
    public event Action skipTurn;

    [SerializeField] private ActionChoseButton actionButtonPrefab;
    [SerializeField] private RectTransform spawnOrigin;
    [SerializeField] private Button skipTurnButton;

    private ActionChoseButton actionButton;

    public void Init()
    {
        actionButton = Instantiate(actionButtonPrefab, spawnOrigin);
        spawnOrigin.gameObject.SetActive(false);
    }
    
    public void Show(UnitAction action)
    {
        actionButton.Init(action);
        spawnOrigin.gameObject.SetActive(true);
        
        skipTurnButton.onClick.AddListener(OnSkipTurnClicked);
        actionButton.onClick += OnActionChosen;
    }

    public void Hide()
    {
        skipTurnButton.onClick.RemoveListener(OnSkipTurnClicked);
        actionButton.onClick -= OnActionChosen;
        
        spawnOrigin.gameObject.SetActive(false);
    }

    private void OnSkipTurnClicked()
    {
        skipTurn?.Invoke();
    }

    private void OnActionChosen(UnitAction action)
    {
        unitActionChosen?.Invoke(action);
    }
}
