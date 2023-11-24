// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

// Define console colors
const ConsoleColor darkYelloe = ConsoleColor.DarkYellow;
const ConsoleColor red = ConsoleColor.Red;
const ConsoleColor green = ConsoleColor.Green;
const ConsoleColor blue = ConsoleColor.Blue;
const ConsoleColor yellow = ConsoleColor.Yellow;
const int milliseconds = 2000;

Display display = new Display();

bool checkDate(string date)
{
    try
    {
        DateTime dateNow = DateTime.Now.Date;
        DateTime toDoDate = Convert.ToDateTime(date.ToString()).Date;

        int result = DateTime.Compare(dateNow, toDoDate);
        if (result > 0)
        {
            Console.WriteLine("Date cant be in past | Try again!");
            return false;
        }
    }
    catch (FormatException e)
    {
        Console.WriteLine("Wrong format of Date | Try again!", 0, 2);
        return false;
    }

    return true;
}


// Function to create user
// return Tuple with List<User> and bool
Tuple<List<User>, bool> CreateUsers(List<User> userList)
{
    int userId = 1;
    int listLen = 0;

    if (userList.Count() > 0)
    {
        userId = userList.Max(User => User.UserID) + 1;
    }

    display.ShowMenu(green, "Enter Username:", 0, 2);
    string username = Console.ReadLine();

    if (username.ToUpper().Equals("Q"))
    {
        return Tuple.Create(userList, false);
    }

    display.ShowMenu(green, "Enter Password:", 0, 2);
    string password = Console.ReadLine();
 
    if (password.ToUpper().Equals("Q"))
    {
        return Tuple.Create(userList, false);
    }

    listLen = userList.Count();

    userList.Add(new User(username, password, userId));

    if (listLen < userList.Count())
    {
        display.ShowMenu(green, "Product added successfully", 0, 3);
        Thread.Sleep(milliseconds);
        display.ClearLine(0, 3);
    }
    else
    {
        display.ShowMenu(red, "Somting went wrong", 0, 3);
        Thread.Sleep(milliseconds);
        display.ClearLine(0, 3);
    }
    return Tuple.Create(userList, true);
}

string dir = System.IO.Directory.GetCurrentDirectory();
string userFile = dir.Split("\\bin")[0] + "\\users.csv";
string projectFile = dir.Split("\\bin")[0] + "\\project.csv";
Console.WriteLine("ToDo List! ");
Console.WriteLine($"Userpath: {userFile} \nProjectPath: {projectFile}");

Project project = new Project();
User user = new User();
StringBuilder csv = new StringBuilder();
List<string> inputList = new List<string>();
List<string> userListString = new List<string>();
List<Project> projectList = new List<Project>();
List<User> userList = new List<User>();

userList = user.ReadFromFile(userFile);
user.Show(userList, 0, 4);

if (userList.Count() == 0)
{
    display.ShowMenu(red, "UserList is empty", 0, 0);
    display.ShowMenu(red, "You must have at least i user in ToDo List!", 0, 1);
    Thread.Sleep(milliseconds);
    display.ShowMenu(green, "Create users | Press [Q] to Quit", 0, 0);
    display.ClearLine(0, 1);
    display.ClearLine(0, 2);

    user.Show(userList, 0, 4);

    while (true)
    {
        Tuple< List<User>, bool> myTuple = CreateUsers(userList);
        userList = myTuple.Item1;

        if (!myTuple.Item2)
        {
            user.Show(userList, 0, 4);
            display.ShowMenu(green, "Do you want to save userlist to file? [Y/N]", 0, 2);
            string choice = Console.ReadLine();
            if (choice.ToUpper().Equals("Y"))
            {
                user.SaveToFile(userList, userFile);
            }

        }
    }
}



int flag = 0;

    string[] messageStr = new string[] {"Skriv in en task", "Skriv in ett datum yy-MM-dd", "Skriv in beskrivning", "Skriv in vem som utför tasken"};

//Console.WriteLine("Klicka på [Q] för exit");

while (true)
{
    Console.WriteLine(messageStr[flag]);
    inputList.Add(Console.ReadLine());
 
    if (inputList[flag].ToString().ToUpper().Equals("Q"))
    {
        project.PrintToFile(projectList, projectFile);

        Console.WriteLine("Are you sure that you want to exit? [Y/N]");
        inputList.Add(Console.ReadLine());
        if (inputList[flag].ToString().ToUpper().Equals("Y"))
        { break; }
    }

    if (messageStr[flag].ToString().ToLower().Contains("datum"))
    {
        if (!checkDate(inputList[flag]))
        {
            inputList.RemoveAt(flag);
        }
        else
        {
            flag++;
        }
    }
    else
    {
        flag++;
    }

    if (flag == messageStr.Length)
    { 
        int listSize = projectList.Count;
        projectList.Add(new Project(inputList[0], Convert.ToDateTime(inputList[1]), inputList[2], inputList[3]));
        if (listSize < projectList.Count)
        {
            Console.WriteLine("Product added successfully");
            Thread.Sleep(milliseconds);
            flag = 0;
            inputList.Clear();
        }
        else 
        {
            Console.WriteLine("Something went wrong when it should be added to projectList!");
        }
    }
}

class Rating : Project

{ 

}

class Details : Project
{ 

}
