using Unity.Usercentrics;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Demo example.
/// This class contains a complete example of the Usercentrics integration.
/// This is:
/// - Initialization
/// - Update Services
/// - Show First and Second Layer
///
/// It also contains an example of AppTrackingTransparency usage.
/// 
/// Note that in a real integration the initialization should occur in some
/// initial stage of your game, for example the splash screen.
/// </summary>
public class DemoScript : MonoBehaviour
{
    [SerializeField] private Button ShowFirstLayerButton = null;
    [SerializeField] private Button ShowSecondLayerButton = null;
    [SerializeField] private Button ShowAttButton = null;

    void Awake()
    {
        InitAndShowConsentManagerIfNeeded();
    }

    private void Start()
    {
        ShowFirstLayerButton.onClick.AddListener(() => { ShowFirstLayer(); });
        ShowSecondLayerButton.onClick.AddListener(() => { ShowSecondLayer(); });
        ShowAttButton.onClick.AddListener(() => { ShowAtt(); });
    }

    private void InitAndShowConsentManagerIfNeeded()
    {
        Usercentrics.Instance.Initialize((usercentricsReadyStatus) =>
        {
            if (usercentricsReadyStatus.shouldCollectConsent)
            {
                ShowFirstLayer();
            }
            else
            {
                UpdateServices(usercentricsReadyStatus.consents);
            }
        },
        (errorMessage) =>
        {
            // Log and collect the error...
        });
    }

    private void ShowAtt()
    {
        AppTrackingTransparency.Instance.PromptForAppTrackingTransparency((status) =>
        {
            switch (status)
            {
                case AuthorizationStatus.AUTHORIZED:
                    break;
                case AuthorizationStatus.DENIED:
                    break;
                case AuthorizationStatus.NOT_DETERMINED:
                    break;
                case AuthorizationStatus.RESTRICTED:
                    break;
            }
        });
    }

    private void ShowFirstLayer()
    {
        Usercentrics.Instance.ShowFirstLayer(UsercentricsLayout.Sheet, (usercentricsConsentUserResponse) =>
        {
            UpdateServices(usercentricsConsentUserResponse.consents);
        });
    }

    private void ShowSecondLayer()
    {
        Usercentrics.Instance.ShowSecondLayer(true, (usercentricsConsentUserResponse) =>
        {
            UpdateServices(usercentricsConsentUserResponse.consents);
        });
    }

    private void UpdateServices(List<UsercentricsServiceConsent> consents)
    {
        foreach (var serviceConsent in consents)
        {
            switch (serviceConsent.templateId)
            {
                case "XxxXXxXxX":
                    // GoogleAdsFramework.Enabled = service.consent.status;
                    break;
                case "YYyyYyYYY":
                    // AnalyticsFramework.Enabled = service.consent.status;
                    break;
                default:
                    break;
            }
        }
    }
}
