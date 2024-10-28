using UnityEngine;
namespace MyTurnBase
{
    public class CharacterStats : MonoBehaviour
    {
        #region Variables
        [SerializeField] public int health; // ĳ������ ü��
        [SerializeField] public int attackPower; // ���ݷ�
        [SerializeField] public int speed;      // ĳ������ ���ǵ� (���� ������ ��)
        [SerializeField] public bool isAlive = true; // ���� ����

        #endregion


        // ĳ���� �ʱ�ȭ
        public void Initialize(int health, int attackPower)
        {
            this.health = health;
            this.attackPower = attackPower;
            isAlive = true;
        }

        // �ൿ ���� ���� Ȱ��ȭ
        public void EnableActions()
        {
            // �ൿ ���� ���·� ��ȯ
            // ���� ��� UI�� Ȱ��ȭ�ϰų�, �Է��� ���� �� �ֵ��� ����
            Debug.Log(gameObject.name + "�� �ൿ�� Ȱ��ȭ�Ǿ����ϴ�.");
        }

        // �ൿ ���� ���� ��Ȱ��ȭ
        public void DisableActions()
        {
            // �ൿ �Ұ��� ���·� ��ȯ
            // ���� ��� UI�� ��Ȱ��ȭ�ϰų�, �Է��� ���� �� ������ ����
            Debug.Log(gameObject.name + "�� �ൿ�� ��Ȱ��ȭ�Ǿ����ϴ�.");
        }

        // ���� ���� Ȯ��
        public bool IsAlive()
        {
            return isAlive;
        }

        // ������ �ޱ�
        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isAlive = false; // ĳ���� ��� ó��
                Debug.Log(gameObject.name + "��(��) ����Ͽ����ϴ�.");
            }
        }
    }
}
