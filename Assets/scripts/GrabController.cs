using UnityEngine;

public class GrabController : MonoBehaviour
{
    private GameObject selectedObj;

    [SerializeField]
    private float offsetByGround;
    public AudioSource audioSource;

    [SerializeField]
    private Transform marker;

    private Vector3 delta;

    private void Update()
    {

        //��� ���������� ��������, ������� ����� �������������
        RaycastHit dragableHit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out dragableHit, 50f, LayerMask.GetMask("dragable") | LayerMask.GetMask("dragAnSize"));

        //��� ���������� �����������, �� ������� ����� ������� ��������
        RaycastHit groundHit;
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out groundHit, 50f);

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
                    audioSource.Play();
                    marker.gameObject.SetActive(true);
                    delta = selectedObj.transform.position - groundHit.point;
                }

            }
            //���� ���-�� ��� ���������������, �� ��������� ���
            else
            {
                selectedObj.GetComponent<Rigidbody>().isKinematic = false;
                marker.gameObject.SetActive(false);
                audioSource.Play();
                selectedObj = null;
            }
        }

        //���� �������� �������, ���������� ��� ������������ ����� ������� ���� �� �����������
        if (selectedObj)
        {
            selectedObj.transform.position = new Vector3(groundHit.point.x + delta.x, groundHit.point.y + offsetByGround+ selectedObj.transform.localScale.y/2f, groundHit.point.z + delta.z);

            RaycastHit hit;
            Physics.Raycast(selectedObj.transform.position, Vector3.down, out hit, 50f);

            marker.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        }



    }


}
