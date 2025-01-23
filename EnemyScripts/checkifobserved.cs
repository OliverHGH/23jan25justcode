
using UnityEngine;
public class checkifobserved : MonoBehaviour
{
    public Transform player, playercamera, enemy;
    int countcheck;
    public LayerMask walls;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      
    }
    
     public bool observecheck()
    {

        bool A = (ObservedX());
        if(A == false)
        {
            return A;
        }
        else
        {
            bool B = ObservedY();
            if(B == false)
            {
                return B;
            }
            else
            {
                PathClear();
                bool C;
                if (countcheck > 3)
                {
                    C = false;
                }
                else
                {
                    C = true;
                }
                return C;
            }
        }
    }
     void PathClear()
    {
        Vector3 Eright = enemy.position + player.transform.right * 0.7f;
        Vector3 ELeft = enemy.position - player.transform.right * 0.7f;
        Vector3 enemytop = enemy.position;
        Vector3 enemybottom = enemy.position;
        enemytop.y += 1.5f;
        enemybottom.y -= 1.4f;
        bool A = cansee(playercamera.position, ELeft);
        bool B = cansee(playercamera.position, Eright);
        bool C = cansee(playercamera.position, enemytop-player.transform.forward * 0.7f);
        bool D = cansee(playercamera.position, enemybottom- player.transform.forward * 0.7f);
        bool E = cansee(playercamera.position, enemy.position- player.transform.forward*0.7f);
        bool anyFalse = !A || !B || !C || !D || !E;
        if(anyFalse == true)
        {
            countcheck = 0;
        }
        else
        {
            countcheck += 1;
        }
    }
    bool cansee(Vector3 a ,Vector3 b)
    {
        return Physics.Linecast(a, b, walls);
    }
     bool ObservedY()
    {
        float eypos = enemy.position.y;
        float bottom = eypos - 1.5f;
        float top = eypos + 1.5f;
        float dif = findDif(playercamera.position, enemy.position);
        float topdif = (top-playercamera.position.y);
        float topangle= Mathf.Rad2Deg * Mathf.Atan(topdif / dif);
        float angle = playercamera.transform.eulerAngles.x;
        if (angle <= 360 && angle >= 270)
        {
            angle = ((270 - angle) + 90);

        }
        else
        {
            angle = -angle;
        }
        if (Mathf.Abs(angle - topangle) < 31 + (8/dif))
        {
            return true;
        }
        float botdif = (playercamera.position.y-bottom);
        float botangle =-( Mathf.Rad2Deg * Mathf.Atan(botdif / dif));
        if (Mathf.Abs(angle - botangle) < 35 + (8 / dif))
        {
            return true;
        }
        
        return false;

    }

     bool ObservedX()
    {
        Vector2 pp = new Vector2(player.position.x, player.position.z);
        Vector2 ep = new Vector2(enemy.position.x, enemy.position.z);
        Vector2 dahead = new Vector2(player.forward.x, player.forward.z);

        dahead = normaliser(dahead);
        pp = normaliser(ep - pp);
        float dotProduct = (dahead.x * pp.x + dahead.y * pp.y);
        float angle = Mathf.Rad2Deg * Mathf.Acos(dotProduct);
        float dif= (findDif(player.position, enemy.position));
        if (angle < 58 + 20/dif)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  
    float findDif (Vector3 a, Vector3 b)
    {
        float xdif = a.x - b.x;
        float zdif = a.z - b.z;
        return (Mathf.Sqrt(xdif * xdif + zdif * zdif));
    }
    Vector2 normaliser(Vector2 vtnorm)
    {
        float dist = Mathf.Sqrt(vtnorm.x*vtnorm.x+vtnorm.y*vtnorm.y);
        Vector2 final = new Vector2(vtnorm.x / dist, vtnorm.y / dist);
        return final;

    }
}
