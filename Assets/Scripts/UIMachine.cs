using Personal.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMachine : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _Highlights = null;

    // Start is called before the first frame update
    void Start()
    {
        ToggleHelper.unityEvent.AddListener(OnToggleSelected);
    }

    void OnToggleSelected(int index_)
    {
        for (int i = 0; i < _Highlights.Count; i++)
        {
            if (i == index_)
            {
                _Highlights[i].SetActive(true);
            }
            else
            {
                _Highlights[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
