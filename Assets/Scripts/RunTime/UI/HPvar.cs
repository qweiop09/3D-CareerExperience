using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPvar : Singleton<HPvar>
{
    
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
            Debug.Log("노란색 내려가는 중");
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
