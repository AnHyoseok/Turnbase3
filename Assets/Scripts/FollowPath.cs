using System.Collections;
using UnityEngine;

namespace MyTurnBase
{
    public class FollowPath : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform target;                   // 실제 이동하는 대상의 Transform
        [SerializeField]
        private Transform[] wayPoints;              // 이동 가능한 지점
        [SerializeField]
        private float waitTime;             // wayPoint 도착 후 대기시간
        [SerializeField]
        private float unitPerSecond = 1;        // 1초에 움직이는 거리
        [SerializeField]
        private bool isPlayOnAwake = true;  // 자동 시작 여부
        [SerializeField]
        private bool isLoop = true;         // 마지막 경로에서 처음 경로로 이어지는지

        private int wayPointCount;          // 이동 가능한 wayPoint 개수
        private int currentIndex = 0;       // 현재 wayPoint 인덱스

        #endregion
        private void Awake()
        {
            wayPointCount = wayPoints.Length;

            if (target == null) target = transform;
            if (isPlayOnAwake == true) Play();
        }

        public void Play() => StartCoroutine(nameof(Process));

        private IEnumerator Process()
        {
            var wait = new WaitForSeconds(waitTime);

            while (true)
            {
                // wayPoints[currentIndex].position 위치까지 이동
                yield return StartCoroutine(MoveAToB(target.position, wayPoints[currentIndex].position));

                // 다음 이동 지점(wayPoint) 설정
                if (currentIndex < wayPointCount - 1)
                {
                    currentIndex++;
                }
                else
                {
                    if (isLoop == true) currentIndex = 0;
                    else break;
                }

                // waitTime 시간 동안 대기
                yield return wait;
            }

            Debug.Log("모든 경로의 탐색을 완료했습니다.");
        }

        private IEnumerator MoveAToB(Vector3 start, Vector3 end)
        {
            float percent = 0;
            // 거리에 관계 없이 3초 동안 이동
            //float moveTime = 3;
            // 이동 시간 = 총 이동 거리 / 초당 이동 거리
            float moveTime = Vector3.Distance(start, end) / unitPerSecond;

            Debug.Log($"이동거리 : {Vector3.Distance(start, end)}, 이동시간 : {moveTime}");

            while (percent < 1)
            {
                percent += Time.deltaTime / moveTime;

                target.position = Vector3.Lerp(start, end, percent);

                yield return null;
            }
        }
    }
}
