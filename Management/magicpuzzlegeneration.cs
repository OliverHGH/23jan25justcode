using UnityEngine;

public class ciphergeneration 
{
    string plaintext = "lookbehindyou";
    string[] keys = new string[] { "blink","help","stone","lonely"};
    public string keytodisplay, encodedtext;
   public void CreateCipher()
    {
        int length = keys.Length;
        string key = keys[Random.Range(0,length)];
        int keylength = key.Length;
        int plaintextlenth = plaintext.Length;
        string fulllengthkey = "";
        for (int i = 0; i <  (plaintextlenth / keylength); i++)
        {
            fulllengthkey += key;
        }
        for (int i = 0;i < plaintextlenth % keylength; i++)
        {
            fulllengthkey += key[i];
        }
        string ciphertext = "";
        for(int i = 0; i < plaintextlenth; i++)
        {
            int charnum = (CharToInt(plaintext[i])+ CharToInt(fulllengthkey[i]))%26;
            ciphertext += IntToChar(charnum);
        }
        encodedtext = ciphertext;
        keytodisplay = key;
    }

    int CharToInt(char ch)
    {
        return (int)ch- (int)('a') ;
    }

    char IntToChar(int ch)
    {
        return (char)(ch+ (int)('a') );
    }
}
