using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPVar : Singleton<HPVar>
{
    [Header("HPvar")]
    [Space(10)]
    
    [SerializeField] private Slider _hPVar;
    [SerializeField] private Slider _secondHpVar;

    private void Awake()
    {
        base.Awake();

        _secondHpVar.value = _hPVar.value;
    }
    
    private void Update()
    {
        if (_hPVar.value < _secondHpVar.value)
        {
            _secondHpVar.value -= 0.01f;
        }
    }

    public void SetHPVar(float hpRatio)
    {
        _hPVar.value = hpRatio;
    }

    public void SetSecondHPVar()
    {
        _secondHpVar.value = _hPVar.value;
    }
}
