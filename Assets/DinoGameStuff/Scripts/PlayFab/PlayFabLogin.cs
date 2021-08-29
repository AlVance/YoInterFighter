using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

namespace Code
{
    public class PlayFabLogin
    {
        public event Action<string> OnSuccess;
        public bool named;
        LoginResult loginRslt;
        string customId;

        public void Login()
        {
            if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId)) 
                // Busca PlayFabSharedSettings para cambiar este valor
            {
                /*
            Please change the titleId below to your own titleId from PlayFab Game Manager.
            If you have already set the value in the Editor Extensions, this can be skipped.
            */
                PlayFabSettings.staticSettings.TitleId = "TU TITLE ID";
            }


            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true }
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
            /*
#if UNITY_ANDROID
            var androidRequest = new LoginWithAndroidDeviceIDRequest
            {
                AndroidDeviceId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true }
            };
            PlayFabClientAPI.LoginWithAndroidDeviceID(androidRequest, OnLoginSuccess, OnLoginFailure);
#elif UNITY_IOS
            var iosRequest = new LoginWithIOSDeviceIDRequest
            {
                DeviceId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true,
                InfoRequestParameters = new GetPlayerCombinedInfoRequestParams { GetPlayerProfile = true }
            };
            PlayFabClientAPI.LoginWithIOSDeviceID(iosRequest, OnLoginSuccess, OnLoginFailure);
#else
            var request = new LoginWithCustomIDRequest 
            { 
                CustomId = "PlayerDinoGame", CreateAccount = true, InfoRequestParameters = new GetPlayerCombinedInfoRequestParams {GetPlayerProfile = true } 
            };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
#endif
            */
            // SystemInfo.deviceUniqueIdentifier;
        }

        private void OnLoginSuccess(LoginResult result)
        {
            loginRslt = result;
            Debug.Log("Login " + (result.InfoResultPayload.PlayerProfile == null));
            OnSuccess?.Invoke(result.PlayFabId);
            string name = null;
            if(result.InfoResultPayload.PlayerProfile != null)
            {
                name = result.InfoResultPayload.PlayerProfile.DisplayName;
                if(name != null) named = true; else  named = false;
                Debug.Log("Name Inicoi " + name);
            }
            else
            {
                result.InfoResultPayload.PlayerProfile.DisplayName = "Otro Chavalito";
            }
        }

        public bool CheckNamed()
        {
            return named;
        }

        public void SubmitNameButton(InputField nameField)
        {
            var request = new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = nameField.text,
            };
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnLoginFailure);
        }

        void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
        {
            Debug.Log("Update disaple name!");

        }

        private void OnLoginFailure(PlayFabError error)
        {
            Debug.LogError($"Here's some debug information: {error.GenerateErrorReport()}");
        }
    }
}
