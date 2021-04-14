using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    public GameObject mouseFollower;

    public Vector3 mousePos;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
        mousePos.z = Camera.main.farClipPlane;
        mouseFollower.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(mousePos, 2.5f);
    }
}
