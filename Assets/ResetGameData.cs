using UnityEngine;

namespace Root
{
    public class ResetGameData : MonoBehaviour
    {
        [SerializeField] private ConfirmationPopup confirmationPopup;
        [SerializeField] private string translationKey;
        [SerializeField] private bool requireConfirmation = true;

        
        public void Reset()
        {
            if (requireConfirmation && confirmationPopup != null)
            {
                confirmationPopup.ShowPopup(translationKey, RealReset);
            }
            else
            {
                RealReset();
            }
        }

        private void RealReset()
        {
            Database.Database.Ins.ResetData();
        }
    }
}