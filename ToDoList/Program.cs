// See https://aka.ms/new-console-template for more information
using System.Text;

// Define console colors and milliseconds
const ConsoleColor darkYelloe = ConsoleColor.DarkYellow;
const ConsoleColor red = ConsoleColor.Red;
const ConsoleColor green = ConsoleColor.Green;
const ConsoleColor blue = ConsoleColor.Blue;
const ConsoleColor yellow = ConsoleColor.Yellow;
const int milliseconds = 2000;

// Create instances of classes
Project project = new Project();
User user = new User();
Display display = new Display();

// Get the current directory and set file paths
string dir = System.IO.Directory.GetCurrentDirectory();
user.FilePath = dir.Split("\\bin")[0] + "\\users.csv";
project.FilePath = dir.Split("\\bin")[0] + "\\project.csv";

// Creates List of Users, Project, Strings
List<User> userList = new List<User>();
List<string> inputList = new List<string>();
List<string> userListString = new List<string>();
List<Project> projectList = new List<Project>();
StringBuilder input = new StringBuilder();

// Read in Users and Projects from file to list
userList = user.ReadFromFile();
projectList = project.ReadFromFile();

// Function to create user
// return Tuple with List<User> and bool value
Tuple<List<User>, bool> CreateUsers(List<User> userList)
{
    int userId = 1;
    int listLen = 0;

    display.ShowString(green, " ", 4, 3);
    display.ShowString(green, " ", 4, 4);
    display.ShowString(green, " ", 4, 5);

    if (userList.Count() > 0)
    {
        userId = userList.Max(User => User.UserID) + 1;
    }

    display.ShowMenu(green, "Enter Username:", 0, 9);
    string username = Console.ReadLine();

    if (username.ToUpper().Equals("Q"))
    {
        return Tuple.Create(userList, false);
    }

    display.ShowMenu(green, "Enter Password:", 0, 9);
    string password = Console.ReadLine();

    if (password.ToUpper().Equals("Q"))
    {
        return Tuple.Create(userList, false);
    }

    // Checks if user allready exists
    if (!user.ValidateUser(username, "") || userList.Any(User => User.Username == username))
    {
        listLen = userList.Count();
        userList.Add(new User(username, password, userId));

        if (listLen < userList.Count())
        {
            display.ShowMenu(green, "User added successfully", 0, 10);
            Thread.Sleep(milliseconds);
            display.ClearLine(0, 10);
        }
        else
        {
            display.ShowMenu(red, "Somting went wrong", 0, 10);
            Thread.Sleep(milliseconds);
            display.ClearLine(0, 10);
        }
    }
    else
    {
        display.ShowMenu(red, "User already exist | Try again1", 0, 10);
        Thread.Sleep(milliseconds);
        display.ClearLine(0, 10);
    }
    return Tuple.Create(userList, true);
}

// Function to create a task
// Returns a Tuple with List<Project> and a bool
Tuple<List<Project>, bool> CreateTask(List<Project> projectList, List<User> userList)
{
    DateTime date;
    display.ClearOutputScreenFromPosY(9, projectList.Count);
    display.ShowString(green, " ", 4, 3);
    display.ShowString(green, " ", 4, 4);
    display.ShowString(green, " ", 4, 5);
    display.ShowMenu(green, "Write a task:", 0, 9);

    string task = Console.ReadLine();

    if (task.ToUpper().Equals("Q"))
    {
        return Tuple.Create(projectList, false);
    }

    display.ShowMenu(green, "Write a discription:", 0, 9);
    string description = Console.ReadLine();

    if (description.ToUpper().Equals("Q"))
    {
        return Tuple.Create(projectList, false);
    }

    display.ShowMenu(green, "Write a date when it shall be done:", 0, 9);
    do
    {
        string dateOfTask = Console.ReadLine();

        if (dateOfTask.ToUpper().Equals("Q"))
        {
            return Tuple.Create(projectList, false);
        }

        try
        {
            date = Convert.ToDateTime(dateOfTask.ToString());
            if (DateTime.Compare(DateTime.Now, date) <= 0)
            {
                break;
            }
        }
        catch (Exception ex)
        {
            display.ShowMenu(red, "Date not valid!", 0, 11);
            display.ShowMenu(red, "You must write a valid date from today and in future", 0, 11);
            Thread.Sleep(milliseconds);
            display.ClearLine(0, 11);
        }
    }
    while (true);

    do
    {
        int len = user.ShowMenu(userList, 0, 11);
        display.ShowMenu(green, "Write who shall do the task:", 0, 9);
        string doneBy = Console.ReadLine();

        if (doneBy.ToUpper().Equals("Q"))
        {
            return Tuple.Create(projectList, false);
        }
        else
        {
            try
            {
                int choise = Convert.ToInt32(doneBy);
                string name = userList[choise - 1].Username;
                projectList.Add(new Project(task, description, name, date));
                Console.Clear();
                break;
            }
            catch (Exception ex)
            {
                display.ShowMenu(red, "The input wasn't a valid number or sign", 0, 10 + len);
            }
        }
    }
    while (true);

    return Tuple.Create(projectList, true);

}

