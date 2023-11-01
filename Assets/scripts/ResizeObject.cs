using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{

    private int currentSizeIndex = 0; // ������ �������� �������
    [SerializeField]
    private int initSizeStageInd;//��������� ��������� �������

    private Vector3 smallestSize; // ���������� ������ �������
    [SerializeField]
    private float[] sizes; // ������ ������������� �� ��������� �������� �������


    [SerializeField]
    private float resizeTime; 

    private void Start()
    {
        // ��������� ����������� ������ �������
        smallestSize = transform.localScale/sizes[initSizeStageInd];// �������������� ������ ��������, ������� � ������������ �������

        currentSizeIndex= initSizeStageInd;
    }

    public void Resize()
    {
        // ����������� ������ ������� (����������)
        currentSizeIndex = (currentSizeIndex + 1) % sizes.Length;
        // ������ ������ �������

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