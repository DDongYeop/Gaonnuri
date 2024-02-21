using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AreaUI : MonoBehaviour
{
    [Header("Data")] 
    private AreaData _currentAreaData; 
    
    [Header("UI")]
    private Image _image;
    private TextMeshProUGUI _cntText;
    
    private void OnEnable()
    {
        _image = GetComponent<Image>();
        _cntText = transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>();
    }

    public void SetUp(AreaData areaData, Sprite sprite, int cnt)
    {
        _currentAreaData = areaData;
        _image.sprite = sprite;
        _cntText.text = cnt.ToString();
    }

    public void UseArea(int cnt)
    {
        _cntText.text = cnt.ToString();
    }
    
    public void ObjectSelect()
    {
        AreaManager.Instance.CurrentAreaData = _currentAreaData;
    }
}
