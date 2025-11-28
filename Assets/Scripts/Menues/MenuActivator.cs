using UnityEngine;

namespace Root
{
    public class ActivateOnPointerClick : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;

        
        public void ActivateTargetPublic()
        {
            if (targetObject != null)
                targetObject.SetActive(true);
        }
    }
}
