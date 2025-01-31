using UnityEngine;
using CustomInspector;

[CreateAssetMenu(fileName = "ExtractionCardData", menuName = "Scriptable Objects/ExtractionCardData")]
public class ExtractionCardData : CardData
{
    [HorizontalLine("Base Card Data Settings", 3, FixedColor.DustyBlue)]
    public RawResource ResourceToExtract;
    public int ExtractionRate;


}
