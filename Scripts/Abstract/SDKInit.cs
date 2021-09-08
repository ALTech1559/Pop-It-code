using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

namespace Abstract
{
    public class SDKInit : GenericSingleton<SDKInit>
    {
        public override void Awake()
        {
            base.Awake();

            Debug.Log("init sdk");
            
            if (FB.IsInitialized)
                FB.ActivateApp();

            else
                FB.Init(() => { FB.ActivateApp(); });
        }

        private void Start()
        {
            Debug.Log("start sdk");
            
            GameAnalytics.Initialize();
        }
    }
}