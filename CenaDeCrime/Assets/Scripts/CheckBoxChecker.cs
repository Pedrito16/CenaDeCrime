using UnityEngine;

public class CheckBoxChecker : MonoBehaviour
{
    [SerializeField] CheckBox[] checkBoxes;
    public bool AnsweredCheckBox()
    {
        for(int i = 0; i < checkBoxes.Length; i++)
        {
            if (checkBoxes[i].isChecked) return true;
        }
        return false;
    }
}
