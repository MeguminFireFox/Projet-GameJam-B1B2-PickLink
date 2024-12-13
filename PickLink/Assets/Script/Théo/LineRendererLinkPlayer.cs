using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererLinkPlayer : MonoBehaviour
{
    LineRenderer lr;
    public Transform[] PlayersTransform;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = PlayersTransform.Length;
    }

    private void Update()
    {
        for (int i = 0; i < PlayersTransform.Length; i++)
        {
            lr.SetPosition(i, PlayersTransform[i].position);
        }
    }
}
