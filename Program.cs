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
        string chromeDriverPath = @"C:\Selenium_project\chromedriver-win64\chromedriver-win64\chromedriver.exe"; // Replace with your actual path

        // Initialize the ChromeDriver
        using (IWebDriver driver = new ChromeDriver(chromeDriverPath))
        {
            // Navigate to the website
            string url = "https://afd.calpoly.edu/web/sample-tables"; // Replace with the actual URL
            driver.Navigate().GoToUrl(url);

            // Uncomment one of the following options based on your preference:

            // Option 1: Extract data from a specific table (e.g., the first table)
            IWebElement specificTable = driver.FindElement(By.XPath("//table[2]"));
            List<List<string>> specificTableData = ExtractTableData(specificTable);
            PrintDataToTerminal(specificTableData, "Specific Table");
            SaveDataToTextFile(specificTableData, "output_specific_table.txt");
            SaveDataToCsvFile(specificTableData, "output_specific_table.csv");

            // Option 2: Extract data from all tables on the page
            // List<IWebElement> allTables = new List<IWebElement>(driver.FindElements(By.TagName("table")));
            // for (int i = 0; i < allTables.Count; i++)
            // {
            //     List<List<string>> tableData = ExtractTableData(allTables[i]);
            //     PrintDataToTerminal(tableData, $"Table {i + 1}");
            //     SaveDataToTextFile(tableData, $"output_table_{i + 1}.txt");
            //     SaveDataToCsvFile(tableData, $"output_table_{i + 1}.csv");
            // }
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
