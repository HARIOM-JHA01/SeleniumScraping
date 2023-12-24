using System;
using System.Collections.Generic;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main()
    {
        // Set the path to the ChromeDriver executable
        string chromeDriverPath = @"C:\Selenium_project\chromedriver-win64\chromedriver-win64\chromedriver.exe";

        //Input url
        Console.Write("Enter url : ");
        string url = Console.ReadLine()!;

        //Input Table Info
        Console.Write("1. Specific Table(Table No.)\n2. All tables\n");
        Console.Write("Enter Option : ");
        int tableNo = -1;
        int choice = int.Parse(Console.ReadLine());
        if(choice == 1)
        {
            Console.Write("Enter Table No. : ");
            tableNo = int.Parse(Console.ReadLine());
        } else if(choice == 2)
        {
            Console.WriteLine("Working....");
        } else
        {
            Console.WriteLine("Invalid option.");
            return;
        }
        


        // Initialize the ChromeDriver
        IWebDriver driver = new ChromeDriver(chromeDriverPath);
        using (driver)
        {
            // Navigate to the website
            driver.Navigate().GoToUrl(url);
            if (choice == 1)
            {
                // Option 1: Extract data from a specific table (e.g., the first table)
                IWebElement specificTable = driver.FindElement(By.XPath($"//table[{tableNo}]"));
                List<List<string>> specificTableData = ExtractTableData(specificTable);
                PrintDataToTerminal(specificTableData, $"Table {tableNo}");
                SaveDataToTextFile(specificTableData, $"results/output_table_{tableNo}.txt");
                SaveDataToCsvFile(specificTableData, $"results/output_table_{tableNo}.csv");
            }
            else if (choice == 2)
            {
                // Option 2: Extract data from all tables on the page
                 List<IWebElement> allTables = new List<IWebElement>(driver.FindElements(By.TagName("table")));
                 for (int i = 0; i < allTables.Count; i++)
                 {
                     List<List<string>> tableData = ExtractTableData(allTables[i]);
                     PrintDataToTerminal(tableData, $"Table {i + 1}");
                     SaveDataToTextFile(tableData, $"results/output_table_{i + 1}.txt");
                     SaveDataToCsvFile(tableData, $"results/output_table_{i + 1}.csv");
                 }
            } else
            {
                Console.WriteLine("Invalid Option");
            }
        }
    }

    static List<List<string>> ExtractTableData(IWebElement table)
    {
        List<List<string>> tableData = new List<List<string>>();

        // Iterate through rows
        foreach (IWebElement row in table.FindElements(By.TagName("tr")))
        {
            List<string> rowData = new List<string>();

            // Iterate through columns
            foreach (IWebElement cell in row.FindElements(By.TagName("td")))
            {
                rowData.Add(cell.Text);
            }

            tableData.Add(rowData);
        }

        return tableData;
    }

    static void PrintDataToTerminal(List<List<string>> tableData, string tableName)
    {
        Console.WriteLine($"Printing data for {tableName} to terminal:");
        foreach (var row in tableData)
        {
            Console.WriteLine(string.Join("\t", row));
        }
    }

    static void SaveDataToTextFile(List<List<string>> tableData, string filePath)
    {
        Console.WriteLine($"Saving data to {filePath}");
        File.WriteAllLines(filePath, tableData.ConvertAll(row => string.Join("\t", row)));
    }

    static void SaveDataToCsvFile(List<List<string>> tableData, string filePath)
    {
        Console.WriteLine($"Saving data to {filePath}");
        File.WriteAllLines(filePath, tableData.ConvertAll(row => string.Join(",", row)));
    }
}
