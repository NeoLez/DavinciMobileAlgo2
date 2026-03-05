using UnityEngine;
using UnityEngine.Advertisements;

namespace Root
{
    public class EnergyAdButton : MonoBehaviour
    {
        [SerializeField] private int completeStaminaReward;
        [SerializeField] private int partialStaminaReward;

        [SerializeField] private ConfirmationPopup confirmationPopup;
        [SerializeField] private string translationKey;
        [SerializeField] private bool requireConfirmation = true;

        public void LoadStaminaAdd()
        {
            if (requireConfirmation && confirmationPopup != null)
            {
                confirmationPopup.ShowPopup(translationKey, RealLoadStaminaAdd);
            }
            else
            {
                RealLoadStaminaAdd();
            }
        }

        private void RealLoadStaminaAdd()
        {
            AdsManager.Instance.SubscribeToRewardedAdResult(OnAdCompleted);
            AdsManager.Instance.ShowRewardedAd();
        }

        private void OnAdCompleted(UnityAdsShowCompletionState completionState)
        {
            StaminaSystem stamina = Database.Database.Ins.staminaSystem;
            switch (completionState)
            {
                case UnityAdsShowCompletionState.COMPLETED:
                    stamina.AddStamina(completeStaminaReward);
                    break; // Cambie return por break para que llegue al unsubscribe
                case UnityAdsShowCompletionState.SKIPPED:
                    stamina.AddStamina(partialStaminaReward);
                    break;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogWarning("Add couldn't be processed");
                    break;
            }
            AdsManager.Instance.UnsubscribeToRewardedAdResult(OnAdCompleted);
        }
    }
}