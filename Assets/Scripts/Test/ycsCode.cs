using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Video;
using Object = UnityEngine.Object;

public class ResourceLoadJob
{
    public string Path;
    public Action<GameObject> Evt;

    public ResourceLoadJob(string path, Action<GameObject> evt)
    {
        Path = path;
        Evt = evt;
    }
}

public class ycsCode : MonoBehaviour
{
    private GameObject _testObject;

    private Queue<ResourceLoadJob> resourceQ = new Queue<ResourceLoadJob>(); 

    
    public void ResourceLoad(string path, Action<GameObject> evt)
    {
        resourceQ.Enqueue(new ResourceLoadJob(path, evt));
    }
    

    IEnumerator CheckLoad(ResourceLoadJob job)
    {
        var request = Resources.LoadAsync(job.Path);

        while (request.isDone == false)
            yield return null;

        job.Evt.Invoke(request.asset as GameObject);
    }
    
    
    private void Start()
    {
        ResourceLoad("Prefabs/Cube", (obj) => _testObject = Instantiate(obj)); //  
        
        print("1");
        
        print(_testObject.name);
    }
    
    
    private void Update()
    {
        if (resourceQ != null && resourceQ.Count > 0)
        {
            // todo : CheckLoad 코루틴이 돌고 있는지 체크
            
            var job = resourceQ.Dequeue();
            StartCoroutine(CheckLoad(job));
        }
    }
}