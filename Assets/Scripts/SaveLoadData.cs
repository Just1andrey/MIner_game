using UnityEngine;

//����������))) ����� ���� ������� ��� ���� ��� �� ������ Code Blog ��� ������ ��������
//� �����
//https://youtu.be/od_hbf_sdvU
//� ������� �����
//https://youtu.be/2nJqGGL-84E
public class SaveLoadData : MonoBehaviour, ISaveLoadData
{
    #region Singleton
    //��� ������� ��������(��������)
    //https://youtu.be/HFfSHlmMVzk

    //��� ����������� ��������, �������� ����� ��� ������� ����������
    //https://youtu.be/0Q1IHASPzms
    public static SaveLoadData instance { get; private set; }

    void Awake()
    {
        //���� �� ���������� ���� ������ � ������� �������� ������� - �� ���������������))))
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion

    //���� �������� ���������� ���������� � ����������� ������ � JSON, ��������� �� �������� ������ ������/�������
    //�� ���� ������� �� �� ������ �������� ������� Save � Load, �� ����� ��� ��������� ISaveLoadData �� ���� ����)))
    public void Load()
    {
        CheckPrefsExist("Money");
        CheckPrefsExist("Exp");
        CheckPrefsExist("Skill");

        Properties.instance.Money = PlayerPrefs.GetInt("Money");
        Properties.instance.Exp = PlayerPrefs.GetInt("Exp");
        Properties.instance.Skill = PlayerPrefs.GetInt("Skill");
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Money", Properties.instance.Money);
        PlayerPrefs.SetInt("Exp", Properties.instance.Exp);
        PlayerPrefs.SetInt("Skill", Properties.instance.Skill);
    }

    void CheckPrefsExist(string value)
    {
        if (!PlayerPrefs.HasKey(value))
            PlayerPrefs.SetInt(value,0);
    }
}
