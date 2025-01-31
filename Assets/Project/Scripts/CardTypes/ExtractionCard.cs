using CustomInspector;
using UnityEngine;

public class ExtractionCard : Card
{
    [ReadOnly][SerializeField][Foldout] ExtractionCardData extractionCardSO;
    [SerializeField]

    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        extractionCardSO = (ExtractionCardData)data;    
        cardTypeText.text = "Extraction";
    }

    void ExtractResource()
    {

    }
}
