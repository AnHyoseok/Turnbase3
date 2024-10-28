using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyTurnBase
{

    public class ParallaxEffect : MonoBehaviour
    {
        #region Variables
        public new Camera  camera;            // ī�޶�
        public Transform followTarget;   // �÷��̾�

        // ���� ����� ó���ϱ� ���� ����Ʈ
        public List<Transform> backgrounds;

        // �� ����� ���� ��ġ�� Z ��ǥ ����
        private Vector2[] startingPositions;
        private float[] startingZ;

        // ī�޶�� ���������� �Ÿ�
        Vector2 CamMoveSinceStart => (Vector2)camera.transform.position;
        #endregion

        void Start()
        {
            // �� ����� ���� ��ġ�� Z ��ǥ �ʱ�ȭ
            startingPositions = new Vector2[backgrounds.Count];
            startingZ = new float[backgrounds.Count];

            for (int i = 0; i < backgrounds.Count; i++)
            {
                startingPositions[i] = backgrounds[i].position;
                startingZ[i] = backgrounds[i].localPosition.z;
            }
        }

        void Update()
        {
            for (int i = 0; i < backgrounds.Count; i++)
            {
                float zDistanceFromTarget = backgrounds[i].position.z - followTarget.position.z;

                // Ŭ���� ��� ���
                float clippingPlane = camera.transform.position.z + (zDistanceFromTarget > 0 ? camera.farClipPlane : camera.nearClipPlane);

                // Parallax Factor ���
                float parallaxFactor = Mathf.Abs(zDistanceFromTarget) / clippingPlane;

                // ���ο� ��ġ ���
                Vector2 newPosition = startingPositions[i] + CamMoveSinceStart * parallaxFactor;

                // ����� ��ġ ������Ʈ
                backgrounds[i].position = new Vector3(newPosition.x, newPosition.y, startingZ[i]);
            }
        }
    }

}
