using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] // Run before any other script 
public class InputSwipeManager : Singleton<InputSwipeManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event StartTouch OnEndTouch;
    #endregion

    private InputActions playerControls;
    private Camera mainCamera;

    private void Awake()
    {
        playerControls = new InputActions();
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.Player.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Player.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null)
        {
            OnStartTouch(Utils.ScreenToWorld(mainCamera, playerControls.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
        }
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null)
        {
            OnEndTouch(Utils.ScreenToWorld(mainCamera, playerControls.Player.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
        }
    }

    // Return position of finger - use for trail renderer
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, playerControls.Player.PrimaryPosition.ReadValue<Vector2>());
    }
}
