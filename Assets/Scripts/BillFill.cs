using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BillFill : MonoBehaviour
{
    [SerializeField] BillCheat billCheat; 

    public static string shipperText;
    public static string cogateeText;
    public static string airport;
    public static string id;
    public static string proper;
    public static string classOrDivision;
    public static string packing;
    public static string quaranty;
    public static string packinginst;
    public static string author;

    public void ShipperText(string text)
    {
        shipperText = text;
    }

    public void CongateeText(string text)
    {
        cogateeText = text;
    }

    public void Airport(string text)
    {
        airport = text;
    }

    public void ID(string text)
    {
        id = text;
    }

    public void Proper(string text)
    {
        proper = text;
    }

    public void ClassOrDivision(string text)
    {
        classOrDivision = text;
    }

    public void Packing(string text)
    {
        packing = text;
    }

    public void Quaranty(string text)
    {
        quaranty = text;
    }

    public void Packinginst(string text)
    {
        packinginst = text;
    }

    public void Author(string text)
    {
        author = text;
    }

    public void Comfirm()
    {
        this.gameObject.SetActive(false);
        CharacterMovementWithHeadBobbing.isActive = true;
    }
}
