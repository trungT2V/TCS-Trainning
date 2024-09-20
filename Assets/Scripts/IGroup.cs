using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGroup
{
    public void SelectLabel(LabelElement label, bool billConfirm = false);
}
