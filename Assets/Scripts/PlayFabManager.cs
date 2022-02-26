using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabManager : MonoBehaviour
{

    [SerializeField] private GameObject submitNameWindow;
    [SerializeField] private InputField nameInput;

    // Start is called before the first frame update
    void Start()
    {
        submitNameWindow.SetActive(false);
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFail);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Succesful Login/Account create!");
        string name = null;
        if (result.InfoResultPayload.PlayerProfile != null)
        {
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        }
        if (name == null)
        {
            submitNameWindow.SetActive(true);
        }
    }

    void OnLoginFail(PlayFabError error)
    {

        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
        submitNameWindow.SetActive(false);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Registered and logged in!");
    }

    public void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void sendPlayerData()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"N-Back Score", PlayerPrefs.GetFloat("NBackScore", 0).ToString() },
                {"N-Back Time", PlayerPrefs.GetString("NBackTime", "0:00:00") },
                {"Digit Span Score", PlayerPrefs.GetFloat("DigitSpanScore", 0).ToString() },
                {"Stroop Score", PlayerPrefs.GetFloat("StroopScore", 0).ToString() },
                {"Stroop Reaction Average", PlayerPrefs.GetFloat("StroopReaction", 0).ToString() }
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    void OnDataSend(UpdateUserDataResult result)
    {
        Debug.Log("Successful user data send!");
    }
}
