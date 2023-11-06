using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeController : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(2)) // ��������� ������� �������� ����
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���������, ������� �� ��� �����-���� ������
            if (Physics.Raycast(ray, out hit, 50f, LayerMask.GetMask("sizeble") | LayerMask.GetMask("dragAnSize")))
            {
                // �������� ��������� Transform �������, �� ������� ��������� ���
                Transform hitTransform = hit.transform;


                //!!������������� �������� �� ����������� ��������� �������, ����� �������� ������ ����
                // ���������, ����������� �� ������ � ����, ������� �� ������ ��������
                // ������ (��� ������������� �������� ��� �������� ����)
                if (hitTransform.GetComponent<ResizeObject>())
                {
                    hitTransform.GetComponent<ResizeObject>().Resize();
                }
            }
        }
    }
}
