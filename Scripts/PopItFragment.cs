using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SphereCollider))]
public class PopItFragment : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal int index;
    internal Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameController.GetMove)
        {
            GameController.RemoveFragment(index, GameController.GetPlayerColor);
            GameController.ChangePlayerScore();
        }
    }
}
