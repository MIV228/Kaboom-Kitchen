using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableHandGrabbingModel : MonoBehaviour
{
    public GameObject leftHandModel;
    public GameObject rightHandModel;

    // Start is called before the first frame update
    void Start()
    {
        XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(HideGrabbingHand);
        grabInteractable.selectExited.AddListener(ShowGrabbingHand);
    }

    public void HideGrabbingHand(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.tag == "LeftHand")
        {
            leftHandModel.SetActive(false);
        }
        else if (args.interactorObject.transform.tag == "RightHand")
        {
            rightHandModel.SetActive(false);
        }
    }

    public void ShowGrabbingHand(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.tag == "LeftHand")
        {
            leftHandModel.SetActive(true);
        }
        else if (args.interactorObject.transform.tag == "RightHand")
        {
            rightHandModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
