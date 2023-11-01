using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfHole : MonoBehaviour
{
    [HideInInspector]
    public bool isActive;

    private void Start()
    {
        isActive = true;
    }
}
