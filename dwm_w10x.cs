using Raylib_cs;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Xml;

namespace dwm_win10x;

public static class dwm_main
{   
    private static int _width = 1280;
    private static int _height = 720;
    private static bool _mode = false;
    private static int _winwidth = 0;

    public static void draw_window()
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
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка загрузки XML: {ex.Message}");
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Right))
        {
            _mode = true;
        }
        if (Raylib.IsKeyPressed(KeyboardKey.Left))
        {
            _mode = false;
        }
        Rectangle win_karcas = new Rectangle(0, 0, _winwidth, _height);
        Raylib.DrawRectangleRec(win_karcas, Color.White);
    }

    public static void stop_window()
    {
        _mode = false;
        _winwidth = 0;
        return;
    }
}