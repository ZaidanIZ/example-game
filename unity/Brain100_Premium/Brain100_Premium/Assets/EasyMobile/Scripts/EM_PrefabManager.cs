using UnityEngine;
using System.Collections;

namespace EasyMobile
{
    public class EM_PrefabManager : MonoBehaviour
    {
        public static EM_PrefabManager Instance { get; private set; }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);

                // Debug logging setting: only enable debug log in editor or development builds.
                #if DEBUG || DEVELOPMENT_BUILD
                Debug.unityLogger.logEnabled = true;
                #else
                Debug.unityLogger.logEnabled = false;
                #endif
            }
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}

