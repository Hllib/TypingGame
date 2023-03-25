using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _animator.SetTrigger("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _animator.SetTrigger("Exit");
    }
}
