// Class to handle console display and interactions
class Display
{
    // Constructors
    // Default constructor
    public Display()
    {
    }
    // Method to clear a specific line on the console
    public void ClearLine(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
        Console.Write(new String(' ', Console.BufferWidth));
    }
    // Method to clear the entire console screen
    public void ClearOutputOnScreen()
    {
        Console.Clear();
    }
    // Method to clear specified lines on the console
    public void ClearOutputScreenFromPosY(int posY, int lines)
    {
        for (int i = posY; i < lines + 1 + posY; i++)
        {
            ClearLine(0, i);
        }
    }

    // Metods for set the positions
    public void SetCursurPos(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
    }
    // Method to display category information
    public void ShowCategory(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(">> Welcome to ToDo List");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(">> You have [  ] task to do [  ] are done! Great job");
        Console.WriteLine(">> [1] List Project:");
        Console.WriteLine(">> [2] Add new User:");
        Console.WriteLine(">> [3] Edit Task:");
        Console.WriteLine(">> [Q] Save and Quit:");
        ShowMenu(ConsoleColor.Green, ">> Make your choise", 0, 7);
        Console.ResetColor();
    }

    // Method to display a menu item at a specific position
    public void ShowMenu(ConsoleColor menuColor, string menuText, int posX, int posY)
    {
        int len = menuText.Length;

        ClearLine(posX, posY);
        Console.ForegroundColor = menuColor;
        SetCursurPos(posX, posY);
        Console.WriteLine(menuText);
        Console.ResetColor();
        SetCursurPos(len + 1, posY);
    }
    // Method to display a string at a specific position without clearing the line first
    public void ShowString(ConsoleColor menuColor, string str, int posX, int posY)
    {
        Console.ForegroundColor = menuColor;
        SetCursurPos(posX, posY);
        Console.Write(str);
    }
}
