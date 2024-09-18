using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillCheat : MonoBehaviour
{
    [SerializeField] TMP_Text shipper;
    [SerializeField] TMP_Text cog;
    [SerializeField] TMP_Text Air;
    [SerializeField] TMP_Text ID;
    [SerializeField] TMP_Text Proper;
    [SerializeField] TMP_Text Class;
    [SerializeField] TMP_Text Packing;
    [SerializeField] TMP_Text Quaranty;
    [SerializeField] TMP_Text Packinginst;
    [SerializeField] TMP_Text Author;

    private void OnEnable()
    {
        shipper.text = BillFill.shipperText;
        cog.text = BillFill.cogateeText;
        Air.text = BillFill.airport;
        ID.text = BillFill.id;
        Proper.text = BillFill.proper;
        Class.text = BillFill.classOrDivision;
        Packing.text = BillFill.packing;
        Quaranty.text = BillFill.quaranty;
        Packinginst.text = BillFill.packinginst;
        Author.text = BillFill.author;
    }
}
