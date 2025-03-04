using GameManagerSystem.GameBehaviors;
using System;
using UnityEngine;

namespace GameManagerSystem.UI
{
    [Serializable]
    public class PrimaryMenusUIManager: MonoBehaviour 
    {
        [Header("Game UI Objects")]
        [SerializeField] private GameObject inGameUI;
        [Header("Menu GameObjects")]
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject loseMenu;


        #region Menu & UI Visibility Control - LAMBDA EXPRESSIONS

        public void ShowStartMenu() => SetUIVisibility(startMenu, true);
        public void HideStartMenu() => SetUIVisibility(startMenu, false);
        public void ShowPauseMenu() => SetUIVisibility(pauseMenu, true);
        public void HidePauseMenu() => SetUIVisibility(pauseMenu, false);
        public void ShowWinMenu() => SetUIVisibility(winMenu, true);
        public void HideWinMenu() => SetUIVisibility(winMenu, false);
        public void ShowLoseMenu() => SetUIVisibility(loseMenu, true);
        public void HideLoseMenu() => SetUIVisibility(loseMenu, false);
        public void ShowInGameUI() => SetUIVisibility(inGameUI, true);
        public void HideInGameUI() => SetUIVisibility(inGameUI, false);

        #endregion

        public void HideAllGameMenus()
        {
            HideStartMenu();
            HidePauseMenu();
            HideWinMenu();
            HideLoseMenu();
        }

        public void HideAllUI()
        {
            HideInGameUI();
            HideAllGameMenus();
        }

        private void SetUIVisibility(GameObject menu, bool visible)
        {
            if (menu != null)
            {
                menu.SetActive(visible);
            }
        }
        private void HideAllMenusExcept(GameObject menuToShow)
        {
            HideAllGameMenus();
            SetUIVisibility(menuToShow, true);
        }
    }
}
