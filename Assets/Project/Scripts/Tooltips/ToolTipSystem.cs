using UnityEngine;

public class ToolTipSystem : MonoBehaviour
{
    [SerializeField] protected ToolTip toolTipObject;
    public static ToolTipSystem Instance { get; private set; }

    private void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        toolTipObject.gameObject.SetActive(false);
    }

    public void Show(Card card, CardData cardData)
    {
        if(card == null || cardData == null)
        {
            Debug.LogError(" unable to display tool tip as Card or CardData is null!");
            return;
        }
        if(toolTipObject == null)
        {
            Debug.LogError("unable to display tool tip as ToolTipObject is null!");
            return;
        }
        toolTipObject.SetContent(card, cardData);
        toolTipObject.gameObject.SetActive(true);
    }

    public void Hide()
    {
        toolTipObject.gameObject.SetActive(false);
    }

}