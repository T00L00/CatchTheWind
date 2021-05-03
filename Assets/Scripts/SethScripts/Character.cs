using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public VectorFieldController vectorFieldController;
    public SwipeDetection swipeDetector;

    // Max amount of side-to-side force to apply for floating appearance
    public float sideForceNeg = -5f;
    public float sideForcePos = 5f;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeDetector = GameObject.Find("SwipeManager").GetComponent<SwipeDetection>();
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        rigidBody.AddForce(GetSide2SideForce() 
            + VectorField.VectorAtPosition(vectorFieldController.vectorField, rigidBody.transform.position)
            + swipeDetector.swipeForce);

        // Slowly decrease force over time
        swipeDetector.swipeForce *= 0.99f;
    }

    Vector3 GetSide2SideForce()
    {
        return new Vector3
        {
            x = Random.Range(sideForceNeg, sideForcePos)
        };
    }


}
