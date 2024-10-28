
using UnityEngine;
namespace MyTurnBase
{
	public class Title : MonoBehaviour
	{
        #region Variables
        public SceneFader fader;
        #endregion
        private void Start()
        {
            fader.FromFade();
        }
    }
}
