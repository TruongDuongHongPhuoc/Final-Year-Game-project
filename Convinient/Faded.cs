using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
public class Faded : MonoBehaviour
{
    public static Faded Instance { get;private set; }
    public static bool fadedIn;
    public bool fadedOut;
    public float WaitBeforeFaded;
    [SerializeField] UnityEngine.UI.Image fadedImage;
    private void Awake() {
        if (Instance == null){
            Instance = this;
        }else if(Instance != null && Instance != this){
            Destroy(gameObject);
        }
    }
    private void Update()
    {
       if (fadedIn)
        {
            if (fadedImage.color.a < 1)
            {
                Color newColor = fadedImage.color;
                newColor.a += Time.deltaTime;
                fadedImage.color = newColor;
                // Check if the alpha value is close enough to 1
                if (fadedImage.color.a >= 1f)
                {
                    fadedIn = false;
                    StartCoroutine(waitBeforeFadedOut());
                }
            }
        }
        if (fadedOut)
        {
            if (fadedImage.color.a > 0)
            {
                Color newColor = fadedImage.color;
                newColor.a -= Time.deltaTime;
                fadedImage.color = newColor;
                // Check if the alpha value is close enough to 0
                if (fadedImage.color.a <= 0.01f)
                {
                    fadedOut = false;
                    fadedImage.gameObject.SetActive(false);
                }
            }
        }
    }
    public void FadedOutTrue(){
        fadedOut = true;
    }
    public void FadedInTrue(float SetTimeWait){
        WaitBeforeFaded = SetTimeWait;
        fadedIn = true;
        fadedImage.gameObject.SetActive(true);
    }
    public void FadedInTrue(){
        FadedInTrue(0f);
    }
    IEnumerator waitBeforeFadedOut(){
        yield return new WaitForSecondsRealtime(WaitBeforeFaded);
        FadedOutTrue();
    }
}
