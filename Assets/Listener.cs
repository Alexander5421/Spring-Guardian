using UnityEditorInternal;
using UnityEngine;

namespace DefaultNamespace
{
    public class Listener : MonoBehaviour
    {
        public EventMaker eventMaker;
        // start is called before the first frame update
        void Start()
        {
            eventMaker.OnEvent += foo;
        }
        
        void foo()
        {
            Debug.Log("foo");            
            Destroy(gameObject);

        }
        // Context menu
        [ContextMenu("KillSelf")]
        void suicide()
        {
            Destroy(gameObject);
        }
    }
}