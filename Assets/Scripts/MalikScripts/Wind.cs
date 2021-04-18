using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

/// <summary>
/// This class uses the Ontrigger events such as OnTriggerEnter2D, OnTriggerStay2D, and OnTriggerExit2D to detect when a collider enters the 
/// collider attached to the wind gameobject and adds a force away from the center of said object. This works in conjunction with the vector
/// field system.
/// </summary>
public class Wind : MonoBehaviour
{
    //Not Yet Fully Implemented
    public Vector2 startMousePos;
    public Vector2 currentMousePos;
    public Vector2 endMousePos;

    public float windRadius;
    public float pushStrength = 10;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = windRadius;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Object entered trigger");
    }

    /// <summary>
    /// This function uses OnTriggerStay2Dto detect when a collider is in the 
    /// collider attached to the wind gameobject and adds a force away from the center of said object.
    /// </summary>
    void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("Object is in trigger");
        Vector2 position = transform.position;
        Vector2 targetPosition = other.transform.position;
        Vector2 direction = targetPosition - position;
        direction.Normalize();
        other.attachedRigidbody.AddForce(new Vector3(direction.x * pushStrength, direction.y * pushStrength, 0));

    }
    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object left the trigger");
    }

    /// <summary>
    /// NOT YET IMPLEMENTED
    /// Determines when the mouse has been eiter pressed, released, or is still being pressed down and 
    /// saves when the mouse position when any of those are true. 
    /// </summary>
    private void MousePosCheck()
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