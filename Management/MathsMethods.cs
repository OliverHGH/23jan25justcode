using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathsMethods 
{
    public Vector2 normaliser(Vector2 vtnorm)
    {
        float dist = Mathf.Sqrt(vtnorm.x * vtnorm.x + vtnorm.y * vtnorm.y);
        Vector2 final = new Vector2(vtnorm.x / dist, vtnorm.y / dist);
        return final;

    }
    public float findDif(Vector3 a, Vector3 b)
    {
        float xdif = a.x - b.x;
        float zdif = a.z - b.z;
        return (Mathf.Sqrt(xdif * xdif + zdif * zdif));
    }
    public bool islookingat(Transform a, Transform b, int maxanglex, int maxangley, LayerMask walls, bool fullcheck)
    {

        bool PathClear()
        {
            Vector3 rightofrecipient = b.position + a.transform.right * 0.7f;
            Vector3 leftofrecepient = b.position - a.transform.right * 0.7f;
            Vector3 topofrecipient = b.position;
            Vector3 bottomofrecipient = b.position;
            topofrecipient.y += 1.4f;
            bottomofrecipient.y -= 1.4f;
            bool E = lineofsightblocked(a.position, b.position - a.transform.forward * 0.3f);
            bool anylinesofisght = !E;
            if (fullcheck == true)
            {
                bool A = lineofsightblocked(a.position, leftofrecepient);
                bool B = lineofsightblocked(a.position, rightofrecipient);
                bool C = lineofsightblocked(a.position, topofrecipient - a.transform.forward * 0.3f);
                bool D = lineofsightblocked(a.position, bottomofrecipient - a.transform.forward * 0.3f);
                anylinesofisght = !A || !B || !C || !D || !E;
            }
            return anylinesofisght;
        }

        bool lineofsightblocked(Vector3 one ,Vector3 two)
        {
        return Physics.Linecast(one, two, walls);
         }
        bool ObservedY()
       {
        float eypos = b.position.y;
        float bottom = eypos - 1.5f;
        float top = eypos + 1.5f;
        float dif = findDif(a.position, b.position);
        float topdif = (top-a.position.y);
        float topangle= Mathf.Rad2Deg * Mathf.Atan(topdif / dif);
        float angle = a.transform.eulerAngles.x;
        
        if (angle <= 360 && angle >= 270)
        {
            angle = ((270 - angle) + 90);

        }
        else
        {
            angle = -angle;
        }
        if (Mathf.Abs(angle - topangle) < maxangley + (8/dif))
        {
            return true;
        }
        float botdif = (a.position.y-bottom);
        float botangle =-( Mathf.Rad2Deg * Mathf.Atan(botdif / dif));
        if (Mathf.Abs(angle - botangle) < maxangley + (8 / dif))
        {
            return true;
        }
        
        return false;

    }

        bool ObservedX()
        {
        Vector2 pp = new Vector2(a.position.x, a.position.z);
        Vector2 ep = new Vector2(b.position.x, b.position.z);
        Vector2 dahead = new Vector2(a.forward.x, a.forward.z);

        dahead = normaliser(dahead);
        pp = normaliser(ep - pp);
        float dotProduct = (dahead.x * pp.x + dahead.y * pp.y);
        float angle = Mathf.Rad2Deg * Mathf.Acos(dotProduct);
        float dif= (findDif(a.position, b.position));
        if (angle < maxanglex + (20/dif))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
        return PathClear() && ObservedX() && ObservedY();
    }

    public Vector3 halfway(Vector3 A, Vector3 B)
    {
        float newX = (A.x + B.x) / 2;
        float newY = (A.y + B.y) / 2;
        float newZ = (A.z + B.z) / 2;
        return new Vector3 (newX, newY, newZ);
    }


}
