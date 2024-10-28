using System.Collections;
using UnityEngine;

namespace MyTurnBase
{
    public class FollowPath : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private Transform target;                   // ���� �̵��ϴ� ����� Transform
        [SerializeField]
        private Transform[] wayPoints;              // �̵� ������ ����
        [SerializeField]
        private float waitTime;             // wayPoint ���� �� ���ð�
        [SerializeField]
        private float unitPerSecond = 1;        // 1�ʿ� �����̴� �Ÿ�
        [SerializeField]
        private bool isPlayOnAwake = true;  // �ڵ� ���� ����
        [SerializeField]
        private bool isLoop = true;         // ������ ��ο��� ó�� ��η� �̾�������

        private int wayPointCount;          // �̵� ������ wayPoint ����
        private int currentIndex = 0;       // ���� wayPoint �ε���

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
                // wayPoints[currentIndex].position ��ġ���� �̵�
                yield return StartCoroutine(MoveAToB(target.position, wayPoints[currentIndex].position));

                // ���� �̵� ����(wayPoint) ����
                if (currentIndex < wayPointCount - 1)
                {
                    currentIndex++;
                }
                else
                {
                    if (isLoop == true) currentIndex = 0;
                    else break;
                }

                // waitTime �ð� ���� ���
                yield return wait;
            }

            Debug.Log("��� ����� Ž���� �Ϸ��߽��ϴ�.");
        }

        private IEnumerator MoveAToB(Vector3 start, Vector3 end)
        {
            float percent = 0;
            // �Ÿ��� ���� ���� 3�� ���� �̵�
            //float moveTime = 3;
            // �̵� �ð� = �� �̵� �Ÿ� / �ʴ� �̵� �Ÿ�
            float moveTime = Vector3.Distance(start, end) / unitPerSecond;

            Debug.Log($"�̵��Ÿ� : {Vector3.Distance(start, end)}, �̵��ð� : {moveTime}");

            while (percent < 1)
            {
                percent += Time.deltaTime / moveTime;

                target.position = Vector3.Lerp(start, end, percent);

                yield return null;
            }
        }
    }
}
