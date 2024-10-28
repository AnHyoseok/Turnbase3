using System.Collections.Generic;
using UnityEngine;

namespace MyTurnBase
{
    public class CharacterSelector : MonoBehaviour
    {

        #region Variables
        public List<GameObject> characters; // ĳ���� ������Ʈ ����Ʈ
        private int selectedIndex = -1; // ���õ� ĳ���� �ε���
        private List<GameObject> outlines = new List<GameObject>(); // �ܰ��� ������Ʈ ����Ʈ
        public TurnManager turnManager; // TurnManager�� ���� ����
        #endregion

        private void Start()
        {
            // �� ĳ������ �ʱ� ��ġ�� �����Ϳ��� ������ ��ġ�� ���
            for (int i = 0; i < characters.Count; i++)
            {
                // ĳ������ ���� ��ġ�� �����ͼ� ����
                Vector2 initialPosition = characters[i].transform.position;

                // �ܰ����� ������Ʈ ����
                GameObject outline = new GameObject("Outline");
                outline.transform.SetParent(characters[i].transform);
                outline.transform.localPosition = Vector2.zero;

                // SpriteRenderer �߰��ϰ� ��������Ʈ �� ���� ����
                SpriteRenderer outlineSprite = outline.AddComponent<SpriteRenderer>();
                outlineSprite.sprite = characters[i].GetComponent<SpriteRenderer>().sprite;
                outlineSprite.color = Color.yellow;
                outlineSprite.sortingOrder = characters[i].GetComponent<SpriteRenderer>().sortingOrder - 1;

                // �ܰ��� ũ�� ����
                outline.transform.localScale = Vector3.one * 1.2f;

                // �ʱ� ��Ȱ��ȭ
                outline.SetActive(false);

                outlines.Add(outline);

                // ĳ���� ��ġ ���� (�����Ϳ��� ������ ��ġ)
                characters[i].transform.position = initialPosition;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
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

        // ĳ���� ���� �Լ�
        public void OnCharacterSelected(int index)
        {
            selectedIndex = index;
            HighlightSelectedCharacter();

            // ���õ� ĳ������ �ൿ Ȱ��ȭ
            if (turnManager != null)
            {
                turnManager.EnableCharacterActions(index);
            }
        }

        // ���õ� ĳ���� ���� ǥ�� (�ܰ��� �߰�)
        private void HighlightSelectedCharacter()
        {
            for (int i = 0; i < outlines.Count; i++)
            {
                outlines[i].SetActive(i == selectedIndex);
            }
        }
    }
}

