using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plate : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material _material;
    
    private Vector3 _originScale;
    private float _expandRatio = 1.3f;
    private Color _originColor;

    void PlateColorAndSizeChange()
    {
        gameObject.transform.DOScale(transform.localScale * _expandRatio, 0.3f);
        _meshRenderer.material.color = new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f,
            Random.Range(0, 255) / 255f);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlateColorAndSizeChange();
    }

    private void OnTriggerExit(Collider other)
    {
        _material.color = _originColor;
        gameObject.transform.DOScale(_originScale, 0.3f);
    }
    
    private void Start()
    {
        _originScale = gameObject.transform.localScale;
        _meshRenderer = GetComponent<MeshRenderer>();
        _material = _meshRenderer.material;
        _originColor = _material.color;
    }
}
