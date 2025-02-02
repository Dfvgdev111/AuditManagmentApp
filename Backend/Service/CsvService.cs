using System.Globalization;
using CsvHelper;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace Backend.Service
{
    public class CsvService
    {
         public byte[] GenerateCsv<T>(List<T> records)
    {
        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        
        csvWriter.WriteRecords(records);
        streamWriter.Flush();

        return memoryStream.ToArray();
    }
    }
}