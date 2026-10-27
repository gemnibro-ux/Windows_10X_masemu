using Raylib_cs;
using System.Xml;

namespace Explorer;

public static class Desktop
{
    private static int _width = 1280;
    private static int _height = 720;
    private static bool _fullscreen = false;
    private static string _background = "blur.jpg";
    private static Color _color = new Color(235, 235, 235, 255);

    public static void Init()
    {
        try
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load("disk/configs.xml");

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
                _background = themeNode.Attributes["background"]?.Value ?? "blur.jpg";
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

        if (_fullscreen && !Raylib.IsWindowFullscreen())
        {
            Raylib.ToggleFullscreen();
        }

        Texture2D background_texture = Raylib.LoadTexture($"C:/gemnibro/win10x/backgrounds/{_background}");

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.DrawTexture(background_texture, 0, 0, Color.RayWhite);
            Raylib.DrawRectangle(0, _height - 38, _width, 38, _color);


            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(background_texture);
        Raylib.CloseWindow();
    }
}
