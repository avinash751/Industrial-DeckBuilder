using CustomInspector;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI HeaderText;
    [SerializeField] List<Image> inputImages;
    [SerializeField] List<Image> outputImages;
    [SerializeField] TextMeshProUGUI upKeepCostText;
    [SerializeField] TextMeshProUGUI sellValueText;
    [SerializeField] TextMeshProUGUI stateText;
    [SerializeField] Color activeColor;
    [SerializeField] Color inactiveColor;
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Vector2 offset;
    [SerializeField] ReorderableDictionary<RawResource, Sprite> rawResourceSpriteLibrary;
    [SerializeField] ReorderableDictionary<RefinedResource, Sprite> refinedResourceSpriteLibrary;


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
        Vector2 customPivot = resolutionPivot.x >= 0.5f ? resolutionPivot + offset : resolutionPivot - offset;
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
            ClearAllSprites();
            outputImages.First().sprite = rawResourceSpriteLibrary[extractionCardData.ResourceToExtract];


        }
        else if (card is ProductionCard && cardData is ProductionCardData)
        {
            ProductionCardData processingCardData = (ProductionCardData)cardData;
            ProductionCard processingCard = (ProductionCard)card;
            stateText.text = processingCard.isProductionActive ? "Active" : "Inactive";
            stateText.color = processingCard.isProductionActive ? activeColor : inactiveColor;
            ClearAllSprites();
            List<RawResource> rawResourcesInput = processingCardData.ResourceInput.rawResources.ToList();
            List<RefinedResource> refinedResourcesInput = processingCardData.ResourceInput.refinedResources.ToList();
            SetInputSprites(rawResourcesInput, refinedResourcesInput);
            List<RawResource> rawResourcesOutput = processingCardData.ResourceOutput.rawResources.ToList();
            List<RefinedResource> refinedResourcesOutput = processingCardData.ResourceOutput.refinedResources.ToList();
            SetOutputSprites(rawResourcesOutput, refinedResourcesOutput);


        }
    }

    public void SetInputSprites(List<RawResource> rawResources, List<RefinedResource> refinedResources)
    {
        if(inputImages == null || inputImages.Count ==0)
        {
            return;
        }


        if(rawResources.Count + refinedResources.Count > inputImages.Count)
        {
            Debug.LogError("Too many input resources for the number of images available.");
            return;
        }

        int activeImages = inputImages.Count;
        for (int i = 0; i < activeImages; i++)
        {
            if (i < rawResources.Count)
            {
                inputImages[i].gameObject.SetActive(true);
                inputImages[i].sprite = rawResourceSpriteLibrary[rawResources[i]];
                activeImages--;
            }
            else
            {
                inputImages[i].gameObject.SetActive(false);
            }
        }
        for (int i = activeImages; i < inputImages.Count; i++)
        {
            if (i < refinedResources.Count)
            {
                inputImages[i].gameObject.SetActive(true);
                inputImages[i].sprite = refinedResourceSpriteLibrary[refinedResources[i]];
                activeImages--;
            }
            else
            {
                inputImages[i].gameObject.SetActive(false);
            }
        }

    }

    public void SetOutputSprites(List<RawResource> rawResources, List<RefinedResource> refinedResources)
    {
        if (outputImages == null || outputImages.Count == 0)
        {
            return;
        }
        if (rawResources.Count + refinedResources.Count > outputImages.Count)
        {
            Debug.LogError("Too many output resources for the number of images available.");
            return;
        }
        int activeImages = outputImages.Count;
        for (int i = 0; i < activeImages; i++)
        {
            if (i < rawResources.Count)
            {
                outputImages[i].gameObject.SetActive(true);
                outputImages[i].sprite = rawResourceSpriteLibrary[rawResources[i]];
                activeImages--;
            }
            else
            {
                outputImages[i].gameObject.SetActive(false);
            }
        }
        for (int i = activeImages; i < outputImages.Count; i++)
        {
            if (i < refinedResources.Count)
            {
                outputImages[i].gameObject.SetActive(true);
                outputImages[i].sprite = refinedResourceSpriteLibrary[refinedResources[i]];
                activeImages--;
            }
            else
            {
                outputImages[i].gameObject.SetActive(false);
            }
        }

    }

    void ClearAllSprites()
    {
        foreach (var image in inputImages)
        {
            image.gameObject.SetActive(false);
        }
        foreach (var image in outputImages)
        {
            image.gameObject.SetActive(false);
        }
    }


}