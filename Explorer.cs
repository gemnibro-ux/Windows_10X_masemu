using Raylib_cs;
using System.Numerics;
using System.Xml;
using dwm_win10x;

namespace Explorer;

public static class Desktop
{
    private static int _width = 1280;
    private static int _height = 720;
    private static bool _fullscreen = false;
    private static string _background = "blue.png";
    private static Color _color = new Color(235, 235, 235, 255);

    public static Vector2 point = new Vector2();

    public static void Init()
    {
        try
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("config.xml");

            XmlNode? windowNode = xDoc.SelectSingleNode("//Settings/Window");
            if (windowNode != null && windowNode.Attributes != null)
            {
                _width = int.Parse(windowNode.Attributes["width"]?.Value ?? "1280");
                _height = int.Parse(windowNode.Attributes["height"]?.Value ?? "720");
                _fullscreen = bool.Parse(windowNode.Attributes["fullscreen"]?.Value ?? "false");
            }

            XmlNode? themeNode = xDoc.SelectSingleNode("//Settings/Theme");
            if (themeNode != null && themeNode.Attributes != null)
            {
                _background = themeNode.Attributes["background"]?.Value ?? "blur.png";
                _color = new Color(byte.Parse(themeNode.Attributes["colorR"]?.Value ?? "237"), byte.Parse(themeNode.Attributes["colorG"]?.Value ?? "237"), byte.Parse(themeNode.Attributes["colorB"]?.Value ?? "237"), byte.Parse(themeNode.Attributes["colorA"]?.Value ?? "255"));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки XML: {ex.Message}");
        }
    }

    public static void Show()
    {
        Raylib.InitWindow(_width, _height, "Simulator Windows 10X");
        Raylib.SetTargetFPS(60);

        Console.WriteLine($"{_background} - это значения обоя");
        Texture2D bg = Raylib.LoadTexture($"disk/backgrounds/{_background}");
        Texture2D clock = Raylib.LoadTexture("disk/appsicons/2.png");
        Rectangle clock_hitbox = new Rectangle(30, 30, clock.Width, clock.Height);
        Texture2D calculator = Raylib.LoadTexture("disk/appsicons/5.png");
        Texture2D start = Raylib.LoadTexture("disk/appsicons/67.png");
        start.Width = 42;
        start.Height = 54;
        Rectangle bg_hitbox = new Rectangle(0, 0, bg.Width, bg.Height);
        Rectangle start_hitbox = new Rectangle(_width / 2 - start.Width / 2, _height - start.Height + 16, start.Width, start.Height);
        Rectangle choice = new Rectangle(0, 0, 0, 0);
        bool a = false;
        bool b = false;
        bool c = false;

        while (!Raylib.WindowShouldClose())
        {
            point = Raylib.GetMousePosition();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(_color);
            Raylib.DrawRectangleRec(bg_hitbox, Color.RayWhite);
            Raylib.DrawTexture(bg, 0, 0, Color.White);
            if (Raylib.IsMouseButtonPressed(MouseButton.Right) && Raylib.CheckCollisionPointRec(point, bg_hitbox))
            {
                a = true;
            }

            Raylib.DrawRectangle(0, _height - 38, _width, 38, Color.White);
            if (Raylib.IsMouseButtonDown(MouseButton.Left))
            {
                float _choiceWidth;
                float _choiceHeight;
                choice.Width = 0; choice.Height = 0;
                if (Raylib.IsMouseButtonPressed(MouseButton.Left))
                {
                    choice.X = point.X; choice.Y = point.Y;
                }
                _choiceWidth = point.X - choice.X;
                _choiceHeight = point.Y - choice.Y;
                choice.Width = _choiceWidth; choice.Height = _choiceHeight; 
                Raylib.DrawRectangleRec(choice, Color.Blue);
            }

            Raylib.DrawTexture(clock, 30, 30, Color.White);
            Raylib.DrawRectangleRec(clock_hitbox, new Color(0, 0, 0, 0));
            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && Raylib.CheckCollisionPointRec(point, clock_hitbox))
            {
                c = true;
            }

            Raylib.DrawTexture(calculator, 30, 80, Color.White);
            
            Raylib.DrawTexture(start, _width / 2 - start.Width / 2, _height - start.Height + 16, Color.White);
            Raylib.DrawRectangleRec(start_hitbox, new Color(0, 0, 0, 0));
            if (Raylib.IsMouseButtonPressed(MouseButton.Left) && Raylib.CheckCollisionPointRec(point, start_hitbox))
            {
                b = true;
            }

            if (a)
            {
                Raylib.DrawRectangle((int)point.X, (int)point.Y, 300, 200, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Left) || Raylib.IsKeyPressed(KeyboardKey.LeftShift))
                {
                    a = false;
                }
            }
            if (b)
            {
                Raylib.DrawRectangle(_width / 2 - 400 / 2, _height - 620, 400, 560, Color.White);
                if (Raylib.IsMouseButtonPressed(MouseButton.Right))
                {  
                    b = false;
                }
            }
            if (c)
            {
                dwm_main.draw_window();
                if (Raylib.IsKeyPressed(KeyboardKey.End))
                {
                    c = false;
                    dwm_main.stop_window();
                }
            }

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(bg);
        Raylib.CloseWindow();
    }
}