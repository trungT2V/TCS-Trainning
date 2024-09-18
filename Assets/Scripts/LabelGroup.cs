using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LabelGroup : MonoBehaviour
{
    [SerializeField] private LabelElement[] elements;
    [SerializeField] private GameObject bill;

    private LabelElement currentLabelSelected;

    private void Start()
    {
        foreach(LabelElement e in elements)
        {
            e.Setup(this);
        }
    }

    public void SelectLabel(LabelElement label, bool billConfirm = false)
    {
        if(currentLabelSelected != null)
        {
            currentLabelSelected.Selected(false);
        }

        if(label.labelType == E_LABEL.BILL && billConfirm == false)
        {
            bill.SetActive(true);
            currentLabelSelected = null;
            CharacterMovementWithHeadBobbing.isActive = false;
        }
        else
        {
            currentLabelSelected = label;
            currentLabelSelected.Selected(true);
        }
    }

    public E_LABEL GetCurrentSelectedLabel()
    {
        if (currentLabelSelected == null)
            return E_LABEL.NONE;

        return currentLabelSelected.labelType;
    }

    public void ResetToNone()
    {
        if (currentLabelSelected != null)
        {
            currentLabelSelected.Selected(false);
            currentLabelSelected = null;
        }
    }
}
