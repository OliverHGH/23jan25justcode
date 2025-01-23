using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameManage;
class Loadinfo
{
    public bool loading; //true if loading save, false if new game
    public string slotpath;
}
public class Datatosave
{
    public int numbersave;
    public string stringsave;
    public int LevelNum;
    public float[] Ppos = new float[3];
    public float[] Epos = new float[3];
    public Difficulty dlevel;
    public string[] inventoryslot1 = new string[10];
    public string[] inventoryslot2 = new string[10];
    public string[] inventoryslot3 = new string[10];
    public string[] inventoryslot4 = new string[10];
    public string[] inventoryslot5 = new string[10]; // saves each slot as array as JOSN has no 2d arrays

}
public class titlescreenmanager : MonoBehaviour
{
    public GameObject mainmenu, loadslots;
    public Button campaign, load, survival, slot1, slot2, back;
    void Start()
    {
        campaign.onClick.AddListener(NewCamapaign);
        load.onClick.AddListener(GoToLoad);
        survival.onClick.AddListener(NewSurvival);
        back.onClick.AddListener(BacktoMain);
        slot1.onClick.AddListener(Load1);
        slot2.onClick.AddListener(Load2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GoToLoad()
    {
        mainmenu.SetActive(false);
        loadslots.SetActive(true);
    }

    void NewSurvival()
    {


    }

    void NewCamapaign()
    {
        Loadinfo loadinfo = new Loadinfo();
        loadinfo.loading = false;
        File.WriteAllText(Application.persistentDataPath + "loadinfo", JsonUtility.ToJson(loadinfo));
        SceneManager.LoadScene("TestC1");
    }

    void Load1()
    {
        string LoadData = File.ReadAllText(Application.persistentDataPath + "/PlayerData1.json");
        Datatosave d = JsonUtility.FromJson<Datatosave>(LoadData);
        int scenetoload = d.LevelNum;
        Loadinfo info = new Loadinfo();
        info.loading = true;
        info.slotpath = Application.persistentDataPath + "/PlayerData1.json";
        File.WriteAllText(Application.persistentDataPath + "loadinfo", JsonUtility.ToJson(info));
        SceneManager.LoadScene(scenetoload);
    }

    void Load2()
    {
        string LoadData = File.ReadAllText(Application.persistentDataPath + "/PlayerData2.json");
        Datatosave d = JsonUtility.FromJson<Datatosave>(LoadData);
        int scenenum = d.LevelNum;
        Loadinfo info = new Loadinfo();
        info.loading = true;
        info.slotpath = Application.persistentDataPath + "/PlayerData2.json";
        File.WriteAllText(Application.persistentDataPath + "loadinfo",JsonUtility.ToJson(info));
        SceneManager.LoadScene(scenenum);
    }
    void BacktoMain()
    {
        mainmenu.SetActive(true);
        loadslots.SetActive(false);
    }
}
