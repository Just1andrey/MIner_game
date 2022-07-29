using UnityEngine;
using UnityEngine.UI;

public   class Properties : MonoBehaviour
{
    #region Singleton
    //Это паттерн синглтон(одиночка)
    //https://youtu.be/HFfSHlmMVzk

    //это статическое свойство, работает почти как обычная переменная
    //https://youtu.be/0Q1IHASPzms
    public static Properties instance { get; private set; }

    void Awake()
    {
        //если ты прикрепишь этот скрипт к другому игровому объекту - он самоуничтожится))))
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }
    #endregion

     [SerializeField] private Text MoneyText;
     [SerializeField] private Text ExpText;
    [SerializeField] private Text SkillText;

    private int _money;
    private int _exp;
    private int _skill;

    public int howMuchToAddMoney = 10;
    public int howMuchToAddExp = 5;
    public int howMuchToAddSkill = 1;
    public int maxExp = 50;

    public bool SaveResults = true;

    //свойства объекта, работают как переменные при изменении которых выполняется какая-то логика
    //https://youtu.be/0Q1IHASPzms
    [HideInInspector]
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;

            if (MoneyText != null)
                Data.Money = _money;
                MoneyText.text = _money.ToString();
            
        }
    }
    [HideInInspector]
    public int Exp
    {
        get
        {
            return _exp;
        }
        set
        {
            _exp = value;

            if (_exp >= maxExp)
            {
                AddSkill();
                _exp = 0;
            }


            if (ExpText != null)
                Data.opit = _exp;
                ExpText.text = _exp.ToString();
        }
    }
    [HideInInspector]
    public int Skill
    {
        get
        {
            return _skill;
        }
        set
        {
            _skill = value;
            if (SkillText != null)
                Data.profi = _skill;
                SkillText.text = _skill.ToString();
        }
    }

    void Start()
    {
        Money = 0;
        Exp = 0;
        Skill = 0;

        if (SaveResults)
            SaveLoadData.instance.Load();
        if (MoneyText == null || ExpText == null || SkillText == null)
            Debug.Log("Перенеси текстовые поля для денег/нвыков/опыта в переменные Properties.");
    }

    public void AddMoney()
    {
        Money += howMuchToAddMoney;
    }

    public void AddExp()
    {
        Exp += howMuchToAddExp;
    }

    public void AddSkill()
    {
        Skill += howMuchToAddSkill;
    }

}
