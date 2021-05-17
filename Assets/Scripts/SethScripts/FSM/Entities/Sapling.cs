using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public VectorFieldController vectorFieldController;
    public SwipeDetection swipeDetector;
    public TreeSpotManager treeSpotManager;
    private BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask platformLayerMask;

    // Max amount of side-to-side force to apply for floating appearance
    public float sideForceNeg = -5f;
    public float sideForcePos = 5f;

    // State machine variables
    private StateMachine _stateMachine;
    public GameObject nearestTreeSpot { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeDetector = GameObject.Find("SwipeManager").GetComponent<SwipeDetection>();
        treeSpotManager = TreeSpotManager.Instance;
        boxCollider2d = GetComponent<BoxCollider2D>();

        _stateMachine = new StateMachine();

        // Create all possible states available to sapling
        var floating = new Floating();
        var search = new SearchForSiteToPlant(this);
        var moveToSite = new MoveToSite(this);
        var plantTree = new PlantTree();
        var panic = new Panic();
        var wander = new Wander();

        // Define transitions
        At(floating, search, Landed());
        At(search, moveToSite, HasTarget());
        At(moveToSite, plantTree, ReachedTarget());
        At(plantTree, search, TreePlanted());
        At(search, floating, NoLongerGrounded());
        At(moveToSite, floating, NoLongerGrounded());
        At(plantTree, floating, NoLongerGrounded());
        // At(search, wander, HasNoTarget());

        _stateMachine.SetState(floating);

        // Method to transition to new state based on given condition
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> Landed() => () => IsGrounded() == true;
        Func<bool> NoLongerGrounded() => () => IsGrounded() == false;
        Func<bool> HasTarget() => () => (IsGrounded() == true) && (nearestTreeSpot != null);
        Func<bool> HasNoTarget() => () => (IsGrounded() == true) && (nearestTreeSpot = null);
        Func<bool> ReachedTarget() => () => (IsGrounded() == true) && (nearestTreeSpot != null) && (Vector2.Distance(transform.position, nearestTreeSpot.transform.position) < 1f);
        Func<bool> TreePlanted() => () => (IsGrounded() == true) && (nearestTreeSpot != null) && (treeSpotManager.GetTreeSpotStatus(nearestTreeSpot) == true);
    }

    private void Update()
    {
        _stateMachine.Tick();
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
            x = UnityEngine.Random.Range(sideForceNeg, sideForcePos)
        };
    }

    private bool IsGrounded()
    {
        float extraHeightText = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);

        //Color rayColor;
        //if (raycastHit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}
        //Debug.DrawRay(boxCollider2d.bounds.center + new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        //Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, 0), Vector2.down * (boxCollider2d.bounds.extents.y + extraHeightText), rayColor);
        //Debug.DrawRay(boxCollider2d.bounds.center - new Vector3(boxCollider2d.bounds.extents.x, boxCollider2d.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2d.bounds.extents.x * 2f), rayColor);

        //Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
        
    } 
    
}
