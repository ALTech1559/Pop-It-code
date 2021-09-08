using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsController : MonoBehaviour
{
    [SerializeField] private List<GameObject> icons;

    internal void Activate(int index)
    {
        icons[index - 1].SetActive(true);
    }

    internal void Disactivate()
    {
        foreach (GameObject icon in icons)
        {
            icon.SetActive(false);
        }
    }
}
