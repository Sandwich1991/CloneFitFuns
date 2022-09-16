using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHelper : MonoBehaviour
{
    public static void Instantiate(Object obj, Transform parent = null)
    {
        Instantiate(obj, parent);
    }
}
