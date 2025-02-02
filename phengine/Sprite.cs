namespace phengine.engine.components;

public class Sprite : Component
{
    public enum spriteType
    {
        primitive,
        image
    }
    
    public spriteType type { get; set; }
    public Color colour { get; set; } = Color.Red;
    public Sprite()
    {
        runWithRender = true;
    }
}