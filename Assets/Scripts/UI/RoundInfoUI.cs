using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoundInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tmPro;

    public void SetRound(int round)
    {
        tmPro.text = $"Round {round}";
    }
}
