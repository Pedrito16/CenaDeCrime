    using UnityEngine;

public class CheckBoxChecker : MonoBehaviour
{
    [SerializeField] bool isChecked = false;
    [SerializeField] CheckBox[] checkBoxes;
    public bool AnsweredCheckBox()
    {
        for(int i = 0; i < checkBoxes.Length; i++)
        {
            if (checkBoxes[i].isChecked)
            {
                isChecked = true;
                print("estou checado: " + checkBoxes[i].name);
                return true;
            }
        }
        isChecked = false;
        return false;
    }
}
