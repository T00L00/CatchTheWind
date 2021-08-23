using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace CTW.UI
{
    [DefaultExecutionOrder(-1)] // Run before any other script 
    public class PlayerInputManager : Singleton<PlayerInputManager>
    {
        #region Events
        public delegate void StartTouch(Vector2 position, float time);
        public event StartTouch OnStartTouch;
        public delegate void EndTouch(Vector2 position, float time);
        public event StartTouch OnEndTouch;
        #endregion

        private InputActions playerControls;
        private Camera mainCamera;

        public GameObject pauseMenuUI;
        private bool gameIsPaused = false;

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
            playerControls.Player.PauseMenu.performed += ctx => OpenPauseMenu(ctx);
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

        public void OpenPauseMenu(InputAction.CallbackContext context)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        // Return position of finger - use for trail renderer
        public Vector2 PrimaryPosition()
        {
            return Utils.ScreenToWorld(mainCamera, playerControls.Player.PrimaryPosition.ReadValue<Vector2>());
        }

        #region Menu functions
        public void Resume()
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            gameIsPaused = false;
        }

        private void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            gameIsPaused = true;
        }

        public void QuitGame()
        {
            Debug.Log("Quitting");
            Application.Quit();
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene(StaticFields.MAIN_MENU);
            Time.timeScale = 1f;
        }

        #endregion
    }
}
