using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePrefs : MonoBehaviour
{
    [SerializeField] private int myLoadInt;


    private void Start()
    {
        PlayerPrefs.SetInt("Score",20);
        PlayerPrefs.SetFloat("FloatScore",20.0f);
        PlayerPrefs.SetString("Score","Cos");
    }

    public void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            LoadPrefs();
        }
    }

    private void LoadPrefs()
    {
        myLoadInt = PlayerPrefs.GetInt("Score");
    }
}
