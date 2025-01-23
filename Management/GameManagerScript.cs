using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
namespace GameManage
{
    public enum Difficulty
    { 
        Easy,
        Normal,
        Difficult
    }


    public class GameManagerScript : MonoBehaviour
    {
        public Difficulty currentdifficulty;
        public List<AngelBT> enemylist = new List<AngelBT>();
        public List<GameObject> enemybodylist = new List<GameObject>();
        public GameObject playertopause,cameratopause, pausemenu, enemy1obj,enemy2obj;
        public Button d1,d2,d3, q;
        private bool gamepaused = false;
        public bool gameover = false;
        float timer, endtimer, wontimer;
        playerlook lookpauser;
        playermovement movepauser;
        public int thingscollectedforlevel1 =0;
        void Start()
        {
            pausemenu.SetActive(false);
            enemylist.Add(enemy1obj.GetComponent<AngelBT>());
            lookpauser = cameratopause.GetComponent<playerlook>();
            movepauser = playertopause.GetComponent<playermovement>(); 
            enemybodylist.Add(enemy1obj);
            if (enemy2obj != null)
            {
                enemylist.Add(enemy2obj.GetComponent<AngelBT>());
                enemybodylist.Add(enemy2obj);
            }
            currentdifficulty = Difficulty.Normal;
            timer = 0;
            d1.onClick.AddListener(MakeEasy);
            d2.onClick.AddListener(MakeNormal);
            d3.onClick.AddListener(MakeHard);
            q.onClick.AddListener(Quit);
            endtimer = 0;
            wontimer = 0;
            gamepaused = false;

        }

        void Quit()
        {
            SceneManager.LoadScene(0);
        }
        public void ChangeDifficulty(Difficulty d)
        {
            currentdifficulty=d;
            foreach(AngelBT x in enemylist)
            {
                x.ChangeDifficulty(d);
            }
        }
        void MakeEasy()
        {
            ChangeDifficulty(Difficulty.Easy);
        }
        void MakeNormal()
        {
            ChangeDifficulty(Difficulty.Normal);
        }
        void MakeHard()
        {
            ChangeDifficulty(Difficulty.Difficult);
        }

        void TogglePaused()
        {
            gamepaused =! gamepaused;
            if (gamepaused == true)
            {
                pausemenu.SetActive(true);
                lookpauser.paused = true;
                movepauser.paused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                foreach(AngelBT x in enemylist)
                {
                    x.paused = true;
                }
            }
            else
            {
                lookpauser.paused = false;
                movepauser.paused = false;
                pausemenu.SetActive(false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                foreach (AngelBT x in enemylist)
                {
                    x.paused = false;
                }
            }

        }
        void Update()
        {
            if (!gameover)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    TogglePaused();
                }
            }
            if (!gamepaused)
            {
                timer += Time.deltaTime;
                if (thingscollectedforlevel1 == 3)
                {
                    Debug.Log("Won level one");
                    wontimer += Time.deltaTime;
                }
                if (wontimer > 2)
                {
                    Loadinfo loadinfo = new Loadinfo();
                    loadinfo.loading = false;
                    File.WriteAllText(Application.persistentDataPath + "loadinfo", JsonUtility.ToJson(loadinfo));
                    SceneManager.LoadScene("TestC2");
                    SceneManager.LoadScene(2);
                }
                foreach (AngelBT x in enemylist)
                {
                    if (timer / (x.Encounters / 3) < 20 && timer > 120)
                    {
                        Debug.Log("this angel has been seen a lot");
                        x.MakeSneaky();
                    }
                    else if (x.Encounters / 3 > 0 && timer / (x.Encounters / 3) > 100 && timer > 120)
                    {
                        Debug.Log("you are hidey");
                        x.HuntBetter();
                    }
                }
            }
            if (gameover)
            {
                endgame();
                endtimer += Time.deltaTime;
                if (endtimer > 2.5)
                {
                    Debug.Log("game over");
                    SceneManager.LoadScene(0);
                }
               
            }
        }
        void endgame()
        {
           if(endtimer==0)
            {
                lookpauser.paused = true;
                movepauser.paused = true;
                int pos = 0;
                foreach (AngelBT x in enemylist)
                {
                    x.paused = true;
                    if (x.haskilledplayer)
                    {
                        cameratopause.transform.position = enemybodylist[pos].transform.position + enemybodylist[pos].transform.forward * 2f+ enemybodylist[pos].transform.up*1f;
                        cameratopause.transform.LookAt(enemybodylist[pos].transform.position + enemybodylist[pos].transform.up * 1f);
                    }
                    pos++;
                }

            }

        }
    }

}