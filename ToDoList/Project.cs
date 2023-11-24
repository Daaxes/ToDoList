// See https://aka.ms/new-console-template for more information
using System.Text;

class Project : Display
{
    public string Task { get; set; }
    public DateTime DateOfTask { get; set; }
    public string DoneBy { get; set; }
    public string Description { get; set; }
    public string FilePath { get; set; }
    public bool TaskDone { get; set; }

    // Constructors
    // Defalt Constructor
    public Project() 
    { 
    }
    public Project(string task, string description, string doneBy, DateTime dateOfTask)
    {
        Task = task;
        DateOfTask = dateOfTask;
        Description = description;
        DoneBy = doneBy;
        TaskDone = false;
    }

    public Project(string task, string description, string doneBy, DateTime dateOfTask, bool taskDone = false)
    {
        Task = task;
        DateOfTask = dateOfTask;
        Description = description;
        DoneBy = doneBy;
        TaskDone = taskDone;
    }

    private string ConvertToCSVString(List<Project> projectList)
    {
        StringBuilder str = new StringBuilder();
        foreach (Project list in projectList)
        {
            str.AppendLine($"{list.Task},{list.Description},{list.DoneBy},{list.DateOfTask.ToString("yy-MM-dd")},{list.TaskDone},");
        }
        return str.ToString();
    }

    private List<Project> ConvertStringToList(List<Project> projectList, string input)
    {
        int count = 0;

        string[] listString = input.TrimEnd(',').Split(',').ToArray();

        if (listString.Count() == 5)
        {
            projectList.Add(new Project(listString[0], listString[1], listString[2], Convert.ToDateTime(listString[3]), Convert.ToBoolean(listString[4])));
        }
        return projectList;
    }

    public void SaveToFile(List<Project> projectList)
    {
        string list = ConvertToCSVString(projectList);

        try
        {
            FileStream fs = File.Open(FilePath, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs))
                sw.Write(list);
            fs.Close();
        }
        catch (Exception e)
        {
            ShowMenu(ConsoleColor.Red, "File couldent be opend", 0, 8);
        }
    }

    public List<Project> ReadFromFile()
    {
        List<Project> projectList = new List<Project>();
        int count = 0;
        string lines = "";
        StringBuilder sb = new StringBuilder();

        if (File.Exists(FilePath))
        {
            try
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    while ((lines = sr.ReadLine()) != null)
                    {
                        projectList = ConvertStringToList(projectList, lines);
                    }
                }
            }
            catch (Exception e)
            {
                ShowMenu(ConsoleColor.Red, "File couldent be opend", 0, 8);
            }
        }
        return projectList;
    }
//    public Project(string task, DateTime dateOfTask, string description, string doneBy, bool taskDone = false)

    public int ShowMenu(List<Project> projectList, int posX, int posY)
    {
        int count = 0;
        int padLenTask = projectList.Max(Product => Product.Task.Length) + 1;
        int padLenDescription = projectList.Max(Product => Product.Description.Length) + 1;
        int lenDoneBy = projectList.Max(Product => Product.DoneBy.Length) + 1;
        int lenDate = 10;
        int lenTaskDone = 7;

        ShowMenu(ConsoleColor.Green, "  ".PadRight(4) + "Tasks".PadRight(padLenTask) + "Deskription".PadRight(padLenDescription) + "DoneBy".PadRight(lenDoneBy) + "Date of the task".PadRight(17) + "Task Done", posX, posY);
        SetCursurPos(posX, posY);
        foreach (Project project in projectList)
        {
            ShowMenu(ConsoleColor.Green, $">> {project.Task.PadRight(padLenTask)} {project.Description.PadRight(padLenDescription)}{project.DoneBy.PadRight(lenDoneBy)}{project.DateOfTask.ToString("yy-MM-dd").PadRight(17)}{project.TaskDone}", 0, posY + 1 + count);
            count++;
        }
        return count + 1;
    }
 
    public int ChooseFromProduct(List<Project> projectList, int posX, int posY)
    {
        int count = 0;
        int padLenTask = projectList.Max(Product => Product.Task.Length) + 1;
        int padLenDescription = projectList.Max(Product => Product.Description.Length) + 1;
        int lenDoneBy = projectList.Max(Product => Product.DoneBy.Length) + 1;
        int lenDate = 10;
        int lenTaskDone = 7;

        ShowMenu(ConsoleColor.Green, "  ".PadRight(9) + "Tasks".PadRight(padLenTask) + "Deskription".PadRight(padLenDescription) + " DoneBy".PadRight(lenDoneBy) + " Date of the task".PadRight(18) + "Task Done", posX, posY);
        SetCursurPos(posX, posY);
        
        foreach (Project project in projectList)
        {
            ShowMenu(ConsoleColor.Green, $">> [{(count + 1)}] - {project.Task.PadRight(padLenTask)}{project.Description.PadRight(padLenDescription)} {project.DoneBy.PadRight(lenDoneBy)} {project.DateOfTask.ToString("yy-MM-dd").PadRight(17)}{project.TaskDone} ", 0, posY + 1 + count);
            count++;
        }
        return count + 1;
    }
}


