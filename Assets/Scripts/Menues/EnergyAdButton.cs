using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Root
{
    public class EnergyAdButton : MonoBehaviour {
        [SerializeField] private int completeStaminaReward;
        [SerializeField] private int partialStaminaReward;
        public void LoadStaminaAdd() {
            AdsManager.Instance.SubscribeToRewardedAdResult(OnAdCompleted);
            AdsManager.Instance.ShowRewardedAd();
        }

        private void OnAdCompleted(UnityAdsShowCompletionState completionState)
        {
            StaminaSystem stamina = Database.Database.Ins.staminaSystem;
            switch (completionState) {
                case UnityAdsShowCompletionState.COMPLETED:
                    stamina.AddStamina(completeStaminaReward);
                    return;
                case UnityAdsShowCompletionState.SKIPPED:
                    stamina.AddStamina(partialStaminaReward);
                    return;
                case UnityAdsShowCompletionState.UNKNOWN:
                    Debug.LogWarning("Add couldn't be processed");
                    return;
            }
            AdsManager.Instance.UnsubscribeToRewardedAdResult(OnAdCompleted);
        }
    }
}
