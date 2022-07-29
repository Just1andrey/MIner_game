using UnityEngine;

//интерфейсы))) перед ними изучаем всю тему ооп на канале Code Blog или других ресурсах
//в юнити
//https://youtu.be/od_hbf_sdvU
//в обычном шарпе
//https://youtu.be/2nJqGGL-84E
public class SaveLoadData : MonoBehaviour, ISaveLoadData
{
    #region Singleton
    //Это паттерн синглтон(одиночка)
    //https://youtu.be/HFfSHlmMVzk

    //это статическое свойство, работает почти как обычная переменная
    //https://youtu.be/0Q1IHASPzms
    public static SaveLoadData instance { get; private set; }

    void Awake()
    {
        //если ты прикрепишь этот скрипт к другому игровому объекту - он самоуничтожится))))
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion

    //если захотите перепишите реализацию с сохранением данных в JSON, изменения не затронут другие классы/скрипты
    //ну если конечно же не менять названия методов Save и Load, но тогда вам интерфейс ISaveLoadData по жопе даст)))
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
