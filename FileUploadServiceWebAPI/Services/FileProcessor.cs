using ExcelDataReader;
using FileUploadServiceWebAPI.Extensions;
using FileUploadServiceWebAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUploadServiceWebAPI.Services
{
    public class FileProcessor : IFileProcessor
    {
        public ProccesedData ExtractDataFromExcelFile(string filname)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\files");
            string filePath = Path.Combine(path, filname);
            StringBuilder sb = new StringBuilder();

            var evenNumbers = string.Empty;
            var oddNumbers = string.Empty;
            var numDivBy3 = string.Empty;
            var numDivBy5 = string.Empty;
            var numDivBy7 = string.Empty;

            var evenNumbersList = new List<int>();
            var oddNumbersList = new List<int>();
            var numDivBy3List = new List<int>();
            var numDivBy5List = new List<int>();
            var numDivBy7List = new List<int>();

            int divisibleBy9Count = 0;

            var allValues = new List<int>();
            ProccesedData data = null;

            double mode = 0, mean = 0, median = 0, avg = 0, sum = 0, noOfColumnsBy9 = 0;

            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        do
                        {
                            while (reader.Read()) //Each ROW
                            {
                                for (int column = 0; column < reader.FieldCount; column++)
                                {
                                    if (!string.IsNullOrEmpty(reader.GetValue(column)?.ToString()) || !string.IsNullOrWhiteSpace(reader.GetValue(column)?.ToString()))
                                    {
                                        int result;
                                        var isInt = int.TryParse(reader.GetValue(column)?.ToString(), out result);
                                        if (isInt)
                                        {
                                            if (result % 2 == 0)
                                            {
                                                evenNumbersList.Add(result);
                                            }

                                            if (result % 2 == 1)
                                            {
                                                oddNumbersList.Add(result);
                                            }

                                            if (result % 3 == 0)
                                            {
                                                numDivBy3List.Add(result);
                                            }

                                            if (result % 5 == 0)
                                            {
                                                numDivBy5List.Add(result);
                                            }

                                            if (result % 7 == 0)
                                            {
                                                numDivBy7List.Add(result);
                                            }

                                            if (result % 9 == 0)
                                            {
                                                noOfColumnsBy9++;
                                            }
                                            allValues.Add(result);
                                            //  Console.WriteLine(result);
                                        }
                                        // Console.WriteLine(reader.GetValue(column));//Get Value returns object
                                  

                                    }
                                }
                            }
                        } while (reader.NextResult()); //Move to NEXT SHEET

                    }

                }

                sum = allValues.Sum();
                mean = allValues.Average();
                mode = allValues.Mode();
                median = allValues.Median();
                avg = allValues.Average();
                if (noOfColumnsBy9 > 0)
                {
                    noOfColumnsBy9 = Math.Ceiling(noOfColumnsBy9 / 3);
                }

                evenNumbers = string.Join(",", evenNumbersList);
                oddNumbers = string.Join(",", oddNumbersList);
                numDivBy3 = string.Join(",", numDivBy3List);
                numDivBy5 = string.Join(",", numDivBy5List);
                numDivBy7 = string.Join(",", numDivBy7List);

                data = new ProccesedData
                {
                    Average = Math.Round(avg, 2).ToString(),
                    EvenNumbers = evenNumbers,
                    OddNumbers = oddNumbers,
                    Mean = Math.Round(mean, 2).ToString(),
                    Median = Math.Round(median, 2).ToString(),
                    Mode = Math.Round(mode, 2).ToString(),
                    NumbersDivBy3 = numDivBy3,
                    NumbersDivBy5 = numDivBy5,
                    NumbersDivBy7 = numDivBy7,
                    Sum = Math.Round(sum, 2).ToString(),
                    NoOfColBy9 = noOfColumnsBy9.ToString()
                };

                data.ResponseCode = "00";
            }
            catch (Exception ex)
            {
                data = new ProccesedData
                {
                    ResponseCode = "02"
                };
            }

            return data;


        }
    }
}
