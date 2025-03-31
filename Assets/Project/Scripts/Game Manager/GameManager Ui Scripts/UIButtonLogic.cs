using GameManagerSystem;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;

[System.Serializable]
public class UIButtonData
{
    [HideInInspector] public string ButtonName;
    public ButtonType ButtonType;
    public Button Button;
}

[System.Serializable]
public class MenuRelation
{
    public string MenuName;
    public List<UIButtonData> UIButtonDataList;
}

[RequireComponent(typeof(GameManager))]
public class UIButtonLogic : MonoBehaviour
{
    [SerializeField] List<MenuRelation> MenuRelations;
    private List<Func<IEnumerator>> commandsToExecute = new List<Func<IEnumerator>>();

    private void Start()
    {
        foreach (var menuRelation in MenuRelations)
        {
            foreach (var buttonData in menuRelation.UIButtonDataList)
            {
                List<Func<IEnumerator>> commandsForListner = new List<Func<IEnumerator>>();
                CreateButtonCommand(buttonData.ButtonType, commandsForListner);
                buttonData.Button.onClick.AddListener(() => StartCoroutine(GameCommandExecutorForButton(commandsForListner)));
            }
        }
    }

    private void OnValidate()
    {
        foreach(var menuRelation in MenuRelations)
        {
            foreach(var buttonData in menuRelation.UIButtonDataList)
            {
                buttonData.ButtonName = buttonData.ButtonType + " Button";
            }
        }
    }

    void CreateButtonCommand(ButtonType buttonType, List<Func<IEnumerator>> commandList)
    {
        switch (buttonType)
        {
            case ButtonType.StartGame:
                commandList.Add(() => WrapActionAsCoroutine(GameManager.Instance.PlayGame));
                break;
            case ButtonType.PauseGame:
               // commandList.Add(() => WrapActionAsCoroutine(GameManager.Instance.TogglePause));
                break;
            case ButtonType.ResumeGame:
               // commandList.Add(() => WrapActionAsCoroutine(GameManager.Instance.TogglePause));
                break;
            case ButtonType.RestartGame:
                commandList.Add(() => WrapActionAsCoroutine(GameManager.Instance.RestartGame));
                break;
            case ButtonType.GoToMainMenu:
                commandList.Add(() => WrapActionAsCoroutine(GameManager.Instance.LoadMainMenu));
                break;
            case ButtonType.QuitGame:
                commandList.Add(() => WrapActionAsCoroutine(GameManager.Instance.QuitGame));
                break;
        }
    }

    IEnumerator WrapActionAsCoroutine(Action action)
    {
        action.Invoke();
        yield return null;
    }


    IEnumerator GameCommandExecutorForButton(List<Func<IEnumerator>> commands)
    {
        for (int i = 0; i < commands.Count; i++)
        {
            yield return StartCoroutine(commands[i].Invoke());
        }
    }
}