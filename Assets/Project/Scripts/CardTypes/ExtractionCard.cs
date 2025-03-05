using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System.Linq;
using CustomInspector;

public class ExtractionCard : Card
{
    [SerializeField] private ExtractionCardData extractionCardData;
    [SerializeField] private Connector outputConnector;
    //[SerializeField] private CardFeedbacks cardFeedbacks; // Added CardFeedbacks reference - assign in inspector!

    [ReadOnly][SerializeField] bool isExtracting;

    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        extractionCardData = (ExtractionCardData)data;
        cardTypeText.text = "Extraction";
        outputConnector = connectorSpawner.GetConnectors(false).First();
    }

    private void Start()
    {
        if (MonthTimer.Instance == null) return;
        MonthTimer.Instance.OnMonthEnd += HandleEndOfMonthPayment;
    }

    private void OnDisable()
    {
        if (MonthTimer.Instance == null) return;
        MonthTimer.Instance.OnMonthEnd -= HandleEndOfMonthPayment;
    }

    protected override void HandleEndOfMonthPayment()
    {
        if (outputConnector == null ||!outputConnector.IsConnected())
            return;
        PayUpkeepCost();
    }

    private void Update()
    {
        if (!isExtracting && outputConnector.IsConnected())
        {
            StartCoroutine(ExtractionRoutine());
            isExtracting = true;
        }
    }

    // Coroutine to spawn resources based on the extraction rate.
    IEnumerator ExtractionRoutine()
    {
        while (outputConnector.IsConnected())
        {
      
            SpawnResource();
            yield return new WaitForSeconds(1f / extractionCardData.ExtractionRate);
        }
        isExtracting = false;
    }

    void SpawnResource()
    {
        if (extractionCardData.resourcePrefab == null || outputConnector == null)
            return;

        // Instantiate the resource at the output connector's position.
        GameObject resourceObj = Instantiate(extractionCardData.resourcePrefab, outputConnector.transform.position, Quaternion.identity);
        Resource res = resourceObj.GetComponent<Resource>();
        if (res != null)
        {
            res.isRefined = false;
            res.rawType = extractionCardData.ResourceToExtract;
        }

        if (outputConnector.conveyor != null)
        {
            ResourceMover mover = resourceObj.GetComponent<ResourceMover>();
            mover.Initialize(outputConnector.conveyor.GetPathPoints());
        }
    }
}