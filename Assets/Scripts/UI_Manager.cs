using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Button _exitButton;


    //���������� ������ ������
    //https://youtu.be/j9ExKQbhfBU
    //��� ������ ����� ������ ����� ��������� �� 3 ������
    //1 ���������� ��� ������
    private void OnEnable()
    {
        //onClick.AddListener ��� .NET ������� c ���������
        //https://youtu.be/XplFktDsPs0
        _exitButton.onClick.AddListener(Exit);
        //2 ��� � ��� ���������� ��������� �����
    }

    private void OnDisable()
    {
        _exitButton.onClick.RemoveListener(Exit);
        //3 �� ���������� ������� �����
    }

    public void Exit()
    {
        SaveLoadData.instance.Save();
        Application.Quit();
    }
}
