using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text _text;
    
    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    void Update()
    {
        _text.text = $"{Managers.Player.Trash}개의 쓰레기를 주웠습니다!";
    }
}
