using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;


public class Wind : MonoBehaviour
{
    public GameObject windZone;

    public Vector2 startMousePos;
    public Vector2 currentMousePos;
    public Vector2 endMousePos;
    public List<Vector2> windSpots = new List<Vector2>();
    int moveSpeed = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered trigger");
    }

    void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("Object is in trigger");
        Vector2 position = transform.position;
        Vector2 targetPosition = other.transform.position;
        Vector2 direction = targetPosition - position;
        direction.Normalize();
        other.attachedRigidbody.AddForce(new Vector3(direction.x * moveSpeed, direction.y * moveSpeed, 0));

    }
    // void OnCollisionExit2D(Collider2D other)
    // {
    //     Debug.Log("Object left the trigger");
    // }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            startMousePos = Mouse.current.position.ReadValue();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            endMousePos = Mouse.current.position.ReadValue();
        }
        else
        {
            currentMousePos = Mouse.current.position.ReadValue();
        }
    }

}