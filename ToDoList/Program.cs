// See https://aka.ms/new-console-template for more information
using System.IO;
using System.Text;

Console.WriteLine("Hello, World!");
string path = "c:\\tmp\\test.txt";
Output fileOutput = new Output();

StringBuilder csv = new StringBuilder();

List<Car> cars = new List<Car>();
List<Car> readbackcars = new List<Car>();
Car car1 = new Car("Volvo", "V90");
car1.Year = 2019;
Car car2 = new Car("SAAB", "900T");
//carBrands.Add(car1);  // Can not add to string list
Car car3 = new Car("BMW", "XL200", 2020);

cars.Add(car1);
cars.Add(car2);
cars.Add(car3);

foreach (Car car in cars)
{
    csv.Append("," + car.Brand+ "," + car.Model + "\n");
//    Console.WriteLine($"Brand: {car.Brand} Model: {car.Model}");
}

//List<Car> FileCars = 
Console.WriteLine(fileOutput.ReadFromFile("c:\\tmp\\test.txt"));
                               //.Skip(1)
                               //.Select(Car => FileCars.FromCsv(Car)
                               //.ToList();



string textString = "These lines will be writend to test.txt";

fileOutput.WriteFile(path, textString);

Console.WriteLine("Read Back");
string str = fileOutput.OpenFileForRead(path);

//foreach (string.)
//Console.WriteLine(IOoutput.OpenFileForWrite("c:\textfile.txt"));

// Output class for managing console and print to file output
class Output
{
    // Constructors
    // Default constructor
    public Output()   
    {
    }

    // Constructor for file handeling
    public Output(string filePath, string fileName, string fileExtension, string fileType)
    {
        FilePath = filePath;
        FileName = fileName;
        FileExtension = fileExtension;
        FileType = fileType;
    }

    public bool FileExists { get; set; }
    public int PosX { get; set; }
    public int PosY { get; set; }
    public string FilePath { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public string FileType { get; set; }
    
    public string ReadFromFile(string fileName)
    {
             string line;
       try
        {
            StreamReader fileRead = new StreamReader(fileName);

            line = fileRead.ReadLine();

            while (line != null)
            {
                Console.WriteLine(line);
                line = fileRead.ReadLine();
            }
        }
        catch (Exception e)
        {
            return "File couldent be opend";
        }

        return line;
    }

    
    public void WriteListToFile(StringBuilder csv)
    {
//        Console.WriteLine(csv);
        string dir = "c:\\tmp\\test.txt";
        File.WriteAllText(dir, csv.ToString());
    
    }
    public string WriteFile(string path, string textString)
    {
        try
        {
            if (!File.Exists(path))
            {
                using (FileStream fs = File.Create(path))
                {
                    System.IO.File.WriteAllText(path, "Text to add to the file\n");
                }
            }
            else
            {
                Console.WriteLine($"{path} File exists");
                //                Console.WriteLine($"{path} File dont exist");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("File couldent be opend");
        }
        finally
        {
            Console.WriteLine("Done");
        }
        return "";
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


    // Metods for set the positions
    public void SetCursurPos(int posX, int posY)
    {
        Console.SetCursorPosition(posX, posY);
    }

    public void ShowCategory()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Product".PadRight(10) + "Brand".PadRight(10) + "Model".PadRight(15) + "Office".PadRight(15) + "Purchase Date".PadRight(14) + "Price in USD".PadRight(17) + "Currency".PadRight(10) + "Local price");
        Console.WriteLine("-------".PadRight(10) + "-----".PadRight(10) + "-----".PadRight(15) + "------".PadRight(15) + "-------------".PadRight(14) + "------------".PadRight(17) + "--------".PadRight(10) + "-----------");
        Console.ResetColor();
    }

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


}
class Car
{
    public Car(string brand, string model)
    {
        Brand = brand;
        Model = model;
    }

    public Car(string brand, string model, int year)
    {
        Brand = brand;
        Model = model;
        Year = year;
    }

    public static Car FromCsv(string csv)
    {
        string[] csvStr = csv.Split(',');
        Car cars = new Car();
        cars.Brand = Convert.ToString(csvStr[0]);
        cars.Model = Convert.ToString(csvStr[1]);
        return cars;
    }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
}
