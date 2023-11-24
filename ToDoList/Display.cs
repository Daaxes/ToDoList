// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Read Back");
//Console.WriteLine(fileOutput.ReadFromFile(projectFile));

class Display
{
    // Constructors
    // Default constructor
    public Display()   
    {
    }

    public void ClearLine(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
        Console.Write(new String(' ', Console.BufferWidth));
    }

    public void ClearOutputOnScreen()
    {
        Console.Clear();
    }

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
        ShowMenu(ConsoleColor.Green, ">> Make your choise",0 ,7);
        Console.ResetColor();
    }

    //  witout  clear line first Writing out a string at specific position
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
    // Writing out a string at specific position witout  clear line first 
    public void ShowString(ConsoleColor menuColor, string str, int posX, int posY)
    { 
        Console.ForegroundColor = menuColor;
        SetCursurPos(posX, posY);
        Console.Write(str);
    }
}
