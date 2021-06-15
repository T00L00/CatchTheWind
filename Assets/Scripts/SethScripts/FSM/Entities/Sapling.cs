using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public VectorFieldController vectorFieldController;
    public SwipeDetection swipeDetector;
    public LayerMask platformLayerMask;
    public LayerMask treeSiteLayerMask;
    public Animator animator { get; set; }
    private CapsuleCollider2D capsuleCollider;

    // Max amount of side-to-side force to apply for floating appearance
    public float sideForceNeg = -5f;
    public float sideForcePos = 5f;

    // State machine variables
    private StateMachine _stateMachine;
    public TreeSpot nearestTreeSpot { get; set; }
    public bool foundTreeSite { get; set; }
    public bool isGrounded { get; set; }
    public Vector3 groundMovementForce { get; set; }
    public int facing { get; set; } // 1 for right, -1 for left
    public bool enemyNear { get; set; }
    public bool atTreeSpot { get; set; }

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        swipeDetector = GameObject.Find("SwipeManager").GetComponent<SwipeDetection>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();

        _stateMachine = new StateMachine();

        // Create all possible states available to sapling
        var floating = new Floating(this);
        var search = new SearchForSiteToPlant(this);
        var moveToSite = new MoveToSite(this);
        var plantTree = new PlantTree(this);
        var panic = new Panic(this);
        var wander = new Wander(this);

        // Define transitions
        At(floating, search, Landed());
        At(search, moveToSite, HasTarget());
        At(search, panic, EnemyNear());
        At(moveToSite, plantTree, HasReachedTarget());
        At(plantTree, search, TreePlanted());

        At(search, floating, NoLongerGrounded());
        At(moveToSite, floating, NoLongerGrounded());
        At(plantTree, floating, NoLongerGrounded());

        _stateMachine.SetState(floating);

        // Method to transition to new state based on given condition
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        Func<bool> Landed() => () => IsGrounded() == true;
        Func<bool> NoLongerGrounded() => () => IsGrounded() == false;
        Func<bool> HasTarget() => () => (IsGrounded() == true) && (nearestTreeSpot != null);
        Func<bool> HasNoTarget() => () => (IsGrounded() == true) && (nearestTreeSpot == null);
        Func<bool> HasReachedTarget() => () => (IsGrounded() == true) && (nearestTreeSpot != null) && AtTreeSpot();
        Func<bool> TreePlanted() => () => (IsGrounded() == true) && (nearestTreeSpot.planted == true); // something needs to trigger planted change to true
        Func<bool> EnemyNear() => () => (enemyNear == true) && (IsGrounded() == true);
    }

    private void Start()
    {
        foundTreeSite = false;
        groundMovementForce = Vector3.zero;
        facing = 1;
        enemyNear = false;
        atTreeSpot = false;
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        // Apply all forces acting on sapling
        rigidBody.AddForce(
        VectorField.VectorAtPosition(vectorFieldController.vectorField, rigidBody.transform.position)
        + swipeDetector.swipeForce
        + groundMovementForce);

        // Slowly decrease swipe force over time
        swipeDetector.swipeForce *= 0.99f;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlatformEndPoint")
        {
            Flip();
            groundMovementForce *= -1;
        }
    }

    #region Functions

    /// <summary>
    /// Generate random side to side forces to mimic floating
    /// </summary>
    /// <returns></returns>
    Vector3 GetSide2SideForce()
    {
        return new Vector3
        {
            x = UnityEngine.Random.Range(sideForceNeg, sideForcePos)
        };
    }

    /// <summary>
    /// Apply a boxcast downward to check for ground
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        float extraHeightText = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, extraHeightText, platformLayerMask);

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

        isGrounded = raycastHit.collider != null;
        return isGrounded;
    } 

    /// <summary>
    /// Apply a boxcast in the direction of facing to check for tree spot
    /// </summary>
    public void FoundTreeSite()
    {
        // Make sure to search direction is same as facing direction
        Vector3 direction = Vector3.right;
        if (facing == -1) { direction = Vector3.left; }

        // Do a BoxCast that will only return something if a tree spot is found
        float distanceToCast = 3f;
        RaycastHit2D raycastHitForward = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, direction, distanceToCast, treeSiteLayerMask);
        Debug.DrawRay(capsuleCollider.bounds.center, direction * distanceToCast, Color.red);

        nearestTreeSpot = raycastHitForward.collider?.gameObject.GetComponent<TreeSpot>();
        if (nearestTreeSpot?.planted == false)
        {
            foundTreeSite = true;
            Debug.Log("Open tree spot found!");
            return;
        }
        
        // If tree spot has already been planted, do nothing
        if (nearestTreeSpot?.planted == true)
        {
            foundTreeSite = false;
            nearestTreeSpot = null;
            Debug.Log("Tree spot already planted.");
            return;
        }

    }

    public bool AtTreeSpot()
    {
        atTreeSpot = Vector2.Distance(transform.position, nearestTreeSpot.transform.position) < 0.5f;
        return atTreeSpot;
    }

    //Flip the player so it looks to the direction where it's going
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        facing *= -1;
    }

    public void GrowTree()
    {
        nearestTreeSpot?.animator.SetBool("finishedPlanting", true);
    }

    #endregion
}
