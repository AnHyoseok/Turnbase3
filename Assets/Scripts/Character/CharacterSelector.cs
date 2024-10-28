using System.Collections.Generic;
using UnityEngine;

namespace MyTurnBase
{
    public class CharacterSelector : MonoBehaviour
    {

        #region Variables
        public List<GameObject> characters; // 캐릭터 오브젝트 리스트
        private int selectedIndex = -1; // 선택된 캐릭터 인덱스
        private List<GameObject> outlines = new List<GameObject>(); // 외곽선 오브젝트 리스트
        public TurnManager turnManager; // TurnManager에 대한 참조
        #endregion

        private void Start()
        {
            // 각 캐릭터의 초기 위치를 에디터에서 설정한 위치로 사용
            for (int i = 0; i < characters.Count; i++)
            {
                // 캐릭터의 현재 위치를 가져와서 설정
                Vector2 initialPosition = characters[i].transform.position;

                // 외곽선용 오브젝트 생성
                GameObject outline = new GameObject("Outline");
                outline.transform.SetParent(characters[i].transform);
                outline.transform.localPosition = Vector2.zero;

                // SpriteRenderer 추가하고 스프라이트 및 색상 설정
                SpriteRenderer outlineSprite = outline.AddComponent<SpriteRenderer>();
                outlineSprite.sprite = characters[i].GetComponent<SpriteRenderer>().sprite;
                outlineSprite.color = Color.yellow;
                outlineSprite.sortingOrder = characters[i].GetComponent<SpriteRenderer>().sortingOrder - 1;

                // 외곽선 크기 조정
                outline.transform.localScale = Vector3.one * 1.2f;

                // 초기 비활성화
                outline.SetActive(false);

                outlines.Add(outline);

                // 캐릭터 위치 설정 (에디터에서 설정한 위치)
                characters[i].transform.position = initialPosition;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hit.collider != null)
                {
                    GameObject clickedCharacter = hit.collider.gameObject;
                    int index = characters.IndexOf(clickedCharacter);

                    if (index != -1)
                    {
                        OnCharacterSelected(index);
                    }
                }
            }
        }

        // 캐릭터 선택 함수
        public void OnCharacterSelected(int index)
        {
            selectedIndex = index;
            HighlightSelectedCharacter();

            // 선택된 캐릭터의 행동 활성화
            if (turnManager != null)
            {
                turnManager.EnableCharacterActions(index);
            }
        }

        // 선택된 캐릭터 강조 표시 (외곽선 추가)
        private void HighlightSelectedCharacter()
        {
            for (int i = 0; i < outlines.Count; i++)
            {
                outlines[i].SetActive(i == selectedIndex);
            }
        }
    }
}

