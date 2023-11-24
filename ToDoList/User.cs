using System.Text;

// Class to handle user-related operations Inherit from Project Class
class User : Project
{
    // { get; set } fields properties
    public string Username { get; set; }
    public string Password { get; set; }
    public int UserID { get; set; }
    public string FilePath { get; set; }

    // Constructors
    // Default constructor
    public User()
    {
    }

    // Constructor with parameters
    public User(string username, string password, int userID)
    {
        Username = username;
        Password = password;
        UserID = userID;
    }

    // Method to validate user credentials
    public bool ValidateUser(string usr, string pass)
    {
        List<User> userList = ReadFromFile();

        if (pass.Length == 0)
        {
            if (userList.Any(User => User.Username == usr))
            {
                return true;
            }
        }
        else
        {
            foreach (User user in userList)
            {
                if (user.Username.ToLower().ToString().ToUpper().Equals(usr.ToUpper()) && user.Password.ToString().Equals(pass))
                {
                    return true;
                }
            }
        }
        return false;
    }

    // Method to convert user list to CSV string
    private string ConvertToCSVString(List<User> userList)
    {
        StringBuilder str = new StringBuilder();
        foreach (User list in userList)
        {
            str.AppendLine($"{list.Username},{list.Password},{list.UserID},");
        }
        return str.ToString();
    }

    // Method to convert CSV string to user list
    private List<User> ConvertStringToList(List<User> userList, string input)
    {
        int count = 0;

        string[] listString = input.TrimEnd(',').Split(',').ToArray();

        if (listString.Count() == 3)
        {
            userList.Add(new User(listString[0], listString[1], Convert.ToInt32(listString[2])));
        }
        return userList;
    }

    // Method to save user list to file
    public void SaveToFile(List<User> userList)
    {
        string list = ConvertToCSVString(userList);

        try
        {
            FileStream fs = File.Open(FilePath, FileMode.Append);
            using (StreamWriter sw = new StreamWriter(fs))
                sw.Write(list);
            fs.Close();
        }
        catch (Exception e)
        {
            ShowMenu(ConsoleColor.Red, "File couldent be opend", 0, 8);
        }
    }

    // Method to read in user list from file
    public List<User> ReadFromFile()
    {
        List<User> userList = new List<User>();
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
                        userList = ConvertStringToList(userList, lines);
                    }
                }
            }
            catch (Exception e)
            {
                ShowMenu(ConsoleColor.Red, "File couldent be opend", 0, 8);
            }
        }
        return userList;
    }

    // Method to display user information
    public void Show(List<User> userList, int posX, int posY)
    {
        int count = 0;
        SetCursurPos(posX, posY);
        foreach (User user in userList)
        {
            ShowMenu(ConsoleColor.Green, $"Username: {user.Username} Password: {user.Password} UserID: {user.UserID.ToString()}", 0, posY + count);
            count++;
        }

    }

    // Method to display user menu
    public int ShowMenu(List<User> userList, int posX, int posY)
    {
        int count = 0;
        SetCursurPos(posX, posY);
        foreach (User user in userList)
        {
            ShowMenu(ConsoleColor.Green, $">> [{(count + 1)}] {user.Username}", 0, posY + count);
            count++;
        }
        return count + 1;
    }
}
