using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Test88888888888 : MonoBehaviour
{
    [Header("������������ ������ ������ ������")]
    [SerializeField] private Transform _skinsParent;

    [Header("�������� ������� �� ������� ������")]
    [SerializeField] private GameObject[] _skinArray;
    [SerializeField] private int _currentSkin = 0;
    [SerializeField] private float x, y, width, height, _rotationSpeed;

    private Rect rect;

    private void Awake() => Initialize();

    private void Initialize()
    {
        ResetAllSkins();
        _skinArray[_currentSkin].SetActive(true);
    }

    public void SelectSkin(bool right)
    {
        ResetAllSkins();

        if (right)
            _currentSkin = (_currentSkin + 1) % _skinArray.Length;
        else
            _currentSkin = (_currentSkin - 1 + _skinArray.Length) % _skinArray.Length;


        switch (_currentSkin)
        {
            case 0: _skinArray[_currentSkin].SetActive(true);
                break; 
            
            case 1: _skinArray[_currentSkin].SetActive(true);
                break;

            case 2: _skinArray[_currentSkin].SetActive(true);
                break; 

            case 3: _skinArray[_currentSkin].SetActive(true);
                break;

            default:
                Debug.Log($"������! ��� ������ ��������: {_currentSkin}");
                break;
        }
    }

    private void ResetAllSkins()
    {
        for (int i = 0; i <= _skinArray.Length - 1; i++)
        {
            _skinArray[i].SetActive(false);
        }
    }

    private void Update()
    {
        rect = new Rect(x, y, width, height);

        if (Input.GetMouseButton(0))
        {
            // �������� ������� ����
            Vector3 mousePosition = Input.mousePosition;
            //Debug.Log(mousePosition);

            // ����������� Y-����������, ��� ��� � Unity (0,0) � ������ ����� ����, � � ���� � ������� �����
            mousePosition.y = Screen.height - mousePosition.y;
            //Debug.Log(" ����������� Y-���������� " + mousePosition);

            // ���������, ��������� �� ������ ���� � �������� ����
            if (rect.Contains(mousePosition))
            {
                // ���� ���� � ����, ��������� �������� �� ��� X
                float mouseX = -Input.GetAxis("Mouse X") * _rotationSpeed;

                if (mouseX != 0f)
                {
                    // ������������ ������, ���� �������� ���� ����
                    _skinsParent.transform.Rotate(Vector3.up, mouseX);
                }
            }
        }
    }
     private void OnGUI()
    {
        //GUI.Box(rect, "LOL");
    }
}
