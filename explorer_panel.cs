using Raylib_cs;
using Explorer;

namespace fast_menu;

public static class Panel
{
    public static Rectangle option1 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 10, 300, 16);
    public static Rectangle option2 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 26, 300, 16);
    public static Rectangle option3 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 42, 300, 16);
    public static Rectangle option4 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 56, 300, 16);
    public static Rectangle option5 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 72, 300, 16);
    public static Rectangle option6 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 86, 300, 16);
    public static Rectangle option7 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 102, 300, 16);
    public static Rectangle option8 =  new Rectangle((int)Desktop.point.X + 11, (int)Desktop.point.Y + 118, 300, 16);
    private static Color nul_col = new Color(0, 0, 0, 0);
    public static void draw_panel()
    {
        Raylib.DrawRectangleRec(option1, nul_col);
        Raylib.DrawRectangleRec(option2, nul_col);
        Raylib.DrawRectangleRec(option3, nul_col);
        Raylib.DrawRectangleRec(option4, nul_col);
        Raylib.DrawRectangleRec(option5, nul_col);
        Raylib.DrawRectangleRec(option6, nul_col);
        Raylib.DrawRectangleRec(option7, nul_col);
        Raylib.DrawRectangleRec(option8, nul_col);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left) && Raylib.CheckCollisionPointRec(Desktop.point, option1))
        {
            
        }
    }
}