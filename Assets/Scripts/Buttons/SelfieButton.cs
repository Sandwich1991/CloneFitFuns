using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfieButton : MonoBehaviour
{
    private SelfieUI _selfieUI;
    private Button _selfieButton;
    private Camera _subCam;
    private bool _isActive = false;
    
    private void Awake()
    {
        _selfieButton = GetComponent<Button>();
    }

    private void Start()
    {
        _selfieButton.onClick.AddListener(SelfieMode);
    }

    void SelfieMode()
    {
        GameObject go = new GameObject { name = "SubCam" };
        _subCam = go.AddComponent<Camera>();
        _subCam.cullingMask = ~(1 << 6);

        SetCamPosition();
        MainUIOff();
        SetSelfieUI();
        
        Managers.Player.DisableMove();
    }

    void ExitSelfieMode()
    {
        Managers.Resource.Destroy(_subCam.gameObject);
        
        MainUIOn();
        RemoveSelfieUI();
        
        Managers.Player.EnableMove();
    }

    void SetCamPosition()
    {
        Vector3 playerPos = Managers.Player.PlayerPos;
        Transform player = Managers.Player.PlayerTransform;
        
        _subCam.transform.position = playerPos + player.TransformDirection(new Vector3(0f, 1.5f, 2f));
        _subCam.transform.rotation = Quaternion.Euler(19f, Managers.Player.PlayerRot.y + 180f, 0f);
    }

    void MainUIOn()
    {
        Game.MainUI.gameObject.SetActive(true);
    }

    void MainUIOff()
    {
        Game.MainUI.gameObject.SetActive(false);
    }

    void SetSelfieUI()
    {
        _selfieUI = Managers.Resource.Instantiate("SelfieUI").GetComponent<SelfieUI>();
        _selfieUI.exitButton.onClick.AddListener(ExitSelfieMode);
    }

    void RemoveSelfieUI()
    {
        Managers.Resource.Destroy(_selfieUI.gameObject);
    }
}
