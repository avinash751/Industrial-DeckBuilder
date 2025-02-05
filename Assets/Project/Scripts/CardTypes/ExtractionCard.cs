using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class ExtractionCard : Card
{
    [SerializeField] private ExtractionCardData extractionCardData;
    [SerializeField] private Connector outputConnector;

    [SerializeField]bool isExtracting;
    public override void InitializeCard(CardData data)
    {
        base.InitializeCard(data);
        extractionCardData = (ExtractionCardData)data;
        cardTypeText.text = "Extraction";

        // Start the extraction process.
        
    }

    private void Update()
    {
        if(!isExtracting && outputConnector.IsConnected())
        {
            StartCoroutine(ExtractionRoutine());
            isExtracting = true;
        }
    }

    // Coroutine to spawn resources based on the extraction rate.
    IEnumerator ExtractionRoutine()
    {
        while (true)
        {
            SpawnResource();
            yield return new WaitForSeconds(1f / extractionCardData.ExtractionRate);
        }
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
            ResourceMover mover = resourceObj.GetComponent< ResourceMover>();
            mover.Initialize(outputConnector.conveyor.GetPathPoints());
        }
    }
}
