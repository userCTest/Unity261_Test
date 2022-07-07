using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Usercentrics;
using System;

public class UC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Usercentrics.Instance.Initialize((usercentricsReadyStatus) =>
        {
           
            ShowFirstLayer();
            
        },
        (errorMessage) =>
        {
            // Log and collect the error...
        });
    }

    // Update is called once per frame
    void Update()
    {

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
            Console.WriteLine($"Service: {serviceConsent.dataProcessor} -- Consent: {serviceConsent.status}");
        }
    }
}
