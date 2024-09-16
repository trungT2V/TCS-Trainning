using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BillFill : MonoBehaviour
{
    [SerializeField] TMP_InputField inputFieldShipper;
    [SerializeField] TMP_InputField inputFieldCogatee;
    [SerializeField] LabelGroup labelGroup;
    [SerializeField] LabelElement labelBill;

    public static string shipperText;
    public static string cogateeText;

    private void OnEnable()
    {
        inputFieldShipper.text = "";
        inputFieldCogatee.text = "";
    }

    public void ShipperText(string text)
    {
        shipperText = text;
    }

    public void CongateeText(string text)
    {
        cogateeText = text;
    }

    public void Comfirm()
    {
        labelGroup.SelectLabel(labelBill, true);
        this.gameObject.SetActive(false);
        CharacterMovementWithHeadBobbing.isActive = true;
    }
}
