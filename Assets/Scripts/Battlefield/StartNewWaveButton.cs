using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNewWaveButton : MonoBehaviour
{
    [SerializeField] private Button startNewWaveButton;
    private void Awake()
    {
        BattlefieldEventManager.instance.StartNewWave += StartNewWave;
        BattlefieldEventManager.instance.WaveCleared += WaveCleared;
    }
    public void OnClick()
    {
        BattlefieldEventManager.instance.OnStartNewWave();
    }

    private void StartNewWave()
    {
        startNewWaveButton.interactable = false;
    }
    private void WaveCleared()
    {
        startNewWaveButton.interactable = true;
    }


}
