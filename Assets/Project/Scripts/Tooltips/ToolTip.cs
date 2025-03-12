using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HeaderText;
    [SerializeField] GameObject resourceInputObject;
    [SerializeField] GameObject resourceOutputObject;
    [SerializeField] TextMeshProUGUI upKeepCostText;
    [SerializeField] TextMeshProUGUI sellValueText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Vector2 offset;


    private void OnEnable()
    {
        if (rectTransform == null)
        {
          
            TryGetComponent(out rectTransform);
        }
        else
        {
            Vector2 mousePosition = Input.mousePosition;
            float pivotX = mousePosition.x / Screen.width;
            float pivotY = mousePosition.y / Screen.height;
            Vector2 resolutionPivot = new Vector2(pivotX, pivotY);
            Vector2 customPivot = resolutionPivot.x >= 0.5f ? resolutionPivot + offset : resolutionPivot - offset;
            rectTransform.pivot = customPivot;
            rectTransform.position = mousePosition;
        }
    }

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;
        Vector2 resolutionPivot = new Vector2(pivotX, pivotY);
        Vector2 customPivot = resolutionPivot.x >= 0.5f ? resolutionPivot+ offset : resolutionPivot - offset;
        rectTransform.pivot = customPivot;
        rectTransform.position = mousePosition;
    }
    public void SetContent(Card card, CardData cardData)
    {
        HeaderText.text = cardData.name;
        sellValueText.text = "Sell Value: $" + cardData.SellValue;
        upKeepCostText.text = "Upkeep Cost: $" + cardData.MonthlyUpKeepCost;

        if (card is ExtractionCard && cardData is ExtractionCardData)
        {
            ExtractionCardData extractionCardData = (ExtractionCardData)cardData;
            ExtractionCard extractionCard = (ExtractionCard)card;
            stateText.text = extractionCard.isExtracting ? "Active" : "Inactive";
            stateText.color = extractionCard.isExtracting ? activeColor : inactiveColor;
        }
        else if (card is ProductionCard && cardData is ProductionCardData)
        {
            ProductionCardData processingCardData = (ProductionCardData)cardData;
            ProductionCard processingCard = (ProductionCard)card;
            stateText.text = processingCard.isProductionActive ? "Active" : "Inactive";
            stateText.color = processingCard.isProductionActive ? activeColor : inactiveColor;
        }
    }
}