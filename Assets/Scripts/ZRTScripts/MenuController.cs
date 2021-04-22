using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject startGameText;
    [SerializeField] Animator startGameTextAnimator;

    [SerializeField] private CanvasGroup menuPanel;
    
    [SerializeField] int clickedTimer;
    
    private InputActions inputActions;
    

    private void Awake()
    {
        inputActions = new InputActions();
        DeactivePanel();
    }

    private void OnEnable()
    {
        inputActions.UI.Enable();
        inputActions.UI.Click.performed += onClickPressed;
    }

    private void OnDisable()
    {
        inputActions.UI.Click.performed -= onClickPressed;
    }

    private void onClickPressed(InputAction.CallbackContext context)
    {
        bool wasClicked = context.performed;
        if (wasClicked)
            OpenMenu();
    }

    private void OpenMenu()
    {
        StartCoroutine(AnimatorHandler());
        StartCoroutine(ActivatePanel());
        Debug.Log("Doing menu animations!");
        
    }

    private void DeactivePanel()
    {
        menuPanel.alpha = 0;
        menuPanel.interactable = false;
        menuPanel.blocksRaycasts = false;
    }

    private IEnumerator ActivatePanel()
    {
        yield return new WaitForSeconds(clickedTimer);
        menuPanel.alpha = 1;
        menuPanel.interactable = true;
        menuPanel.blocksRaycasts = true;
    }

    private IEnumerator AnimatorHandler()
    {
        startGameTextAnimator.SetTrigger("FadeText");
        yield return new WaitForSeconds(clickedTimer);
        startGameText.SetActive(false);
    }
}
