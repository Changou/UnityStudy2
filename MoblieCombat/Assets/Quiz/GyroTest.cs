//========================================================
/* 
    --------------------------------
    -   자이로 센서..( 자이로 스코프 )
    --------------------------------


        ---------------
        -   gyroscope..
        ---------------

            -   운동하는 물체의 각도를
                계측하는 센서..
                [ 참고 : Spatial_Intro.jpg ]



        ----------------
        -   Input.gyro의
            주요 속성..
        ----------------

            ------------
            -   enable..
            ------------

                -   자이로스코프 기능을
                    On/Off..

                -   기본값 : off..



            --------------
            -   attitude..
            --------------

                -   디바이스의 회전값..

                -   Quaternion 타입..



            ------------------
            -   rotationRate..
            ------------------

                -   기울기 검출값..

                -   디바이스의 회전 속도를
                    반환..

                -   Vector3 타입..



            --------------------------
            -   rotationRateUnbiased..
            --------------------------

                -   기울기 검출값
                    + 필터링 입력..

                -   편향되지 않은 회전속도를
                    반환..

                    -   표면상 차이는 없음..

                -   Vector3 타입..


            
        -------------------------------------
        -   자이로 센서( Gyroscope )
            vs 가속도 센서( Accelerometer )..
        -------------------------------------

            -   가속도 센서..
                -   직선 방향의
                    움직임을 감지..


            -   자이로 센서..
                -   회전 방향의
                    움직임을 감지..

 */
//========================================================
using UnityEngine;
using UnityEngine.UI;
//========================================================
public class GyroTest : MonoBehaviour
{
    //---------------------------
    //  카메라의 부모..
    GameObject _camParent;
    //---------------------------
    public Toggle _unBiased;
    //---------------------------
    void Start()
    {
        _camParent = new GameObject("CamParent");
        _camParent.transform.position = transform.position;
        transform.parent = _camParent.transform;
        //  자이로 스코프 기능 On..
        Input.gyro.enabled = true;
        
    }// void Start()
    //---------------------------
    void Update()
    {
        Vector3 gyroRot = _unBiased.isOn ?
            Input.gyro.rotationRateUnbiased :
            Input.gyro.rotationRate;

        _camParent.transform.Rotate(0, -gyroRot.y, 0);
        transform.Rotate(-gyroRot.x, 0, 0);
        Debug.Log("Input.gyro.rotationRateUnbiased : " + Input.gyro.rotationRateUnbiased);
        Debug.Log("Input.gyro.rotationRate : " + Input.gyro.rotationRate);

    }// void Update()
    //---------------------------

}// public class GyroTest : MonoBehaviour
 //========================================================
/*
    [ 참고 ]

    [안드로이드] 자이로스코프 이야기. 가속도센서 vs 자이로센서 비교
    https://techlog.gurucat.net/104

    Gyroscope Vs Accelerometer
    https://answers.unity.com/questions/1173889/gyroscore-vs-accelerometer.html

    Tip : Unity 자이로 & 가속도 센서 관련
    https://blog.naver.com/slee16/221706003164

    unity Gyroscope camera
    https://m.blog.naver.com/rlawndks4204/221346611798
    
*/
//========================================================
/*
    [ Quiz 1 ]

        [ 참고 ]
            https://github.com/RaviSingla23/SpaceFrenzy
            https://ko.y8.com/games/xracer

        위 링크를 참고하여 비행기 슈팅 게임을 만듭니다.

        플레이어는 가속도 센서와 자이로 스코프를
        이용하여 비행기를 컨트롤 합니다.

        -   자이로 스코프..
            -   좌우 이동..

        -   가속도 센서
            -   상하 이동..   


	    -	목표..

		    -	제한 시간안에 목적지에 도착..



	    -	게임의 상태는
		    준비, 진행, 종료로 구분합니다..

		    -	[ 준비 ]

			    -	UI나 이펙트로
				    준비하라는 메시지를 연출..

			    -	비행기가 천천히 가속..
				    -	비행기의 속도가
					    일정속도에 이르면
					    [ 진행 ]상태가 됨..


		    -	[ 진행 ]

                -   플레이어..

                    -   상하 좌우 이동..

                        -   화면을 벗어나지 않도록
                            이동 범위 제한..

                    -   체력 : 1

                    -   속도 : 가급적 빠르게..



			    -	장애물..
			
				    -	랜덤으로 장애물이 등장..
			
				    -	플레이어는 장애물에 충돌하면
					    체력이 감소하고 
                        체력이 모두 소진되면 게임이 종료..
					    -	장애물에 충돌하면
						    카메라 셰이킹..

				    -	레벨이 증가할수록
					    다양한 장애물이 등장..

				    -	종류..
					    -	지형 장애물..
					    -	이동 장애물..


			    -	아이템..

				    -	3초간 속도 20% 증가..

				    -	3초간 무적..

				    -	무기 발사 + 1회..
					    -	초기 값은 0회..
					    -	1회 이상 가능하면
						    버튼 활성화..
					    -	누적 가능..					

				    -	코인..
					    -	레벨 목표 달성후
						    업그레이드할때 사용..
					    -	+ 10원..
                    
                    -   HP 증가..
                        -   아이템 획득시
                            기존 HP + 1..
                        -   누적 가능..

               


		    -	[ 종료 ]

			    -	승..

				    -	각 레벨의 목표거리 달성..

				    -	다음 레벨로 이동하기전에
					    업그레이드 가능..
					    -	HP + 1
					    -	속도 5% 증가..
					    -	무기 발사 횟수 + 1회..

			    -	패..
				    -	장애물에 충돌하여
					    hp 모두 소모..

				    -	제한 시간내에 목적지
                        도착 실패..

        [ Quiz 2 ]

            간단한 FPS 게임을 만듭니다..
            
            -   컨트롤..

                -   전후 이동
                    -   버튼..

                -   상하 좌우 둘러보기
                    -   자이로 센서..

                -   점프/숙이기..
                    -   가속 센서..

                -   총 / 수류탄..
                    -   버튼..

            -   규칙..

                -   이동하는 중간에 적들이 나타나서
                    플레이어를 공격..
                
                -   제한된 시간내에 목표 지점까지
                    도착하면 승리..

                -   제한 시간이 초과되거나
                    HP가 모두 소모되면 패배..

            

*/
//========================================================