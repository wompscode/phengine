using System.ComponentModel;
using phengine.engine.math;
using phengine.engine.components;
using phengine.engine.objects;
using Component = phengine.engine.components.Component;
using TextRenderer = phengine.engine.components.TextRenderer;

namespace phengine.engine;

static class phengine_Version
{
    public static string version = "0.0.1";
}

public class phengine_Canvas : Form
{
    public phengine_Canvas(Engine engine, Size size, string title)
    {
        DoubleBuffered = true;
        Size = size;
        Text = title;
    }
    
    public readonly List<GameObject> Hierarchy = new List<GameObject>();
    public Color Background = Color.White;

    public GameObject CreateObject(Vector2 position, Vector2 scale)
    {
        GameObject obj = new GameObject(this, position, scale);
        Hierarchy.Add(obj);
        return obj;
    }
}

public abstract class Engine
{
    static Engine _engineInstance;
    private bool init;
    public Vector2 screenSize = new Vector2(512, 512);
    public string screenTitle = "phengine";
    public readonly phengine_Canvas canvas;
    
    public Engine(Vector2 screenSize, string screenTitle = "phengine", Color background = default)
    {
        Console.WriteLine($"welcome to phengine v{phengine_Version.version}");
        _engineInstance = this;
        this.screenSize = screenSize;
        if (screenTitle == "phengine")
        {
            this.screenTitle = $"phengine v{phengine_Version.version}";            
        }
        else
        {
            this.screenTitle = screenTitle;
        }
        
        canvas = new phengine_Canvas(this, new Size((int)this.screenSize.X, (int)this.screenSize.Y), this.screenTitle);
        canvas.Background = background;
        canvas.Closing += InternalClosing;
        canvas.Paint += InternalRender;
        canvas.Load += (sender, args) =>
        {
            init = true;
        };
        canvas.KeyDown += InternvalOnKeyDown;
        canvas.KeyUp += InternvalOnKeyUp;
        Console.WriteLine($"created canvas with title {screenTitle} and size {screenSize.X}, {screenSize.Y}");
        
        _engineThread = new Thread(EngineUpdate);
        _engineThreadRunning = true;
        _engineThread.Start();

        Load();
    }

    private readonly List<Keys> lockKeys = new List<Keys>();
    private void InternvalOnKeyUp(object? sender, KeyEventArgs e)
    {
        if(lockKeys.Contains(e.KeyCode)) lockKeys.Remove(e.KeyCode);
        KeyUp?.Invoke(e.KeyCode);
    }

    private void InternvalOnKeyDown(object? sender, KeyEventArgs e)
    {
        KeyPress?.Invoke(e.KeyCode);
        if (lockKeys.Contains(e.KeyCode)) return;
        lockKeys.Add(e.KeyCode);
        KeyDown?.Invoke(e.KeyCode);
    }

    public void InternalClosing(object? sender, CancelEventArgs e)
    {
        Console.WriteLine("goodbye phengine!");
        init = false;
        _engineThreadRunning = false;
        Closing();
    }

    private readonly Thread _engineThread;
    private bool _engineThreadRunning;
    private void EngineUpdate()
    {
        while (_engineThreadRunning)
        {
            if (init == false) continue;
            Render();
            canvas.BeginInvoke((MethodInvoker)delegate { canvas.Refresh(); });
            Update();
            Thread.Sleep(1);
        }
    }

    public bool IsKeyDown(Keys key)
    {
        return lockKeys.Contains(key);
    }
    
    private void InternalRender(object? sender, PaintEventArgs e)
    {
        Graphics graphics = e.Graphics;
        graphics.Clear(canvas.Background);
        foreach (var gameObject in canvas.Hierarchy)
        {
            List<Component> components = gameObject.Components;
            foreach (Component component in components)
            {
                if (component.runWithRender == false) continue;

                if (component is Sprite sprite)
                {
                    if (sprite.type == Sprite.spriteType.image)
                    {
                    }
                    else if(sprite.type == Sprite.spriteType.primitive)
                    {
                        graphics.FillRectangle(new SolidBrush(sprite.colour), gameObject.position.X, gameObject.position.Y, gameObject.scale.X, gameObject.scale.Y);
                    }
                } else if (component is TextRenderer text)
                {
                    graphics.DrawString(text.Text, text.Font, new SolidBrush(text.Color), gameObject.position.X, gameObject.position.Y);
                }
            }

        }
    }

    public bool SetBackgroundColour(Color colour)
    {
        try
        {            
            canvas.BeginInvoke((MethodInvoker)delegate { canvas.Background = colour; });
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"SetBackgroundColour(): {exception.Message}\n{exception.StackTrace}");
            return false;
        }
    }
    public bool SetTitle(string text)
    {
        try
        {
            canvas.BeginInvoke((MethodInvoker)delegate { canvas.Text = text; });
            return true;
        }
        catch (Exception exception)
        {
            Console.WriteLine($"SetTitle(): {exception.Message}\n{exception.StackTrace}");
            return false;
        }
    }
    protected virtual void Run()
    {
        Application.Run(canvas);
    }

    protected abstract void Load();
    protected abstract void Render();
    protected abstract void Update();
    protected abstract void Closing();
    
    public Action<Keys> KeyDown;
    public Action<Keys> KeyUp;
    public Action<Keys> KeyPress;
}