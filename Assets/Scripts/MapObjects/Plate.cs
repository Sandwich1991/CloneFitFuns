using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plate : MonoBehaviour
{
    private MeshRenderer _meshRenderer;
    private Material _originmaterial;
    private List<Material> _materials;
    private int _listLength;
    private Vector3 _originScale;
    private float _expandRatio = 1.5f;

    void PlateColorAndSizeChange()
    {
        gameObject.transform.localScale = new Vector3(_originScale.x * _expandRatio, _originScale.y, _originScale.z * _expandRatio);
        _meshRenderer.material = _materials[Random.Range(0, _listLength - 1)];
    }
    
    private void OnTriggerEnter(Collider other)
    {
        PlateColorAndSizeChange();
    }

    private void OnTriggerExit(Collider other)
    {
        _meshRenderer.material = _originmaterial;
        gameObject.transform.localScale = _originScale;
    }
    
    private void Start()
    {
        _originScale = gameObject.transform.localScale;
        _meshRenderer = GetComponent<MeshRenderer>();
        _originmaterial = _meshRenderer.material;
        _materials = Utills.GetFileListInDir<Material>("PlateColor/");
        _listLength = _materials.Count;
    }
}
