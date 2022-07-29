using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Button _exitButton;


    //правильная работа кнопок
    //https://youtu.be/j9ExKQbhfBU
    //для каждой новой кнопки нужно добавлять по 3 строки
    //1 переменная для кнопки
    private void OnEnable()
    {
        //onClick.AddListener это .NET событие c делегатом
        //https://youtu.be/XplFktDsPs0
        _exitButton.onClick.AddListener(Exit);
        //2 тут в эту переменную добавляем метод
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(Exit);
        //3 из переменной убираем метод
    }

    public void Exit()
    {
        SaveLoadData.instance.Save();
        Application.Quit();
    }
}
