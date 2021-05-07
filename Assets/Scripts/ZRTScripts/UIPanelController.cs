using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : MonoBehaviour
{

    [SerializeField] private CanvasGroup menuPanel;
    
    public void DeactivePanel()
    {
        menuPanel.alpha = 0;
        menuPanel.interactable = false;
        menuPanel.blocksRaycasts = false;
    }
    
    public void ActivePanel()
    {
        menuPanel.alpha = 1;
        menuPanel.interactable = true;
        menuPanel.blocksRaycasts = true;
    }
}
