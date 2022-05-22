using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class JoystickInput : MonoBehaviour
{
    [SerializeField] private Button _buttonB;

    [SerializeField] private RectTransform _stickArea;
    [SerializeField] private RectTransform _stick;

    //variables de uso
    private float _maxMovement;

    private void Awake()
    {
        _maxMovement = _stickArea.GetComponent<RectTransform>().sizeDelta.x / 2;
    }

    public void ConfigureButtonB(UnityAction p_action)
    {
        ConfigureButton(p_action, _buttonB);
    }
    private void ConfigureButton(UnityAction p_action, Button p_button)
    {
        p_button.onClick.AddListener(p_action);
    }

    public Vector3 GetAxises()
    {
        return (_stick.position - _stickArea.position) / _maxMovement;
    }
    public float GetAxisVertical()
    {
        return (_stick.position - _stickArea.position).y / _maxMovement;
    }
    public float GetAxisHorizontal()
    {
        return (_stick.position - _stickArea.position).x / _maxMovement;
    }
    private void StickMovement()
    {
        _stick.transform.position = Input.mousePosition;//

        Vector3 stickDelta = _stick.position - _stickArea.position;

        if (stickDelta.magnitude>_maxMovement)
        {
            _stick.position =    _stickArea.position + stickDelta.normalized * _maxMovement;
        }
    }

    public void OnMouseButtonDown()
    {
        StickMovement();
    }
    public void OnMouseButtonUp()
    {
        _stick.localPosition = Vector3.zero;
    }
}
