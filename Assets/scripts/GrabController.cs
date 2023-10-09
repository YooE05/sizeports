using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    private GameObject selectedObj;

    [SerializeField]
    private float offsetByGround;

    [SerializeField]
    private Transform marker;

    private Vector3 delta;

    private void Update()
    {

        //��� ���������� ��������, ������� ����� �������������
        RaycastHit dragableHit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out dragableHit, 50f, LayerMask.GetMask("dragable"));

        //��� ���������� �����������, �� ������� ����� ������� ��������
        RaycastHit groundHit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out groundHit, 50f, LayerMask.GetMask("ground"));

        //�������� ������ ���
        if (Input.GetMouseButtonDown(0))
        {
            //���� ���� ������ �� ���������������
            if (selectedObj == null)
            {
                //� ���� �� ��������������� ��������, �� ����������� ���
                if (dragableHit.collider)
                {
             
                    dragableHit.rigidbody.isKinematic = true;
                    selectedObj = dragableHit.collider.gameObject;
                    selectedObj.transform.rotation = Quaternion.Euler(0, 0, 0); //����� �������� �� ��������� ������ �������� �������, �.�. �� ����� ���� ���������

                    marker.gameObject.SetActive(true);   
                    delta = selectedObj.transform.position - groundHit.point;
                }

            }
            //���� ���-�� ��� ���������������, �� ��������� ���
            else
            {
                selectedObj.GetComponent<Rigidbody>().isKinematic = false;
                marker.gameObject.SetActive(false);
                selectedObj = null;
            }
        }

        //���� �������� �������, ���������� ��� ������������ ����� ������� ���� �� �����������
        if (selectedObj)
        {
            selectedObj.transform.position = new Vector3(groundHit.point.x+ delta.x, groundHit.point.y + offsetByGround, groundHit.point.z+ delta.z);

            RaycastHit hit;
            Physics.Raycast(selectedObj.transform.position, Vector3.down,out hit, 50f);

            marker.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }



    }


}
