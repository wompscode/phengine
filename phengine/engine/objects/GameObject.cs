using phengine.engine.components;
using phengine.engine.math;

namespace phengine.engine.objects;

public class GameObject
{
    private readonly phengine_Canvas _canvasInstance;
    public Vector2 position = new Vector2(0,0);
    public Vector2 scale = new Vector2(1,1);
    public readonly List<Component> Components = new List<Component>();

    public GameObject(phengine_Canvas canvas, Vector2 position, Vector2 scale)
    {
        _canvasInstance = canvas;
        this.position = position;
        this.scale = scale;
    }

    public void Destroy()
    {
        _canvasInstance.Hierarchy.Remove(this);
    }
}