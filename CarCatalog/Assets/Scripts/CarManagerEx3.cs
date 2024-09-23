using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarManagerEx3 : CarManagerEx2
{
    protected override void Update()
    {
        DetectGround();

        //  현재 클릭( 터치 )한 오브젝트가
        //  [ UI 오브젝트 ]라면
        //  [ Update 함수 ] 종료..
        if (EventSystem.current.currentSelectedGameObject)
            return;

        base.Update();

    }
}
