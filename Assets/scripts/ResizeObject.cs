using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{
    private Vector3[] sizes; // Массив из трех возможных размеров объекта
    private int currentSizeIndex = 0; // Индекс текущего размера
    private Vector3 originalSize; // Изначальный размер объекта

    private void Start()
    {
        // Сохраняем изначальный размер объекта
        originalSize = transform.localScale;// Инициализируем массив размеров, начиная с изначального размера
        sizes = new Vector3[]
        {
            originalSize,
            new Vector3(2.0f, 2.0f, 2.0f), // Второй размер
            new Vector3(1.5f, 1.5f, 1.5f), // Третий размер
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2)) // Проверяем нажатие колесика мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Проверяем, пересек ли луч какой-либо объект
            if (Physics.Raycast(ray, out hit))
            {
                // Получаем компонент Transform объекта, на который указывает луч
                Transform hitTransform = hit.transform;

                // Проверяем, принадлежит ли объект к слою, который вы хотите изменить
                // размер (при необходимости добавьте или измените слои)
                if (hitTransform.CompareTag("ResizableObject"))
                {
                    // Увеличиваем индекс размера (циклически)
                    currentSizeIndex = (currentSizeIndex + 1) % sizes.Length;
                    // Меняем размер объекта
                    hitTransform.localScale = sizes[currentSizeIndex];
                }
            }
        }
    }
}