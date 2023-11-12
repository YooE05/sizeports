using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    [HideInInspector]
    public bool isActive;

    [SerializeField]
    private GameObject flag;

    private void Start()
    {
        if (flag != null)
        {
            flag.SetActive(false);
        }
        isActive = true;
    }

    public void RiseFlag()
    {
        isActive = false;
        if (flag != null)
        {
            flag.SetActive(true);
        }

    }
}
