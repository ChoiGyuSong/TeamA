using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player1 : PlayerBase
{
    /// <summary>
    /// ���ӸŴ���
    /// </summary>
    GameManager gameManager;
    PlayerBase playerBase;

    Text[] text;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
    }
}
