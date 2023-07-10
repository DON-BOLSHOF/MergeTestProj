using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _offset;
    
    public ReactiveCommand OnBeginDragCommand = new ReactiveCommand();
    public ReactiveCommand<Vector3> OnDragCommand = new ReactiveCommand<Vector3>();
    public ReactiveCommand OnEndDragCommand = new ReactiveCommand();


    public void OnBeginDrag(PointerEventData eventData)
    {
        _offset = transform.position - GetMouseWorldPosition();

        OnBeginDragCommand?.Execute();
    }

    private Vector3 GetMouseWorldPosition()
    {
        var mouseScreenPose = Input.mousePosition;
        mouseScreenPose.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(mouseScreenPose);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var mouseWorldPosition = GetMouseWorldPosition() + _offset;
        
        transform.position = mouseWorldPosition;
        OnDragCommand?.Execute(mouseWorldPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragCommand?.Execute();
    }
}
