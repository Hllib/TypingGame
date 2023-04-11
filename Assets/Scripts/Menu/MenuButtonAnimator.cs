using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Enter");
        }
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.buttonHover, Vector3.zero);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_animator != null)
        {
            _animator.SetTrigger("Exit");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.buttonClick, Vector3.zero);
    }
}
