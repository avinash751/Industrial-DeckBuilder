using UnityEngine;

public class ExtractionCard : Card
{
    [SerializeField] private ExtractionCardData extractionCardData;

    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        extractionCardData = (ExtractionCardData)data;
        cardTypeText.text = "Extraction";
    }

}

