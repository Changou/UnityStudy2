using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("[배경 이미지]"), SerializeField]
    Image _backImage;

    [Header("[스틱 이미지]"), SerializeField]
    Image _stickImage;

    Vector3 _input;
    public float _Horizon => _input.x;
    public float _Vert => _input.y;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        RectTransform rtTrans = _backImage.rectTransform;

        Camera pressEventCam = eventData.pressEventCamera;

        Vector3 dataPos = eventData.position;
        //Debug.Log("dataPos : " + dataPos);

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (rtTrans, dataPos, pressEventCam, out Vector2 curPos))
        {
            //Debug.Log("befor curPos : " + curPos);

            curPos.x = curPos.x / _backImage.rectTransform.sizeDelta.x * 2;
            curPos.y = curPos.y / _backImage.rectTransform.sizeDelta.y * 2;
            //Debug.Log("_backImage.rectTransform.sizeDalta : " + _backImage.rectTransform.sizeDelta);
            //Debug.Log("after curPos : " + curPos);

            _input = new Vector3(curPos.x, curPos.y, 0f);
            _input = (_input.magnitude > 1) ? _input.normalized : _input;

            float x = _input.x * rtTrans.sizeDelta.x * 0.4f;
            float y = _input.y * rtTrans.sizeDelta.y * 0.4f;

            _stickImage.rectTransform.anchoredPosition = new Vector3(x, y, 0f);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _input = Vector3.zero;
        _stickImage.rectTransform.anchoredPosition = _input;
    }
}
