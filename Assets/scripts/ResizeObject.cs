using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{
    private Vector3[] sizes; // ������ �� ���� ��������� �������� �������
    private int currentSizeIndex = 0; // ������ �������� �������
    private Vector3 originalSize; // ����������� ������ �������

    private void Start()
    {
        // ��������� ����������� ������ �������
        originalSize = transform.localScale;// �������������� ������ ��������, ������� � ������������ �������
        sizes = new Vector3[]
        {
            originalSize,
            new Vector3(2.0f, 2.0f, 2.0f), // ������ ������
            new Vector3(1.5f, 1.5f, 1.5f), // ������ ������
        };
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(2)) // ��������� ������� �������� ����
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ���������, ������� �� ��� �����-���� ������
            if (Physics.Raycast(ray, out hit))
            {
                // �������� ��������� Transform �������, �� ������� ��������� ���
                Transform hitTransform = hit.transform;

                // ���������, ����������� �� ������ � ����, ������� �� ������ ��������
                // ������ (��� ������������� �������� ��� �������� ����)
                if (hitTransform.CompareTag("ResizableObject"))
                {
                    // ����������� ������ ������� (����������)
                    currentSizeIndex = (currentSizeIndex + 1) % sizes.Length;
                    // ������ ������ �������
                    hitTransform.localScale = sizes[currentSizeIndex];
                }
            }
        }
    }
}