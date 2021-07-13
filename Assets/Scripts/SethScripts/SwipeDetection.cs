using System.Collections;
using UnityEngine;
using CTW;

namespace CTW.UI
{
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

        public Sapling character;
        public float swipeDampener = 1f;

        private InputSwipeManager inputManager;

        private Vector3 startPosition;
        private float startTime;
        private Vector3 endPosition;
        private float endTime;
        public Vector3 swipeForce
        {
            get; set;
        }

        private Coroutine coroutine;

        private void Awake()
        {
            inputManager = InputSwipeManager.Instance;
        }

        private void Start()
        {
            swipeForce = Vector3.zero;
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
            DetectSwipe();
        }

        /// <summary>
        /// Determine swipe force that is applied to character based on length of swipe and distance away from character
        /// </summary>
        /// <returns></returns>
        private void DetectSwipe()
        {
            if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) <= maximumTime)
            {
                Vector3 direction = endPosition - startPosition;        // Direction of force
                Vector3 center = (endPosition + startPosition) * 0.5f;  // Center of force vector

                // Scale the swipe force according to how far away the swipe was made from rigidbody
                // Closer the swipe, stonger the force
                float distanceFromRb = Vector3.Magnitude(character.rigidBody.transform.position - center);
                swipeForce = direction * Mathf.Exp(-swipeDampener * distanceFromRb) * forceMultiplier;

                if (debugSwipe)
                {
                    Debug.Log("Swipe Detected");
                    Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
                }
            }
        }

        // Need to implement collider detector for swipe
        // If collider detects swipe, apply unscaled force
        // If collider doesn't detect swipe, apply scaled force
    }
}
