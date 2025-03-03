
namespace GameManagerSystem
{
    public enum GameBehaviorEventType
    {
        Initialized,// start of the game (Start)
        GameStarted,// game has started gameloop (Play)
        Paused,
        UnPaused,
        Win,
        Lose
    }
    public enum GameBehaviorType
    {
        Start,
        Play,
        Paused,
        Win,
        Lose
    }

    public enum  ButtonType
    {
        StartGame,
        PauseGame,
        ResumeGame,
        RestartGame,
        GoToMainMenu,
        QuitGame
    }
}
