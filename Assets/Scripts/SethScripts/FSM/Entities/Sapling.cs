using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public VectorFieldController vectorFieldController;
    public SwipeDetection swipeDetector;

    // Max amount of side-to-side force to apply for floating appearance
    public float sideForceNeg = -5f;
    public float sideForcePos = 5f;

    // State machine variables
    private StateMachine _stateMachine;

    // Need to implement a tree site manager that will keep track of which sites have been planted
    public Transform target { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeDetector = GameObject.Find("SwipeManager").GetComponent<SwipeDetection>();

        _stateMachine = new StateMachine();

        // Create all possible states available to sapling
        var search = new SearchForSiteToPlant();
        var moveToSite = new MoveToSite();
        var plantTree = new PlantTree();
        var panic = new Panic();
        var wander = new Wander();

        At(search, moveToSite, HasTarget());
        At(moveToSite, plantTree, ReachedTarget());
        At(plantTree, search, TreePlanted());
        At(search, wander, HasNoTarget());

        // Method to transition to new state based on given condition
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> HasTarget() => () => target != null;
        Func<bool> HasNoTarget() => () => target = null;
        Func<bool> ReachedTarget() => () => target != null && Vector2.Distance(transform.position, target.transform.position) < 1f;
        Func<bool> TreePlanted() => () => // Need boolean to signify that tree has been planted
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
