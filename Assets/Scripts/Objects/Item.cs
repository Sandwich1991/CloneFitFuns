using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private string parent;

    private void Awake()
    {
        parent = transform.parent.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Managers.Player.PlayerTransform)
        {
            Managers.Resource.Destroy(gameObject);
            Managers.Player.Trash++;
            
            switch (parent)
            {
                case "Azone":
                    PlayZone.ACurItems--;
                    break;
                
                case "Bzone":
                    PlayZone.BCurItems--;
                    break;
                
                case "Czone":
                    PlayZone.CCurItems--;
                    break;
            }
            
        }
    }
}
