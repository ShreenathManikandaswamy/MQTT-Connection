using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RotateMachine : MonoBehaviour , IPointerMoveHandler
{
    [SerializeField]
    GameObject _Machine = null;
    [SerializeField]
    private float rotSpeed = 2000;

    public void OnPointerMove(PointerEventData eventData)
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        //float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        _Machine.transform.Rotate(Vector3.up, -rotX);
        //_Machine.transform.Rotate(Vector3.right, -rotY);
    }
}
