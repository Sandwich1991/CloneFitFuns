using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _rectTransform;

    private Canvas _canvas;

    private float _upperOfObject;
    private float _leftOfObject;
    private float _lowerOfObject;
    private float _rightOfObject;

    private bool _isLeftXout;
    private bool _isRightXout;

    private bool _isUpperYout;
    private bool _isLowerYout;

    private float _canvasXmin;
    private float _canvasXmax;
    private float _canvasYmin;
    private float _canvasYmax;

    [SerializeField] private GameObject _menuBar;
    private float _menuBarHeight;
    

    Canvas Canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = GetComponentInParent<Canvas>();
            }

            return _canvas;
        }
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / Canvas.scaleFactor;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        GetPosAndIsOut();

        if (_isLeftXout)
            _rectTransform.position += Vector3.right * Math.Abs(_leftOfObject - _canvasXmin);
        
        if (_isRightXout)
            _rectTransform.position += Vector3.left * Math.Abs(_rightOfObject - _canvasXmax);
        
        if (_isUpperYout)
            _rectTransform.position += Vector3.down * Math.Abs(_upperOfObject - _canvasYmax + _menuBarHeight);
        
        if (_isLowerYout)
            _rectTransform.position += Vector3.up * Math.Abs(_lowerOfObject - _canvasYmin);
    }

    void GetPosAndIsOut()
    {
        _leftOfObject = _rectTransform.position.x - (_rectTransform.sizeDelta.x / 2);
        _upperOfObject = _rectTransform.position.y + (_rectTransform.sizeDelta.y / 2);

        _rightOfObject = _rectTransform.position.x + (_rectTransform.sizeDelta.x / 2);
        _lowerOfObject = _rectTransform.position.y - (_rectTransform.sizeDelta.y / 2);

        _isLeftXout = Canvas.pixelRect.xMin > _leftOfObject;
        _isRightXout = Canvas.pixelRect.xMax < _rightOfObject;

        _isUpperYout = Canvas.pixelRect.yMax < _upperOfObject;
        _isLowerYout = Canvas.pixelRect.yMin > _lowerOfObject;

        _canvasXmin = Canvas.pixelRect.xMin;
        _canvasXmax = Canvas.pixelRect.xMax;
        _canvasYmin = Canvas.pixelRect.yMin;
        _canvasYmax = Canvas.pixelRect.yMax;

        _menuBarHeight = GetComponent<RectTransform>().rect.height;
    }
}
