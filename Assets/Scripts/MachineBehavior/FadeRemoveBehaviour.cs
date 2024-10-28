using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTurnBase
{
    //���� ��������Ʈ ������Ʈ�� ���̵� �ƿ� �� ų
    public class FadeRemoveBehaviour : StateMachineBehaviour
    {
        #region Variables

        private SpriteRenderer spriteRenderer;
        private GameObject removeObject;
        private Color StartColor;

        //fadeȿ��
        public float fadeTimer = 1f;
        private float countdown = 0f;

        //������ �ð��Ŀ� fade ȿ�� ó��
        public float delayTime = 2f;
        private float delayCountdown = 0f;
        #endregion
        //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //����
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            StartColor = spriteRenderer.color;
            removeObject = animator.gameObject;
            //�ʱ�ȭ
            countdown = fadeTimer;
        }

        //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //������Ÿ�� ��ŭ ������ �ɱ� 
            if (delayCountdown < delayTime)
            {
                delayCountdown += Time.deltaTime;
                return;
            }

            //���̵� ��  0 ->1
            // countdown += Time.deltaTime;
            //float newAlpha = StartColor.a * (1-countdown / fadeTimer);
            //���̵� �ƿ� ȿ�� color.a : 1->0
            countdown -= Time.deltaTime;

            float newAlpha = StartColor.a * (countdown / fadeTimer);
            spriteRenderer.color = new Color(StartColor.r, StartColor.g, StartColor.b, newAlpha);

            //���̵� Ÿ�� ��
            if (countdown <= 0f)
            {
                Destroy(removeObject);
            }

        }



    }

}