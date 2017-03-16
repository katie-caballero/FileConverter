//Console App to convert XML to JSON and vice versa

using System.IO;
using System;
using System.Xml;
using System.Reflection;
using ConvertXMLJSON;

namespace FileConverter
{
    class Program
    {
        static void Main()
        {
            string outputFile;
            bool bFileIsXml = true;

            Console.WriteLine("Welcome to Katie's File Converter.");

startOfProgram:
            Console.WriteLine("Please enter the name of the file you would like to convert");
            string inputFile = Console.ReadLine();


            Console.WriteLine("input file: {0}", inputFile);

            string fileContents;
            string fileType = Path.GetExtension(inputFile);

            switch (fileType)
            {
                case ".xml":
                    outputFile = "outputJSON.json";
                    bFileIsXml = true;
                    break;

                case ".json":
                    outputFile = "outputXML.xml";
                    bFileIsXml = false;
                    break;

                default:
                    Console.WriteLine("Error: Please enter either a .xml or .json file");
                    goto endOfProgram;
            }

            try
            {
                using (FileStream inputFileStream = File.OpenRead(inputFile))
                {
                    using (StreamReader inputStreamReader = new StreamReader(inputFileStream))
                    {
                        fileContents = inputStreamReader.ReadToEnd();
                    }
                }

                Console.WriteLine("Loaded input file from '{0}'", inputFile);
            }
            catch(Exception exp)
            {
                Console.WriteLine("Error: Could not read file.");
                goto endOfProgram;
            }

            string convertedFile = String.Empty;

            if (bFileIsXml)
            {
                convertedFile = Converter.ConvertToJson(fileContents);
                Console.WriteLine("Converted xml to json");

                //save out
                using (FileStream outputFileStream = File.Open(outputFile, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter outputStreamWriter = new StreamWriter(outputFileStream))
                    {
                        outputStreamWriter.Write(convertedFile);
                        outputStreamWriter.Flush();
                        outputStreamWriter.Close();
                    }
                }

                Console.WriteLine("Saved output file to '{0}'", outputFile);
            }
            else
            {
               XmlDocument doc = Converter.ConvertToXML(fileContents);

                Console.WriteLine("Converted json to xml");

                doc.Save(outputFile);

                Console.WriteLine("Saved output file to '{0}'", outputFile);

            }



endOfProgram:
            Console.WriteLine();
            Console.WriteLine("What would you like to do next?");
            Console.WriteLine("c. Convert another file | e. Exit this program");
            string choice = Console.ReadLine();

            if (choice == "c")
                goto startOfProgram;
        }
    }
}