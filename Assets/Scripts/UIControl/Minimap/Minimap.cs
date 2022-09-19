using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] Camera _miniCam;
    [SerializeField] private RectTransform _playerMarker;
    [SerializeField] private RectTransform _map;
    [SerializeField] private Button _minimapButton;

    private bool _isActive = false;

    private void Update()
    {
        Vector2 playerPos = _miniCam.WorldToViewportPoint(Managers.Player.PlayerPos);

        _playerMarker.anchoredPosition = new Vector2(playerPos.x * _map.rect.width, playerPos.y * _map.rect.height);
    }

    public void ActiveMap()
    {
        _map.gameObject.SetActive(_isActive? false : true);

        _isActive = _map.gameObject.activeSelf;
    }
    
    
}
