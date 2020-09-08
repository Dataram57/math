using UnityEngine;

public class Cross2D
{
    //Vector2.positiveInfinity      -- they lay on themselves
    //Vector2.negativeInfinity      -- nowhere

    public static Vector2 CrossLines(Vector2 x,Vector2 y, Vector2 z, Vector2 w)
    {
        Vector2 Delta1 = x - y;
        Vector2 Delta2 = z - w;
        float a1=0f;
        float a2 = 0f;
        float b1 = 0f;
        float b2 = 0f;
        bool straight1=false;
        bool straight2=false;

        //straight line
        if (Delta1.x != 0)
        {
            a1 = Delta1.y / Delta1.x;
            b1 = x.y - a1 * x.x;
        }
        else straight1 = true;

        if (Delta2.x != 0)
        {
             a2 = Delta2.y / Delta2.x;
             b2 = z.y - a2 * z.x;
        }
        else straight2 = true;

        Vector2 cross = new Vector2();

        if (straight1 && straight2)
        {
            if (x.x == z.x) return Vector2.positiveInfinity;
            else return Vector2.negativeInfinity;
        }
        else if (straight1)
        {
            cross.x = x.x;
            cross.y = x.x * a2 + b2;
        }
        else if (straight2)
        {
            cross.x = z.x;
            cross.y = z.x * a1 + b1;
        }
        else
        {
            cross.x = (b2 - b1) / (a1 - a2);
            cross.y = a1 * cross.x + b1;
        }


        return cross;
    }

    public static Vector2 CrossLineSegments(Vector2 x, Vector2 y, Vector2 z, Vector2 w)
    {
        Vector2 cross=CrossLines(x, y, z, w);
        if (IsPointInTheBox(x, y, cross) && IsPointInTheBox(z, w, cross)) return cross;
        if (cross == Vector2.positiveInfinity) return Vector2.positiveInfinity;
        return Vector2.negativeInfinity;
    }

    public static bool IsPointInTheBox(Vector2 x, Vector2 y, Vector2 cross)
    {
        //noice
        return ((x.y > cross.y && cross.y > y.y || x.y < cross.y && cross.y < y.y) && (x.x < cross.x && cross.x < y.x || x.x > cross.x && cross.x > y.x));
    }
}
