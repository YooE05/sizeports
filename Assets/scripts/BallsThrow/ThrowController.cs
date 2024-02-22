using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] balls;

    private List<Vector3> ballsStartPosition = new List<Vector3>();

    [SerializeField]
    private Vector3 throwPlace;

    private int activeBallsCount;
    [SerializeField]
    private float[] throwForce;
    [SerializeField]
    private Vector3[] throwDirections;

    [SerializeField]
    private MeshRenderer[] lightsMeshRen;
    [SerializeField]
    private Material enableMat;
    [SerializeField]
    private Material disableMat;

    public AudioSource audioSource;
    [SerializeField]
    private GameObject[] tutorialObjs;
    public GameObject winner;
    [SerializeField]
    private AudioSource winAudio;


    private int points;

    private void OnEnable()
    {
        GolfMinigameController.ballRolledAtHole += AddBall;
        Gates.OnBallHitGoal += AddPoint;
    }

    private void OnDisable()
    {
        GolfMinigameController.ballRolledAtHole -= AddBall;
        Gates.OnBallHitGoal -= AddPoint;
    }

    private void Awake()
    {
        points = 0;
        activeBallsCount = 0;
        for (int i = 0; i < balls.Length; i++)
        {
            ballsStartPosition.Add(balls[i].transform.position);
        }
    }

    private void AddBall()
    {
        activeBallsCount++;
        if (activeBallsCount == 1)
        {
            StartCoroutine(ThrowBallsLoop());
        }
    }

    private void AddPoint()
    {
        // помен€ть цвет у фонар€, добавить очко
        lightsMeshRen[points].material = enableMat;
        points++;
    }
    IEnumerator ThrowBallsLoop()
    {
        for (int i = 0; i < activeBallsCount; i++)
        {
            //восставновить м€чи на их местах, обновить лампочки + задержка
            balls[i].transform.position = ballsStartPosition[i];
            balls[i].tag = "gateBall";
            balls[i].velocity = Vector3.zero;
            balls[i].angularVelocity = Vector3.zero;
            lightsMeshRen[i].material = disableMat;
        }
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < activeBallsCount; i++)
        {
            //телепорт перед игроком + задержка
            balls[i].position = throwPlace;
            yield return new WaitForSeconds(2f);

            //удар + задержка
            balls[i].AddForce(throwDirections[i] * throwForce[i], ForceMode.Impulse);

            audioSource.Play();

            yield return new WaitForSeconds(4f);
        }

        //подсчЄт очков(при недоборе начинаем заново)
        if (points < balls.Length)
        {
            points = 0;
            yield return null;
            StartCoroutine(ThrowBallsLoop());
        }
        else
        {
            winAudio.Play();
            Debug.Log("»гра завершена");
            foreach (var item in tutorialObjs)
            {
                item.SetActive(false);
            }
            winner.SetActive(true);
        }

    }

}
