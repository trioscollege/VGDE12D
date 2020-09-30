
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;       // Needed to access UI elements we use in our game.
using UnityEngine.Audio;    // Needed to access Audio elements we use, such as the AudioMixer.
using TMPro;                // Needed to access TextMeshPro elements.

public class SettingsManager : MonoBehaviour
{
    // public reference to AudioMixer to allow control of volume in the Settings menu
    public AudioMixer m_audioMixer;

    // private array of Resolutions to use in the Settings menu.
    private Resolution[] m_resolutions;
    // private array of strings to use in the Settings menu with the Graphics dropdown in the Settings menu.
    private string[] m_qualitySettings;

    // public reference to the Resolutions Dropdown menu.
    public TMP_Dropdown m_resolutionsDropdown;
    // public reference to the Graphics Quality Dropdown menu.
    public TMP_Dropdown m_qualityDropdown;
    // public reference to the Fullscreen Toggle.
    public Toggle m_fullscreenToggle;

    void Start(){
        // On Start, configure the Resolutions and Quality Settings options, as well as configuring the Fullscreen toggle.
        ConfigureResolutions();
        ConfigureQualitySettings();
        SetFullscreen(Screen.fullScreen);
    }

    // Function to configure the supported resolutions available on the system the program is being run on,
    // then apply those options to the Resolution Dropdown menu.
    void ConfigureResolutions(){
        // Get the list of resolutions from the system using Screen.resolutions, 
        // then save those results to our m_resolutions array.
        m_resolutions = Screen.resolutions;

        // Clear the options currently listed in our m_resolutionsDropdown.
        m_resolutionsDropdown.ClearOptions();

        // Create a new list of strings so that we can create our new dropdown menu options.
        // This is done because you cannot directly convert a Resolution to a text or string value for use in the way we want.
        List<string> resolutionOptions = new List<string>();

        // An int variable for us to use while in the ConfigureResolutions() function to identify which resolution
        // the system has by default (Native Resolution).
        int currentResolutionIndex = 0;

        // Loop Breakdown:
        // Loop thru the array of resolutions we gathered earlier...
        // Create a new string variable called 'option', create the string based on 
        // the width and height of the resolution we're looking at in the loop.
        // 
        // Example: 
        //      string option = m_resolutions[i].width + " x " + m_resolutions[i].height;
        // Turns into:
        //      string option = "1024" + " x " + "768";
        // Creating:
        //      string option = "1024 x 768";
        // 
        // Add the new string that we've just created to the list we created earlier called resolutionOptions.
        for(int i = 0; i < m_resolutions.Length; i++){
            string option = m_resolutions[i].width + " x " + m_resolutions[i].height;
            resolutionOptions.Add(option);

            // Quickly check and see if the resolution we're currently looking at matches the
            // screen resolution the system is using. If it does, set the currentResolutionIndex
            // to equal i, representing the current resolution in our array.
            if(m_resolutions[i].width == Screen.currentResolution.width &&
               m_resolutions[i].height == Screen.currentResolution.height){
                currentResolutionIndex = i;
            }
        }

        // Once we've finished looping thru all the available resolutions, 
        // Access the m_resolutionsDropdown and use AddOptions to add our list of strings
        // to the m_resolutionsDropdown list of options, which we cleared earlier.
        m_resolutionsDropdown.AddOptions(resolutionOptions);
        // Set the value of our m_resolutionsDropdown menu to the value from our currentResolutionIndex.
        m_resolutionsDropdown.value = currentResolutionIndex;
        // Refresh the m_resolutionsDropdown so that it now shows the value that matches the currentResolutionIndex
        // that we set earlier, which matches the native resolution of the system.
        m_resolutionsDropdown.RefreshShownValue();
    }

    // Function to configure the available Quality settings based on the settings in Unity,
    // then apply the options to the Quality Dropdown Menu.
    // This acts very similarly to the ConfigureResolutions() function.
    void ConfigureQualitySettings(){
        // Access the m_qualitySettings array and give it the names of our Unity Quality Settings.
        m_qualitySettings = QualitySettings.names;

        // Clear the options currently listed in our Quality Dropdown menu.
        m_qualityDropdown.ClearOptions();

        // Create a new list of strings so we can create a new list for our Quality options.
        List<string> qualityOptions = new List<string>();

        // Create an int value called currentQualityIndex to store our current 
        // Quality settings based on the default of the system.
        int currentQualityIndex = QualitySettings.GetQualityLevel();

        // Loop thru each Quality setting in our array, create a new string called option
        // and assign it the name of the quality settings we're on in our array, then
        // add that option to our new list so we can populate the dropdown menu.
        for(int i = 0; i < m_qualitySettings.Length; i++){
            string option = m_qualitySettings[i];
            qualityOptions.Add(option);
        }

        // Add the list of options to the Quality Dropdown menu
        m_qualityDropdown.AddOptions(qualityOptions);
        // Set the Quality setting to the one we stored in the currentQualityIndex.
        m_qualityDropdown.value = currentQualityIndex;
        // Update the Dropdown to show the currently selected setting on our list.
        m_qualityDropdown.RefreshShownValue();
    }
    
    // Function to set the Screen Resolution based on the Resolution Dropdown Menu in the Settings menu.
    // The function requires an int parameter we called resolutionIndex, which will be the number associated
    // to the menu selection the user makes. This is automatic from Unity.
    public void SetResolution(int resolutionIndex){
        // Create a temporary value of type Resolution, set it to the entry in our m_resolutions 
        // equal to the resolutionIndex (the menu selection value).
        Resolution resolution = m_resolutions[resolutionIndex];
        // Using our temporary value, assign the new screen resolution based on the value's width and height values.
        // Pass in the current Screen.fullScreen value as the third required parameter of the SetResolution function.
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // Function to set the Graphics Quality based on the Graphics Quality Dropdown Menu in the Settings menu.
    // The function requires an int parameter we called qualityIndex, which will be the number associated
    // to the menu selection the user makes. This is automatic from Unity.
    public void SetGraphicsQuaulity(int qualityIndex){
        // Using the qualityIndex value we pass in from the menu, assign the new QualityLevel.
        // The SetQualityLevel() function requires a second parameter to say wether or not to 
        // perform expensive changes, such as changing Anti-Aliasing. For this, we'll pass in true,
        // however if your game is more highly detailed, you may want to consider using false for 
        // this and providing the user and option to restart the game to apply the changes they want to make.
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }
    
    // Function to set Screen.fullscreen based on the FullscreenToggle in the Settings menu.
    // The function requires a bool parameter we called isFullscreen, which will be the status of the
    // Toggle we put on our Settings screen.
    public void SetFullscreen(bool isFullscreen){
        // Set the Screen.fullScreen variable to be what the isFullscreen value is when it is passed in.
        Screen.fullScreen = isFullscreen;
    }

    // Function to set AudioMixer volume based on the VolumeSlider in the Settings menu.
    // The function requires a float parameter we called volume, which will be the number associated
    // to the position of the Volume Slider on our Settings screen.  
    public void SetVolume(float volume){
        // Using the SetFloat function in the AudioMixer, pass in the name of the variable we exposed
        // from the mixer in Unity, then pass in the new volume based on the value we passed 
        // into the SetVolume funciton.
        m_audioMixer.SetFloat("MainAudioVolume", volume);
    }
}