// Function to mark a task as done
void SetTaskDone(List<Project> projectList)
{
    do
    {
        project.ChooseFromProduct(projectList, 0, 9);
        display.SetCursurPos(21, 7);

        try
        {
            int chosen = Int32.Parse(Console.ReadLine());
            projectList[chosen - 1].TaskDone = true;
            display.ClearOutputScreenFromPosY(9, projectList.Count);
            break;
        }
        catch (Exception e)
        {
            display.ShowMenu(red, "Something went wrong", 0, 9);

        }
    }
    while (true);

}

// Function to update the display with the number of tasks and completed tasks
void updateDisplayTaskDone(List<Project> projectList)
{
    int task = projectList.Count();
    int taskDone = projectList.Where(Project => Project.TaskDone.Equals(true)).Count();
    display.ShowString(blue, task.ToString(), 14, 1);
    display.ShowString(blue, taskDone.ToString(), 30, 1);
}

// Function to check user login credentials
bool checkLogin()
{
    string username = "";
    string password = "";

    display.ShowMenu(yellow, "Write your Username: ", 0, 2);
    username = Console.ReadLine();
    display.ShowMenu(yellow, "Write your password ", 0, 2);
    password = Console.ReadLine();

    if (user.ValidateUser(username, password))
    {
        return true; ;
    }

    display.ShowMenu(red, "The user dont exist or wrong password | Try again", 0, 3);
    Thread.Sleep(milliseconds);
    display.ClearLine(0, 3);
    return false;
}

// Function to display a list of users
void showUsers(List<User> userList)
{
    user.Show(userList, 0, 9);
}

// If no users or userlistfile not exist -> You must Create users
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
        Tuple<List<User>, bool> myTuple = CreateUsers(userList);
        userList = myTuple.Item1;

        // Have pressed Q for quit
        if (!myTuple.Item2)
        {
            display.ShowMenu(green, "Do you want to save userlist to file? [Y/N]", 0, 2);
            string choice = Console.ReadLine();
            if (choice.ToUpper().Equals("Y"))
            {
                user.SaveToFile(userList);
                break;
            }
            else if (choice.ToUpper().Equals("N"))
            {
                user.Show(userList, 0, 4);
                Thread.Sleep(milliseconds);
                break;
            }
        }
    }
}

Console.Clear();
display.ShowMenu(yellow, "Logon to ToDo List", 0, 0);

// Check if logon is right 
while (!checkLogin()) ;
display.ShowMenu(green, "Login Worked!", 0, 2);
Thread.Sleep(milliseconds);
display.ClearLine(0, 2);

// 
while (true)
{
    display.ShowCategory(0, 0);                     // Write out ToDo List graphics
    updateDisplayTaskDone(projectList);             // Write out how many task there are and how many is done  
    display.SetCursurPos(21, 7);                    // Set Cursur to input line
    Console.ResetColor();                           // Resets forground color
    input.Append(Console.ReadLine());               // Read input to StringBuilder input

    switch (input.ToString())           
    {
        case "1":
            project.ShowMenu(projectList, 0, 9);    // Write out Projectlist
            break;
        case "2":
            CreateTask(projectList, userList);      // Write out Create Tash menues
            break;
        case "3":
            SetTaskDone(projectList);               // Edit Project list if task is done
            break;
        case "4":
            project.SaveToFile(projectList);        // Save projectlist to file in Current working directory
            break;
        default:
            break;
    }
    if (input.ToString().ToUpper().Equals("Q"))     // Save projectlist to file end exit program
    {
        project.SaveToFile(projectList);
        display.ShowMenu(green, "Task added successfully", 0, 10);
        Thread.Sleep(milliseconds);
        Console.Clear();
        System.Environment.Exit(0);
    }
    input.Clear();                                  // Clear input variable
}