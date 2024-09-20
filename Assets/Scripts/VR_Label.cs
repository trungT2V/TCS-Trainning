using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VR_Label : MonoBehaviour, IGroup
{
    [SerializeField] VR_Raycast hand;
    [SerializeField] private LabelElement[] elements;

    [Space]
    [SerializeField] GameObject labelAxitPrefab;
    [SerializeField] GameObject labelFirePrefab;
    [SerializeField] GameObject labelArowPrefab;

    private GameObject currentLabel;

    void Start()
    {
        foreach (LabelElement e in elements)
        {
            e.Setup(this);
        }
    }

    public void StickLabel()
    {
        currentLabel = null;
    }

    public void ClearLabel()
    {
        hand.ClearLabel();
        Destroy(currentLabel);
        currentLabel = null;
    }

    public void SelectLabel(LabelElement label, bool billConfirm = false)
    {
        if (currentLabel)
        {
            Destroy(currentLabel);
            currentLabel = null;
        }

        currentLabel = InstanceLabel(label.labelType);
        hand.SetLabel(currentLabel.transform);
    }

    private GameObject InstanceLabel(E_LABEL label)
    {
        GameObject result = null;

        switch (label)
        {
            case E_LABEL.FIRE:
                result = Instantiate(labelFirePrefab);
                break;
            case E_LABEL.AXIT:
                result = Instantiate(labelAxitPrefab);
                break;
            case E_LABEL.ARROW:
                result = Instantiate(labelArowPrefab);
                break;
        }

        return result;
    }
}
