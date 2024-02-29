using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text _titleText_1;
    [SerializeField] private Text _titleText_2;
    [SerializeField] private GameObject _topFruit;
    [SerializeField] private GameObject _bottomFruit;


    private void Start()
    {
        LeanTween.moveLocalX(_titleText_1.gameObject, 150, 1f).setLoopClamp().setLoopType(LeanTweenType.pingPong);
        LeanTween.moveLocalX(_titleText_2.gameObject, 150, 1f).setDelay(0.5f).setLoopClamp().setLoopType(LeanTweenType.pingPong);
        LeanTween.rotateAround(_topFruit, Vector3.up, 360f, 10).setLoopClamp();
        LeanTween.rotateAround(_bottomFruit, Vector3.up, 360f, 10).setLoopClamp();
    }

    public void LoadGame()
    {
        SceneHandler.Instance.LoadScene(1);
    }
    public void QuitApp()
    {
        SceneHandler.Instance.QuitApp();
    }
}
