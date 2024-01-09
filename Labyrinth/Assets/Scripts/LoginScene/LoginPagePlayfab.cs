using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEditor.PackageManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using PlayFab.PfEditor.EditorModels;

public class LoginPagePlayfab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TopText;
    [SerializeField] TextMeshProUGUI MessageText;

    [Header("Login")]
    [SerializeField] TMP_InputField EmailLoginInput;
    [SerializeField] TMP_InputField PasswordLoginInput;
    [SerializeField] GameObject LoginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField EmailRegisterInput;
    [SerializeField] TMP_InputField PasswordRegisterInput;
    [SerializeField] TMP_InputField UserNameRegisterInput;
    [SerializeField] GameObject RegisterPage;

    [Header("Recovery")]
    [SerializeField] TMP_InputField EmailRecoveryInput;
    [SerializeField] GameObject RecoveryPage;


    [Header("Welcome")]
    [SerializeField] private GameObject WelcomeObject;
    [SerializeField] private Text WelcomeText;

    [Header("Login Manager")]
    [SerializeField] private LoginManager loginManager;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginUser() 
    {
        var request = new LoginWithEmailAddressRequest {
            Email = EmailLoginInput.text,
            Password = PasswordLoginInput.text,

            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };

        PlayFabClientAPI.LoginWithEmailAddress(request,OnLoginSucces, OnError);
    }


    private void OnLoginSucces(PlayFab.ClientModels.LoginResult result)
    {
        string userName = null;

        if (result.InfoResultPayload != null) {
            userName = result.InfoResultPayload.PlayerProfile.DisplayName;


            WelcomeObject.SetActive(true);
            WelcomeText.text = "Welcome " + userName;
        }


        if(loginManager != null)
        {
            loginManager.playerName = userName;
        }

        StartCoroutine(LoadNextScene());
    }


    public void RegisterUser() 
    {

        if (PasswordLoginInput.text.Length < 6)
        {
            MessageText.text = "Your password is to short! Must be longer than 6 characters";
        }

        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = UserNameRegisterInput.text,
            Email = EmailRegisterInput.text,
            Password = PasswordRegisterInput.text,

            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, OnError);
    }

    private void OnError(PlayFab.PlayFabError error)
    {
        MessageText.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }

    private void OnRegisterSucces(RegisterPlayFabUserResult result)
    {
        MessageText.text = "New Account Is Created";
    }

    public void RecoverUserPassword() 
    {
        Debug.Log("Recover method");
        var request = new SendAccountRecoveryEmailRequest 
        { 
            Email = EmailRecoveryInput.text,
            TitleId = "CD2A5",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverySuccess, OnErrorRecovery);
    }

    private void OnErrorRecovery(PlayFab.PlayFabError error)
    {
        MessageText.text = "No Email Found";
    }

    private void OnRecoverySuccess(SendAccountRecoveryEmailResult result)
    {
        OpenLoginPage(); ;
        MessageText.text = "Recovery mail send";
    }

    #region Button Functions
    public void OpenLoginPage()
    {
        WelcomeObject.SetActive(false);

        LoginPage.SetActive(true);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(false);
        TopText.text = "Login";
    }

    public void OpenRegisterPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(true);
        RecoveryPage.SetActive(false);
        TopText.text = "Register";

    }

    public void OpenRecoveryPage()
    {
        LoginPage.SetActive(false);
        RegisterPage.SetActive(false);
        RecoveryPage.SetActive(true);
        TopText.text = "Recovery";

    }
    #endregion

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(2);

        MessageText.text = "Login in";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
