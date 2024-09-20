using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LabelElement : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;

    public E_LABEL labelType;

    private IGroup labelGroup;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            labelGroup.SelectLabel(this);
        });
    }

    public void Setup(IGroup labelGroup)
    {
        this.labelGroup = labelGroup;
    }


    public void Selected(bool isSelected)
    {
        if (isSelected)
        {
            image.color = selectedColor;
        }
        else
        {
            image.color = normalColor;
        }
    }
}
