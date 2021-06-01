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
    public GameObject nearestTreeSpot { get; set; }
    public bool foundTreeSite { get; set; }
    public bool isGrounded { get; set; }
    public Vector3 groundMovementForce { get; set; }
    public int facing { get; set; } // 1 for right, -1 for left
    public bool enemyNear { get; set; }

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
        var plantTree = new PlantTree();
        var panic = new Panic(this);
        var wander = new Wander(this);

        // Define transitions
        At(floating, search, Landed());
        At(search, moveToSite, HasTarget());
        At(search, panic, EnemyNear());
        At(moveToSite, plantTree, ReachedTarget());
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
        Func<bool> HasNoTarget() => () => (IsGrounded() == true) && (nearestTreeSpot = null);
        Func<bool> ReachedTarget() => () => (IsGrounded() == true) && (nearestTreeSpot != null) && (Vector2.Distance(transform.position, nearestTreeSpot.transform.position) < 2f);
        Func<bool> TreePlanted() => () => (IsGrounded() == true) && (nearestTreeSpot != null);
        Func<bool> EnemyNear() => () => (enemyNear == true) && (IsGrounded());
    }

    private void Start()
    {
        foundTreeSite = false;
        groundMovementForce = Vector3.zero;
        facing = 1;
        enemyNear = false;
    }

    private void Update()
    {
        _stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        //  Apply all forces acting on sapling
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

    Vector3 GetSide2SideForce()
    {
        return new Vector3
        {
            x = UnityEngine.Random.Range(sideForceNeg, sideForcePos)
        };
    }

    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
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

    public void FoundTreeSite()
    {
        float distanceToCast = 3f;
        Vector3 direction = Vector3.right;
        if (facing == -1) { direction = Vector3.left; }

        RaycastHit2D raycastHitForward = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, direction, distanceToCast, treeSiteLayerMask);
        // RaycastHit2D raycastHitBackward = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.left, distanceToCast, treeSiteLayerMask);
        Debug.DrawRay(capsuleCollider.bounds.center, direction * distanceToCast, Color.red);
        if (raycastHitForward.collider?.gameObject.tag == "TreeSpot")
        {
            foundTreeSite = true;
            nearestTreeSpot = raycastHitForward.collider.gameObject;
            Debug.Log("TreeSpot found!");
        }
    }

    //Flip the player so it looks to the direction where it's going
    void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        facing *= -1;
    }

    #endregion
}
