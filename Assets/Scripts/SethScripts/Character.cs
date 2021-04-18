using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public VectorFieldController vectorFieldController;

    // Max amount of side-to-side force to apply for floating appearance
    public float sideForceNeg = -5f;
    public float sideForcePos = 5f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(GetSide2SideForce() + VectorField.VectorAtPosition(vectorFieldController.vectorField, rigidBody.transform.position));
    }

    Vector2 GetSide2SideForce()
    {
        return new Vector2
        {
            x = Random.Range(sideForceNeg, sideForcePos)
        };
    }
}
