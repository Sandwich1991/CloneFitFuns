
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public class Test : MonoBehaviour
{
    private GameObject _testObject;
    public void ResourceLoad(string path, Action<GameObject> evt)
    {
        var request = Resources.LoadAsync(path);
        
        StartCoroutine(CheckLoad(request));

        evt.Invoke(request.asset as GameObject);
    }
    
    IEnumerator CheckLoad(ResourceRequest request)
    {
        while (request.isDone == false)
        {
            yield return null;
        }
        
        print("2");
        
        yield break;
    }
    
    private void Start()
    {
        ResourceLoad("Prefabs/Cube", (obj) => _testObject = Instantiate(obj));
        
        print("1");
        
        print(_testObject.name);
    }
}
