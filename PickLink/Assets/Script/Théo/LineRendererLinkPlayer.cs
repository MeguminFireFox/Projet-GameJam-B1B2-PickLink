using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererLinkPlayer : MonoBehaviour
{
    public LineRenderer lr;
    public List<GameObject> PlayersTransform;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < PlayersTransform.Count; i++)
        {
            lr.SetPosition(i, PlayersTransform[i].transform.position);
        }
    }
}
