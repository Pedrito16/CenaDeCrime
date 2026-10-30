using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryEvidence : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI evidenceNameText;
    [SerializeField] TextMeshProUGUI evidenceDescriptionText;
    InventoryController inventoryController;
    void Start()
    {
        
        inventoryController = GetComponent<InventoryController>();
        if(inventoryController.inventoryItems.Count <= 0)
        {
            evidenceNameText.text = "Sem evidências!";
            evidenceDescriptionText.text = "Você ainda não coletou nenhuma evidência.";
        }
        inventoryController.onRecieveIndex.AddListener(OnChangeEvidence);
    }
    void OnChangeEvidence(int index)
    {
        EvidenceInfoSO evidence = inventoryController.inventoryItems[index].evidence;
        evidenceNameText.text = evidence.evidenceName;
        evidenceDescriptionText.text = evidence.evidenceDescription;
    }
}
