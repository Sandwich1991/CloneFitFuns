using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T LoadAsync<T>(string path, Action<Object> evt) where T : Object
    {
        var obj = Resources.LoadAsync(path);
        
        if (typeof(T) == typeof(GameObject))
            evt.Invoke(obj.asset as GameObject);
        
        return obj.asset as T;
    }
    
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        
        return go;

        // LoadAsync(path, (obj) => MonoHelper.Instantiate(obj, parent));
    }

    public void Destroy(GameObject go)
    {
        Object.Destroy(go);
    }
}


