using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BillCheat : MonoBehaviour
{
    [SerializeField] TMP_Text shipper;
    [SerializeField] TMP_Text cog;

    private void OnEnable()
    {
        shipper.text = BillFill.shipperText;
        cog.text = BillFill.cogateeText;
    }
}
