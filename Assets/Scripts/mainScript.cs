using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mainScript : MonoBehaviour
{
    private Text titleText;
    private Text buttonText;

    private Image resultImage;
    private Sprite correct;
    private Sprite incorrect;

    private string[] words = { "LEARN SIGN LANGUAGE", "Hello!", "What's up?", "What is your name?", "Nice", "Nice to meet you" };
    private string[] buttonTexts = { "START", "TRY" };

    private enum states
    {
        MAIN_MENU,
        HELLO,
        WHATS_UP,
        WHAT_IS_YOUR_NAME,
        NICE,
        NICE_TO_MEET_YOU
    };
    private states currentState;

    private enum cameraStates
    {
        INIT,
        ORIGINAL,
        SPANNED
    }
    private cameraStates currentCameraState;

    private Animator animator;
    private Animator cameraAnimator;

    void Start()
    {
        currentState = states.MAIN_MENU;
        currentCameraState = cameraStates.INIT;

        titleText = GetComponent<Text>();
        titleText = GameObject.Find("TitleText").GetComponent<Text>();
        titleText.text = words[0];

        buttonText = GameObject.Find("ButtonText").GetComponent<Text>();
        buttonText.text = buttonTexts[0];

        resultImage = GameObject.Find("ResultImage").GetComponent<Image>();
        correct = Resources.Load<Sprite>("correct");
        incorrect = Resources.Load<Sprite>("incorrect");

        animator = GameObject.Find("Human").GetComponent<Animator>();
        cameraAnimator = GameObject.Find("GameObject").GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void nextPress()
    {
        switch (currentState)
        {
            case states.MAIN_MENU:
                {
                    //Button not visible in this state
                    break;
                }

            case states.HELLO:
                {
                    tryPress();
                    animator.SetTrigger("WHATS_UP");
                    titleText.text = words[2];
                    currentState = states.WHATS_UP;
                    break;
                }

            case states.WHATS_UP:
                {
                    tryPress();
                    animator.SetTrigger("WHAT_IS_YOUR_NAME");
                    titleText.text = words[3];
                    currentState = states.WHAT_IS_YOUR_NAME;
                    break;
                }

            case states.WHAT_IS_YOUR_NAME:
                {
                    tryPress();
                    animator.SetTrigger("NICE");
                    titleText.text = words[4];
                    currentState = states.NICE;
                    break;
                }

            case states.NICE:
                {
                    tryPress();
                    animator.SetTrigger("NICE_TO_MEET_YOU");
                    titleText.text = words[5];
                    currentState = states.NICE_TO_MEET_YOU;
                    break;
                }

            case states.NICE_TO_MEET_YOU:
                {
                    currentCameraState = cameraStates.ORIGINAL;
                    cameraAnimator.SetTrigger("CAMERA_REVERSE_SPAN");
                    animator.SetTrigger("WAIT");
                    buttonText.text = buttonTexts[0];
                    titleText.text = words[0];
                    currentState = states.MAIN_MENU;
                    break;
                }
        }
    }

    public void tryPress()
    {
        if (currentState == states.MAIN_MENU)
        {
            animator.SetTrigger("HELLO");
            buttonText.text = buttonTexts[1];
            titleText.text = words[1];
            currentState = states.HELLO;
        }
        else
        {
            switch (currentCameraState)
            {
                case cameraStates.INIT:
                    {
                        currentCameraState = cameraStates.SPANNED;
                        cameraAnimator.SetTrigger("INIT");
                        break;
                    }

                case cameraStates.ORIGINAL:
                    {
                        currentCameraState = cameraStates.SPANNED;
                        cameraAnimator.SetTrigger("CAMERA_SPAN");
                        break;
                    }

                case cameraStates.SPANNED:
                    {
                        currentCameraState = cameraStates.ORIGINAL;
                        cameraAnimator.SetTrigger("CAMERA_REVERSE_SPAN");
                        break;
                    }
            }
        }
    }

    public void showCorrect()
    {
        resultImage.sprite = correct;
        Color color = resultImage.color;
        color.a = 255;
        resultImage.color = color;
        StartCoroutine(HideResult());
    }

    public void showWrong()
    {
        resultImage.sprite = incorrect;
        Color color = resultImage.color;
        color.a = 255;
        resultImage.color = color;
        StartCoroutine(HideResult());
    }

    private IEnumerator HideResult()
    {
        yield return new WaitForSeconds(3.0f);
        Color color = resultImage.color;
        color.a = 0;
        resultImage.color = color;
    }
}
