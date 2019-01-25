using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sjabloon
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}
