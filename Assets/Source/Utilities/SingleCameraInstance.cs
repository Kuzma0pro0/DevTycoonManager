using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tycoon
{
    public class SingleCameraInstance : MonoBehaviour
    {
        private static SingleCameraInstance _instance = null;

        public bool SaveLatest;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                if (SaveLatest)
                {
                    Destroy(_instance.gameObject);
                    _instance = this;
                    DontDestroyOnLoad(_instance);
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
