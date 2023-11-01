using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
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
                if (hitTransform.GetComponent<ResizeObject>())
                {
                    hitTransform.GetComponent<ResizeObject>().Resize();
                }
            }
        }
    }
}
