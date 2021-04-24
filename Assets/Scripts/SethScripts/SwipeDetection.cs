using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

    [SerializeField]
    private float minimumDistance = 0.2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField]
    private GameObject trail;
    [SerializeField]
    private float forceMultiplier = 5f;
    [SerializeField]
    private bool debugSwipe = false;

    private InputSwipeManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;
    public Vector2 swipeForce
    {
        get; set;
    }

    private Coroutine coroutine;

    private void Awake()
    {
        inputManager = InputSwipeManager.Instance;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = position;
        coroutine = StartCoroutine(Trail());
    }

    private IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryPosition();
            yield return null;
        }
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        trail.SetActive(false);
        StopCoroutine(coroutine);
        endPosition = position;
        endTime = time;
        swipeForce = forceMultiplier * DetectSwipe();
    }

    // Measure length and duration of swipe
    private Vector2 DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
        {
            if (debugSwipe)
            {
                Debug.Log("Swipe Detected");
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
            }            
            Vector3 direction = endPosition - startPosition;
            return new Vector2 { x = direction.x, y = direction.y }; // resulting force vector that will push sapling
        }
        return new Vector2 { x = 0, y = 0 };
    }


}
