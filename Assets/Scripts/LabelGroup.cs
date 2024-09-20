using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LabelGroup : MonoBehaviour, IGroup
{
    public Action<GameObject> OnSelectedLabel;
    public Action OnDeleteLabel;
    public Action<bool> OnTakeNote;
    public Action<string> OnSender;
    public Action<string> OnReceiver;
    public Action<string> OnNote;

    [SerializeField] private LabelElement[] elements;

    [Header("Reference")]
    [SerializeField] private GameObject billPanel;
    [SerializeField] private GameObject notePanel;
    [SerializeField] GameObject DGPanel;
    [SerializeField] GameObject HandlingPanel;

    private GameObject currentLabel;

    private void Start()
    {
        foreach (LabelElement e in elements)
        {
            e.Setup(this);
        }
    }

    public void SelectLabel(LabelElement label, bool billConfirm = false)
    {
        //if(currentLabelSelected != null)
        //{
        //    currentLabelSelected.Selected(false);
        //}

        //if(label.labelType == E_LABEL.BILL && billConfirm == false)
        //{
        //    bill.SetActive(true);
        //    currentLabelSelected = null;
        //    CharacterMovementWithHeadBobbing.isActive = false;
        //}
        //else
        //{
        //    currentLabelSelected = label;
        //    currentLabelSelected.Selected(true);
        //}

        if (currentLabel)
        {
            OnDeleteLabel?.Invoke();
            Destroy(currentLabel);
            currentLabel = null;
        }

        currentLabel = InstanceLabel(label.labelType);
        OnSelectedLabel?.Invoke(currentLabel);
    }

    private GameObject InstanceLabel(E_LABEL label)
    {
        GameObject result = Instantiate(Resources.Load("labels/" + label.ToString()) as GameObject);
        return result;
    }

    public void ClearLabel()
    {
        if (currentLabel)
        {
            OnDeleteLabel?.Invoke();
            Destroy(currentLabel);
            currentLabel = null;
        }
    }

    public void StickLabel()
    {
        currentLabel = null;
    }

    public void BTN_DG()
    {
        DGPanel.SetActive(true);
        HandlingPanel.SetActive(false);
        billPanel.SetActive(false);
        notePanel.SetActive(false);
        BTN_DoneTakeNote();
    }

    public void BTN_Handling()
    {
        DGPanel.SetActive(false);
        HandlingPanel.SetActive(true);
        billPanel.SetActive(false);
        notePanel.SetActive(false);
        BTN_DoneTakeNote();
    }

    public void IP_Sender(string value)
    {
        OnSender?.Invoke(value);
    }

    public void IP_Receiver(string value)
    {
        OnReceiver?.Invoke(value);
    }

    public void IP_Note(string value)
    {
        OnNote?.Invoke(value);
    }

    public void BTN_StartTakeNote()
    {
        CharacterMovementWithHeadBobbing.isActive = false;
        billPanel.SetActive(false);
        notePanel.SetActive(true);
        HandlingPanel.SetActive(false);
        DGPanel.SetActive(false);

        OnTakeNote?.Invoke(true);
    }

    public void BTN_DoneTakeNote()
    {
        CharacterMovementWithHeadBobbing.isActive = true;
        notePanel.SetActive(false);

        OnTakeNote?.Invoke(false);
    }

    internal void EnableBillFill()
    {
        DGPanel.SetActive(false);
        HandlingPanel.SetActive(false);
        billPanel.SetActive(true);
        notePanel.SetActive(false);

    }
}
