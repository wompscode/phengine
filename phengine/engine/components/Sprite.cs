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
    public Image image { get; set; }

    public bool LoadImage(string fileLoc)
    {
        if (type != spriteType.image) return false;
        try
        {
            image = Image.FromFile(fileLoc);
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"SetBackgroundColour(): {exception.Message}\n{exception.StackTrace}");
            return false;
        }
    }
    public Sprite()
    {
        runWithRender = true;
    }
}