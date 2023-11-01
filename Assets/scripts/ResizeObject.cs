using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{

    private int currentSizeIndex = 0; // Индекс текущего размера
    [SerializeField]
    private int initSizeStageInd;//начальное состояние размера

    private Vector3 smallestSize; // Наименьший размер объекта
    [SerializeField]
    private float[] sizes; // Массив коэффициентов из возможных размеров объекта


    [SerializeField]
    private float resizeTime; 

    private void Start()
    {
        // Сохраняем изначальный размер объекта
        smallestSize = transform.localScale/sizes[initSizeStageInd];// Инициализируем массив размеров, начиная с изначального размера

        currentSizeIndex= initSizeStageInd;
    }

    public void Resize()
    {
        // Увеличиваем индекс размера (циклически)
        currentSizeIndex = (currentSizeIndex + 1) % sizes.Length;
        // Меняем размер объекта

        StopAllCoroutines();
        StartCoroutine("SlightlyyResize");

       //transform.localScale = smallestSize * sizes[currentSizeIndex];
    }


    IEnumerator SlightlyyResize()
    {
        Vector3 crntLocalScale= transform.localScale;
        float crntTime = 0f ;
        while (crntTime< resizeTime)
        {
            transform.localScale = Vector3.Lerp(crntLocalScale, smallestSize * sizes[currentSizeIndex],crntTime/resizeTime);
            crntTime += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

}