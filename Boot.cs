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
            xDoc.Load("configs.xml");

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

        int animFrames = 0;
        int nextFrameDataOffset = 0;

        int currentAnimFrame = 0;
        int frameDelay = 8;
        int frameCounter = 0;  

        Raylib.InitWindow(_width, _height, "adfgf");
        Raylib.SetTargetFPS(70);
        if (_fullscreen && !Raylib.IsWindowFullscreen())
        {
            Raylib.ToggleFullscreen();
        }

        Texture2D startupicon = Raylib.LoadTexture("@win.bmp");

        Image animImage = Raylib.LoadImageAnim("@sys_preloading.gif", ref animFrames);
        Texture2D tex = Raylib.LoadTextureFromImage(animImage);

        double startTime = Raylib.GetTime();

        while (!Raylib.WindowShouldClose() && (Raylib.GetTime() - startTime < 8.0))
        {
            frameCounter++;
            if (frameCounter >= frameDelay)
            {
                currentAnimFrame++;
                if (currentAnimFrame >= animFrames) currentAnimFrame = 0;

                nextFrameDataOffset = animImage.Width * animImage.Height * 4 * currentAnimFrame;
                IntPtr framePtr = IntPtr.Add(animImage.Data, nextFrameDataOffset);
                Raylib.UpdateTexture(tex, framePtr);

                frameCounter = 0;
            }

            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.Black);

            Raylib.DrawTexture(startupicon, _width / 2 - startupicon.Width / 2, _height / 3 - startupicon.Height, Color.RayWhite);
            Raylib.DrawTexture(tex, _width / 2 - tex.Width / 2, _height / 2, Color.RayWhite);

            Raylib.EndDrawing();
        }

        Raylib.UnloadTexture(startupicon);
        Raylib.UnloadTexture(tex);
        Raylib.UnloadImage(animImage);

        Raylib.CloseWindow();
        
        Desktop.Init();
        Desktop.Show();
    }
}