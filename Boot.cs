using Raylib_cs;
using System.Xml;
using Explorer;

namespace Boot;

public static class Sysboot
{   
    private static int _width = 1280;
    private static int _height = 720;
    private static bool _fullscreen = false;

    public static void Main()
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
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки XML: {ex.Message}");
        }


        Raylib.InitWindow(_width, _height, "Windows Boot Manager");
        Raylib.SetTargetFPS(70);
        if (_fullscreen && !Raylib.IsWindowFullscreen())
        {
            Raylib.ToggleFullscreen();
        }

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
        Texture2D startupicon = Raylib.LoadTexture("win.png");
        Raylib.DrawTexture(startupicon, _width / 2 - startupicon.Width / 2, _height / 3 - startupicon.Height, Color.White);
        Raylib.EndDrawing();

        Raylib.WaitTime(3);

        Raylib.UnloadTexture(startupicon);

        Raylib.CloseWindow();
        
        Desktop.Init();
        Desktop.Show();
    }
}