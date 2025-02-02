using phengine.engine.components;
using phengine.engine.math;

namespace phengine.engine.objects;

public class GameObject
{
    public string name { get; set; } = "GameObject";
    public string uuid { get; set; }
    
    private readonly phengine_Canvas _canvasInstance;
    public Vector2 position = new Vector2(0,0);
    public Vector2 scale = new Vector2(1,1);
    public readonly List<Component> Components = new List<Component>();
    public Rectangle rect;
    public GameObject(phengine_Canvas canvas, Vector2 position, Vector2 scale)
    {
        _canvasInstance = canvas;

        bool generated = false;
        while (generated == false)
        {
            Guid uuid = Guid.NewGuid();
            if (canvas.Hierarchy.FirstOrDefault(i => i.uuid == uuid.ToString()) == null)
            {
                this.uuid = uuid.ToString();
                generated = true;
            }
        }
        
        this.position = position;
        this.scale = scale;
        rect = new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y);
    }

    public Action<GameObject> OnIntersectStart;
    public Action<GameObject> OnIntersectEnd;
    
    public void Update()
    {
        rect = new Rectangle((int)position.X, (int)position.Y, (int)scale.X, (int)scale.Y);
    }
    public void Destroy()
    {
        _canvasInstance.Hierarchy.Remove(this);
    }
}