using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTurnBase
{
    public class LookAtMouse : MonoBehaviour
    {
        #region Variables
        [SerializeField] private Vector3 worldPosition; //마우스가 가르키는 월드 공간값
        #endregion

        private void Update()
        {
            worldPosition = ScreenToWorld();
            transform.LookAt(worldPosition);
        }

        Vector3 ScreenToWorld()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            return worldPos;
        }

        Vector3 RayToWorld()
        {
            Vector3 worldPos = Vector3.zero;
            Vector3 mousePos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                worldPos = hit.point;
            }
            return worldPos;
        }
    }
}