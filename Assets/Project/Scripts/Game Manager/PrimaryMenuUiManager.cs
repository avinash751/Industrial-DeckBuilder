using GameManagerSystem.GameBehaviors;
using UnityEngine;

namespace GameManagerSystem.UI
{
    public class PrimaryMenusUIManager : MonoBehaviour
    {
        [Header("Menu GameObjects")]
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject loseMenu;

        private GameManager gameManager;
        private StartBehavior startBehavior;
        private PlayBehavior playBehavior;
        private PauseBehavior pauseBehavior;
        private WinBehavior winBehavior;
        private LoseBehavior loseBehavior;

        private void Awake()
        {
            gameManager = GameManager.Instance;
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in the scene. Make sure GameManager script is present.");
                return;
            }

            startBehavior = gameManager.GetBehavior<StartBehavior>();
            playBehavior = gameManager.GetBehavior<PlayBehavior>();
            pauseBehavior = gameManager.GetBehavior<PauseBehavior>();
            winBehavior = gameManager.GetBehavior<WinBehavior>();
            loseBehavior = gameManager.GetBehavior<LoseBehavior>();

        }

        private void Start()
        {
            if (startBehavior != null) startBehavior.OnBehaviorEvent += OnBehaviorEventHandler;
            if (playBehavior != null) playBehavior.OnBehaviorEvent += OnBehaviorEventHandler;
            if (pauseBehavior != null) pauseBehavior.OnBehaviorEvent += OnBehaviorEventHandler;
            if (winBehavior != null) winBehavior.OnBehaviorEvent += OnBehaviorEventHandler;
            if (loseBehavior != null) loseBehavior.OnBehaviorEvent += OnBehaviorEventHandler;
        }

        private void OnDisable()
        {
            if (startBehavior != null) startBehavior.OnBehaviorEvent -= OnBehaviorEventHandler;
            if (playBehavior != null) playBehavior.OnBehaviorEvent -= OnBehaviorEventHandler;
            if (pauseBehavior != null) pauseBehavior.OnBehaviorEvent -= OnBehaviorEventHandler;
            if (winBehavior != null) winBehavior.OnBehaviorEvent -= OnBehaviorEventHandler;
            if (loseBehavior != null) loseBehavior.OnBehaviorEvent -= OnBehaviorEventHandler;
        }

        #region Menu Visibility Control - LAMBDA EXPRESSIONS

        public void ShowStartMenu() => SetMenuVisibility(startMenu, true);
        public void HideStartMenu() => SetMenuVisibility(startMenu, false);
        public void ShowPauseMenu() => SetMenuVisibility(pauseMenu, true);
        public void HidePauseMenu() => SetMenuVisibility(pauseMenu, false);
        public void ShowWinMenu() => SetMenuVisibility(winMenu, true);
        public void HideWinMenu() => SetMenuVisibility(winMenu, false);
        public void ShowLoseMenu() => SetMenuVisibility(loseMenu, true);
        public void HideLoseMenu() => SetMenuVisibility(loseMenu, false);

        public void HideAllMenus()
        {
            HideStartMenu();
            HidePauseMenu();
            HideWinMenu();
            HideLoseMenu();
        }

        private void SetMenuVisibility(GameObject menu, bool visible)
        {
            if (menu != null)
            {
                menu.SetActive(visible);
            }
        }

        #endregion

        #region Event Handlers

        private void OnBehaviorEventHandler(GameBehaviorEventType eventType)
        {
            Debug.Log("Menu Manager Event Handler: " + eventType);

            switch (eventType)
            {
                case GameBehaviorEventType.Initialized:
                    ShowStartMenu();
                    HidePauseMenu();
                    HideWinMenu();
                    HideLoseMenu();
                    break;
                case GameBehaviorEventType.GameStarted:
                    HideAllMenus();
                    break;
                case GameBehaviorEventType.Paused:
                    ShowPauseMenu();
                    HideAllMenusExcept(pauseMenu);
                    break;
                case GameBehaviorEventType.UnPaused:
                    HidePauseMenu();
                    HideAllMenus();
                    break;
                case GameBehaviorEventType.Win:
                    ShowWinMenu();
                    HideAllMenusExcept(winMenu);
                    break;
                case GameBehaviorEventType.Lose:
                    ShowLoseMenu();
                    HideAllMenusExcept(loseMenu);
                    break;
                default:
                    Debug.LogWarning("PrimaryMenusUIManager - unhandled event type: " + eventType);
                    break;
            }
        }

        private void HideAllMenusExcept(GameObject menuToShow)
        {
            HideAllMenus();
            SetMenuVisibility(menuToShow, true);
        }

        #endregion
    }
}
