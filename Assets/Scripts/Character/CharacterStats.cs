using UnityEngine;
namespace MyTurnBase
{
    public class CharacterStats : MonoBehaviour
    {
        #region Variables
        [SerializeField] public int health; // 캐릭터의 체력
        [SerializeField] public int attackPower; // 공격력
        [SerializeField] public int speed;      // 캐릭터의 스피드 (턴의 영향을 줌)
        [SerializeField] public bool isAlive = true; // 생존 여부

        #endregion


        // 캐릭터 초기화
        public void Initialize(int health, int attackPower)
        {
            this.health = health;
            this.attackPower = attackPower;
            isAlive = true;
        }

        // 행동 가능 상태 활성화
        public void EnableActions()
        {
            // 행동 가능 상태로 전환
            // 예를 들어 UI를 활성화하거나, 입력을 받을 수 있도록 설정
            Debug.Log(gameObject.name + "의 행동이 활성화되었습니다.");
        }

        // 행동 가능 상태 비활성화
        public void DisableActions()
        {
            // 행동 불가능 상태로 전환
            // 예를 들어 UI를 비활성화하거나, 입력을 받을 수 없도록 설정
            Debug.Log(gameObject.name + "의 행동이 비활성화되었습니다.");
        }

        // 생존 여부 확인
        public bool IsAlive()
        {
            return isAlive;
        }

        // 데미지 받기
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isAlive = false; // 캐릭터 사망 처리
                Debug.Log(gameObject.name + "이(가) 사망하였습니다.");
            }
        }
    }
}
