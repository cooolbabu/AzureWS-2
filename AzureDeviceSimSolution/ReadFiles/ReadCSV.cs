namespace ReadFiles;

using System;
using System.IO;
using CsvHelper;

// At somepoint change this CVS Helper. 
public class ReadCSV
{
    public string fileName { get; set; }

    public void ReadFile()
    {

        try
        {
            if (this.fileName == null)
            {
                Console.WriteLine("Filename is null");
            }
            else
            {
                using (StreamReader reader = new StreamReader(this.fileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] values = line.Split(',');

                        foreach (string value in values)
                        {
                            Console.Write(value + "\t");
                        }

                        Console.WriteLine(); // Move to the next line after printing values
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
    }
}


