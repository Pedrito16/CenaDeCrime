using UnityEngine;

[CreateAssetMenu(fileName = "EvidenceInfoSO", menuName = "Scriptable Objects/EvidenceInfo")]
public class EvidenceInfoSO : ScriptableObject
{
    public string evidenceName;
    [TextArea]
    public string evidenceDescription;
}
