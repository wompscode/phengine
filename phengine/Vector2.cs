namespace phengine.engine.math;

public class Vector2
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Vector2()
    {
        X = 0;
        Y = 0;
    }

    public static Vector2 Zero()
    {
        return new Vector2(0, 0);
    }
}