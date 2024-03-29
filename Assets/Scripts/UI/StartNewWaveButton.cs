using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartNewWaveButton : MonoBehaviour
{
    [SerializeField] private Button startNewWaveButton;

    public void OnClick()
    {
        BattlefieldEventManager.instance.OnStartNewWave();
    }

    public void SetInteractable(bool b)
    {
        startNewWaveButton.interactable = b;
    }


}
