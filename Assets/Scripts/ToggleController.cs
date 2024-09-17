using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal.Utilities
{
    public class ToggleController : MonoBehaviour
    {
        public List<ToggleHelper> toggleHelpers;
        [SerializeField]
        private bool canStart = true;

        private void Start()
        {
            if (canStart)
                Init();
        }

        public void Init()
        {
            toggleHelpers[0].Select();
        }

        public void DeselectAll()
        {
            foreach(ToggleHelper helper in toggleHelpers)
            {
                helper.Deselect();
            }
        }
    }
}
