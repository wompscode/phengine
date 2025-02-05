using phengine.engine;
using phengine.engine.components;
using phengine.engine.math;
using phengine.engine.objects;
using TextRenderer = phengine.engine.components.TextRenderer;

namespace phengine;

class Demo : Engine
{
    private GameObject player;
    private GameObject text;
    private bool intersectSwitch = false;
    public Demo() : base(new Vector2(512, 512), "phengine demo", Color.White)
    {
    }
    protected override void Load()
    {
        Console.WriteLine("Load(): hit");
        
        player = canvas.CreateObject(new Vector2(15,15), new Vector2(10,10));
        player.name = "Player";
        Sprite sprite = new Sprite
        {
            type = Sprite.spriteType.image,
            colour = Color.MediumPurple
        };
        sprite.LoadImage(@"../../../demo_sprite.png");
        player.scale = new Vector2(sprite.image.Width, sprite.image.Height);
        player.Components.Add(sprite);

        GameObject test = canvas.CreateObject(new Vector2(64,64), new Vector2(32,32));
        Sprite testSprite = new Sprite
        {
            type = Sprite.spriteType.primitive,
            colour = Color.MediumPurple
        };
        test.Components.Add(testSprite);
        test.OnIntersectStart += o =>
        {
            MessageBox("test", true);
            intersectSwitch = true;
            test.Destroy();
            Console.WriteLine($"{o.name}: {o.uuid} - start");
        };

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
            if (intersectSwitch == false) return;
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            SetBackgroundColour(randomColor);
        }


        if (obj == Keys.M)
        {
            GameObject newObj = canvas.CreateObject(new Vector2(player.position.X, player.position.Y), new Vector2(rnd.Next(32), rnd.Next(32)));
            Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            Sprite newSprite = new Sprite
            {
                type = Sprite.spriteType.primitive,
                colour = randomColor
            };
            newObj.Components.Add(newSprite);
            newObj.OnIntersectStart += o =>
            {
                Console.WriteLine($"{newObj.uuid}: {o.name}");
            };
        }
        
        if (obj == Keys.A)
        {
            player.scale.X -= 4;
        }
        if (obj == Keys.D)
        {
            player.scale.X += 4;
        }
        if (obj == Keys.W)
        {
            player.scale.Y -= 4;
        }
        if (obj == Keys.S)
        {
            player.scale.Y += 4;
        }

        if (obj == Keys.R)
        {
            if (player.Components[0] is Sprite sprite)
            {
                player.scale = new Vector2(sprite.image.Width, sprite.image.Height);
            }
        }

        if (obj == Keys.Escape)
        {
            canvas.Close();
        }
    }
    private int frame = 0;

    protected override void Render()
    {
    }

    private float fps;
    private float dt;
    private int frameC;
    private Random rnd = new ();
    protected override void Update()
    {
        frame++;

        frameC++;
        dt += deltaTime;
        if (dt > 1.0 / 4f)
        {
            fps = (float)Math.Round(frameC / dt);
            frameC = 0;
            dt -= 1.0f / 4f;
        }
        
        if (IsKeyDown(Keys.Left))
        {
            player.position.X -= 5;
        }
        if (IsKeyDown(Keys.Right))
        {
            player.position.X += 5;
        }
        if (IsKeyDown(Keys.Up))
        {
            player.position.Y -= 5;
        }
        if (IsKeyDown(Keys.Down))
        {
            player.position.Y += 5;
        }
        
        bool outcome = SetTitle($"{ScreenTitle}: fps{fps}");

        if (text.Components.Count > 0 && text.Components[0] is TextRenderer textRenderer)
        {
            textRenderer.Text = $"phengine demo: pos({player.position.X}, {player.position.Y}), sca({player.scale.X}, {player.scale.Y})";
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