# Selenium Web Scraping Tool

This simple C# console application uses Selenium to scrape data from HTML tables on a website. It allows users to specify whether they want to extract data from a specific table or from all tables on the page.

## Prerequisites

Before running the application, ensure you have the following:

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.
- [ChromeDriver](https://sites.google.com/chromium.org/driver/) executable placed in the specified directory (`C:\Selenium_project\chromedriver-win64\chromedriver-win64\chromedriver.exe`).

## Usage

1. Clone the repository or download the source code.
2. Open a terminal and navigate to the project directory.
3. Build the project using the command:

   ```bash
   dotnet build
   ```

4. Run the application:

   ```bash
   dotnet run
   ```

5. Enter the URL of the website when prompted.
6. Choose whether to extract data from a specific table or from all tables.

   - If choosing a specific table, enter the table number.
   - If choosing all tables, the application will automatically extract data from each table on the page.

7. The application will print the table data to the console and save it in both tab-separated and comma-separated text files.

## Example

```bash
Enter url : [Enter the URL here]
1. Specific Table(Table No.)
2. All tables
Enter Option : 1
Enter Table No. : 2
Working....
Printing data for Table 2 to terminal:
Header1    Header2    Header3
Data1      Data2      Data3
...

Saving data to results/output_table_2.txt
Saving data to results/output_table_2.csv
```

## Notes

- Ensure proper internet connectivity as the application relies on Selenium to fetch data from the website.
- Customize the ChromeDriver path and file output locations as needed.
- This tool is a starting point and may require modifications for specific use cases or websites.

Feel free to contribute, report issues, or enhance the functionality!
