using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string TargetRoom = "SceneDev";
    public Button Button;
    public GameObject MenuCanvas;
    public GameObject CreditsCanvas;
    public GameObject RulesCanvas;
    private float ScaleX;
    private float ScaleY;

    public void Start()
    {
        ScaleX = Button.transform.localScale.x;
        ScaleY = Button.transform.localScale.y;
    }

    public void OnClickPlay() {
        SceneManager.LoadScene(TargetRoom);
    }

    public void OnClickQuit() {
        Application.Quit();
    }

    public void OnHovered() {
        Button.transform.localScale = new Vector3(ScaleX * 1.2f, ScaleY * 1.2f, 0);
    }

    public void OnNotHovered()
    {
        Button.transform.localScale = new Vector3(ScaleX, ScaleY, 0);
    }

    public void OnClickCredits()
    {
        OnNotHovered();
        CreditsCanvas.SetActive(true);
        MenuCanvas.SetActive(false);
        RulesCanvas.SetActive(false);
    }

    public void OnClickMenu()
    {
        OnNotHovered();
        MenuCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
        RulesCanvas.SetActive(false);
    }

    public void OnClickRules()
    {
        OnNotHovered();
        RulesCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
        MenuCanvas.SetActive(false);
    }
}
