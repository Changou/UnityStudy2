using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarManagerEx3 : CarManagerEx2
{
    protected override void Update()
    {
        DetectGround();

        //  ���� Ŭ��( ��ġ )�� ������Ʈ��
        //  [ UI ������Ʈ ]���
        //  [ Update �Լ� ] ����..
        if (EventSystem.current.currentSelectedGameObject)
            return;

        base.Update();

    }
}
