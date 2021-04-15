using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// This class keeps track of the mouse position and oher various inputs in the game
/// </summary>
public class InputsManager : MonoBehaviour
{
    public static InputsManager instance;
    public GameObject mouseFollower;
    public Vector2 mousePos;
    public Vector3 screenPos;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Mouse.current.position.ReadValue();
        screenPos.z = Camera.main.nearClipPlane;
        mousePos = Camera.main.ScreenToWorldPoint(screenPos);

        mouseFollower.transform.position = mousePos;

    }


}
