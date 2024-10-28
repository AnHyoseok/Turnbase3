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


        public List<CharacterStats> characters; // 턴을 진행할 캐릭터들
        private int currentTurn = 0; // 현재 턴 인덱스

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

        // 턴 시작
        public void StartTurn()
        {
            UpdateTurnUI();
            // 현재 턴 캐릭터의 행동 가능 상태로 전환
            characters[currentTurn].EnableActions();
        }

        // 턴 종료
        public void EndTurn()
        {
            characters[currentTurn].DisableActions(); // 현재 캐릭터 행동 비활성화
           
            NextTurn();
           
        }

        // 다음 턴으로 전환
        public void NextTurn()
        {
            // 현재 턴이 마지막 턴인지 확인
            if (currentTurn == characters.Count - 1)
            {
                battleTurnCount++; // 전투 턴 수 증가
                Debug.Log("전투 턴 수: " + battleTurnCount);
            }

            currentTurn = (currentTurn + 1) % characters.Count; // 순환
            
            StartTurn();
        }

        // 턴 UI 업데이트
        private void UpdateTurnUI()
        {
            // UI 업데이트 코드 작성
            Debug.Log("Current Turn: " + characters[currentTurn].name);
        }

        // 캐릭터의 행동을 활성화
        public void EnableCharacterActions(int index)
        {
            if (index >= 0 && index < characters.Count)
            {
                characters[index].EnableActions();
            }
        }

        // 게임 오버 확인
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
                // 게임 오버 처리
                Debug.Log("Game Over!");
            }
        }
    }


}
