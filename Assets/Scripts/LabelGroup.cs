using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelGroup : MonoBehaviour
{
    [SerializeField] private LabelElement[] elements;

    private LabelElement currentLabelSelected;

    private void Start()
    {
        foreach(LabelElement e in elements)
        {
            e.Setup(this);
        }
    }

    public void SelectLabel(LabelElement label)
    {
        if(currentLabelSelected != null)
        {
            currentLabelSelected.Selected(false);
        }

        currentLabelSelected = label;
        currentLabelSelected.Selected(true);
    }

    public E_LABEL GetCurrentSelectedLabel()
    {
        if (currentLabelSelected == null)
            return E_LABEL.NONE;

        return currentLabelSelected.labelType;
    }
}
