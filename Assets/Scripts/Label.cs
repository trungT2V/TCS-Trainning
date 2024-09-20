using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E_LABEL
{
    NONE,
    FIRE,
    ARROW,
    AXIT,
    BILL,
    RAMEN, // tên của cái nhãn mới, nó phải trùng tên với cái prefab ngoài kia
}

public class Label : BaseInteractiveObject
{
    public E_LABEL labelType;


}
