namespace phengine.engine.components;

public class TextRenderer : Component
{
    public string Text { get; set; }
    public Font Font { get; set; }
    public Color Color { get; set; }
    public TextRenderer()
    {
        runWithRender = true;
    }
}