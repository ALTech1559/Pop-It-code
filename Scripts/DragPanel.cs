using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Cube Cube;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Cube.OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Cube.OnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Cube.OnEndDrag();
    }
}
