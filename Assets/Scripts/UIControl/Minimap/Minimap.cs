using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private Camera miniCam;
    [SerializeField] private RectTransform playerMarker;
    
    private RectTransform _map;

    void UpdatePlayerPos()
    {
        Vector2 playerPos = miniCam.WorldToViewportPoint(Managers.Player.PlayerPos);
        Rect mapRect = _map.rect;

        float xPos = playerPos.x * mapRect.width;
        float yPos = playerPos.y * mapRect.height;

        playerMarker.anchoredPosition = new Vector2(xPos, yPos);
    }
    
    private void Start()
    {
        _map = GetComponent<RectTransform>();
    }

    private void Update()
    {
        UpdatePlayerPos();
    }
}
