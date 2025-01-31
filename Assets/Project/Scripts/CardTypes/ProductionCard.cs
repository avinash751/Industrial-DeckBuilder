using CustomInspector;
using UnityEngine;

public class ProductionCard : Card
{
    [ReadOnly][SerializeField][Foldout] ProductionCardData productionCardSO;

    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        productionCardSO = (ProductionCardData)data;
        cardTypeText.text = "Production";
    }

    void ProduceResource()
    {

    }

}
