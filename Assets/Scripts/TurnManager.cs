using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace MyTurnBase
{
    public class TurnManager : MonoBehaviour
    {
        #region Variables
        [SerializeField] public TextMeshProUGUI nowTurnText;
        [SerializeField] public TextMeshProUGUI battleTurnText;
        private int battleTurnCount;
        #endregion


        public List<CharacterStats> characters; // ���� ������ ĳ���͵�
        private int currentTurn = 0; // ���� �� �ε���

        private void Start()
        {
            battleTurnCount = 0;

            StartTurn();
        }

        private void Update()
        {
            nowTurnText.text = currentTurn.ToString();
            battleTurnText.text = battleTurnCount.ToString();
        }

        // �� ����
        public void StartTurn()
        {
            UpdateTurnUI();
            // ���� �� ĳ������ �ൿ ���� ���·� ��ȯ
            characters[currentTurn].EnableActions();
        }

        // �� ����
        public void EndTurn()
        {
            characters[currentTurn].DisableActions(); // ���� ĳ���� �ൿ ��Ȱ��ȭ
           
            NextTurn();
           
        }

        // ���� ������ ��ȯ
        public void NextTurn()
        {
            // ���� ���� ������ ������ Ȯ��
            if (currentTurn == characters.Count - 1)
            {
                battleTurnCount++; // ���� �� �� ����
                Debug.Log("���� �� ��: " + battleTurnCount);
            }

            currentTurn = (currentTurn + 1) % characters.Count; // ��ȯ
            
            StartTurn();
        }

        // �� UI ������Ʈ
        private void UpdateTurnUI()
        {
            // UI ������Ʈ �ڵ� �ۼ�
            Debug.Log("Current Turn: " + characters[currentTurn].name);
        }

        // ĳ������ �ൿ�� Ȱ��ȭ
        public void EnableCharacterActions(int index)
        {
            if (index >= 0 && index < characters.Count)
            {
                characters[index].EnableActions();
            }
        }

        // ���� ���� Ȯ��
        private void CheckGameOver()
        {
            bool allDefeated = true;
            foreach (var character in characters)
            {
                if (character.IsAlive())
                {
                    allDefeated = false;
                    break;
                }
            }

            if (allDefeated)
            {
                // ���� ���� ó��
                Debug.Log("Game Over!");
            }
        }
    }


}
