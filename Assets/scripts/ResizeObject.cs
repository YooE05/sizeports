using System.Collections;
using UnityEngine;

public class ResizeObject : MonoBehaviour
{

    private int currentSizeIndex = 0; // ������ �������� �������
    [SerializeField]
    private int sceneSizeStageInd;//��������� ��������� ������� �� �����
    [SerializeField]
    private int startSizeStageInd;//��������� �� ������ ����

    private Vector3 smallestSize; // ���������� ������ �������
    [SerializeField]
    private float[] sizes; // ������ ������������� �� ��������� �������� �������

    [SerializeField]
    private float[] weights;


    [SerializeField]
    private float resizeTime;

    public bool changeWeight;

    private Rigidbody rb;

    public AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // ��������� ����������� ������ �������
        smallestSize = transform.localScale / sizes[sceneSizeStageInd];// �������������� ������ ��������, ������� � ������������ �������
        currentSizeIndex = startSizeStageInd;
        transform.localScale = smallestSize * sizes[startSizeStageInd];


        for (int i = 0; i < sizes.Length; i++)
        {
            if (sizes[i] == 0)
            {
                sizes[i] = 1;
            }
        }

        if (changeWeight)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] == 0)
                {
                    weights[i] = 1;
                }
            }
            rb.mass = weights[currentSizeIndex];
        }

        if (resizeTime == 0)
        {
            resizeTime = 0.05f;
        }



    }

    public void Resize()
    {
        // ����������� ������ ������� (����������)
        currentSizeIndex = (currentSizeIndex + 1) % sizes.Length;

        if (changeWeight)
        {
            //������ ���
            rb.mass = weights[currentSizeIndex];
        }
        // ������ ������ �������
        StopAllCoroutines();
        StartCoroutine("SlightlyyResize");
        audioSource.Play();
        //transform.localScale = smallestSize * sizes[currentSizeIndex];
    }


    IEnumerator SlightlyyResize()
    {
        Vector3 crntLocalScale = transform.localScale;
        float crntTime = 0f;
        while (crntTime < resizeTime)
        {
            transform.localScale = Vector3.Lerp(crntLocalScale, smallestSize * sizes[currentSizeIndex], crntTime / resizeTime);
            crntTime += Time.deltaTime;
            yield return null;
        }


        yield return null;
    }

    public float GetWeight()
    {
        return rb.mass;
    }

}