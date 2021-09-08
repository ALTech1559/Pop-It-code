using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelController : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        print("True");
    }
}
