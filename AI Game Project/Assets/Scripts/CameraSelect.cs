using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSelect : MonoBehaviour
{

    public Dropdown cameraDrop;
    public RawImage previewImage;

    private WebCamTexture CameraVideo;

    void Start()
    {
        PopulateDropdown();

        //Listens for when a new camera is selected and calls the method to change it.
        cameraDrop.onValueChanged.AddListener(delegate
        {
            OnSelection(cameraDrop);
        });
    }

    void PopulateDropdown() //Populate dropdown with all available cameras on system
    {
        //We put all the devices connected to the computer into a list called "devices"
        WebCamDevice[] devices = WebCamTexture.devices;

        cameraDrop.ClearOptions();

        //For each of theses devices we add the names to the dropdown we created
        foreach(WebCamDevice device  in devices)
        {
            cameraDrop.options.Add(new Dropdown.OptionData(device.name));
        }

        //Update display
        cameraDrop.RefreshShownValue();
    }

    void OnSelection(Dropdown dropDown)
    {
        //This statment checks if the camera is playing AND if the video isn't null.
        //If so, then stop the camera, begin to change to the new selected camera.
        if (CameraVideo != null && CameraVideo.isPlaying)
        {
            CameraVideo.Stop();
        }

        //Gets the name of the new selected camera and puts it into string value
        string selectedCam = dropDown.options[dropDown.value].text;

        StartCamera(selectedCam);

    }
    void StartCamera(string cameraName)
    {
        //Find the selected camera device
        WebCamDevice selectedCam = default;

        foreach (WebCamDevice device in WebCamTexture.devices)
        {
            if (device.name.Equals(cameraName))
            {
                selectedCam = device;
                break;
            }
        }

        //This will basically change the raw image UI element into the camera output/video.
        if (!string.IsNullOrEmpty(selectedCam.name))
        {
            CameraVideo = new WebCamTexture(selectedCam.name);

            previewImage.texture = CameraVideo;

            CameraVideo.Play();
        }
        else //Error checking incase the camera isn't found
        {
            Debug.LogError("Selected camera not found!");
        }
    }
}
