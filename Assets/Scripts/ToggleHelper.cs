using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Personal.Utilities
{
    public class ToggleHelper : MonoBehaviour
    {
        public ToggleController toggleController;
        public GameObject myPanel;
        [SerializeField]
        private Image highlight;
        public int index = 0;
        public static UnityEvent<int> unityEvent = new UnityEvent<int>();

        public void Select()
        {
            toggleController.DeselectAll();

            if(highlight != null)
                highlight.enabled = true;
            myPanel.SetActive(true);

            if (unityEvent != null)
            {
                unityEvent.Invoke(index);
            }
        }

        public void Deselect()
        {
            myPanel.SetActive(false);
            if(highlight != null)
                highlight.enabled = false;
        }
    }
}
