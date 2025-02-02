using System.Runtime.CompilerServices;
using phengine.engine;
using phengine.engine.components;
using phengine.engine.math;
using phengine.engine.objects;
using TextRenderer = phengine.engine.components.TextRenderer;

namespace phengine;

class Demo : Engine
{
    private GameObject player = null;
    private GameObject text = null;
    public Demo() : base(new Vector2(512, 512), "phengine demo", Color.White)
    {
    }
    protected override void Load()
    {
        Console.WriteLine("Load(): hit");

        
        player = canvas.CreateObject(new Vector2(15,15), new Vector2(10,10));
        Sprite sprite = new Sprite
        {
            type = Sprite.spriteType.primitive,
            colour = Color.MediumPurple
        };
        player.Components.Add(sprite);

        text = canvas.CreateObject(new Vector2(1, 1), new Vector2(5, 5));
        TextRenderer textRenderer = new TextRenderer
        {
            Color = Color.Black,
            Font = new Font(FontFamily.GenericSansSerif, 10.0f),
            Text = "phengine demo"
        };
        text.Components.Add(textRenderer);
        
        KeyPress += KeyPress_Action;
        Run();
    }
    
    private void KeyPress_Action(Keys obj)
    {
        if (obj == Keys.C)
        {
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            SetBackgroundColour(randomColor);
        }
    }

    protected override void Render()
    {
        //Console.WriteLine("Render(): hit");
    }

    private int frame = 0;
    private Random rnd = new Random();
    protected override void Update()
    {
        frame++;

        bool isLeftDown = IsKeyDown(Keys.Left);
        bool isRightDown = IsKeyDown(Keys.Right);
        bool isUpDown = IsKeyDown(Keys.Up);
        bool isDownDown = IsKeyDown(Keys.Down);
        if (isLeftDown)
        {
            player.position.X -= 5;
        }
        if (isRightDown)
        {
            player.position.X += 5;
        }
        if (isUpDown)
        {
            player.position.Y -= 5;
        }
        if (isDownDown)
        {
            player.position.Y += 5;
        }
        bool outcome = SetTitle($"{screenTitle}: frame {frame}");

        if (text.Components.Count > 0 && text.Components[0] is TextRenderer textRenderer)
        {
            textRenderer.Text = $"phengine demo: {player.position.X}, {player.position.Y}";
        }

    }

    protected override void Closing()
    {
        Console.WriteLine("Closing(): hit");
    }

    protected override void Run()
    {
        Console.WriteLine("Run(): hit");
        base.Run();
    }
}