using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager instance;
    public GameObject mouseFollower;
    public float windRadius;
    public Vector2 mousePos;
    public Vector3 screenPos;
    public Collider2D[] colliders;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Input.mousePosition;
        screenPos.z = Camera.main.nearClipPlane;
        mousePos = Camera.main.ScreenToWorldPoint(screenPos);

        mouseFollower.transform.position = mousePos;

    }


}
