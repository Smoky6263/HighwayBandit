using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Test88888888888 : MonoBehaviour
{
    [Header("Родительский объект скинов игрока")]
    [SerializeField] private Transform _skinsParent;

    [Header("дочерние объекты со скинами игрока")]
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
                Debug.Log($"Ошибка! нет такого элемента: {_currentSkin}");
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
            // Получаем позицию мыши
            Vector3 mousePosition = Input.mousePosition;
            //Debug.Log(mousePosition);

            // Преобразуем Y-координату, так как в Unity (0,0) — нижний левый угол, а у мыши — верхний левый
            mousePosition.y = Screen.height - mousePosition.y;
            //Debug.Log(" Преобразуем Y-координату " + mousePosition);

            // Проверяем, находится ли курсор мыши в заданной зоне
            if (rect.Contains(mousePosition))
            {
                // Если мышь в зоне, проверяем движение по оси X
                float mouseX = -Input.GetAxis("Mouse X") * _rotationSpeed;

                if (mouseX != 0f)
                {
                    // Поворачиваем объект, если движение мыши есть
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
