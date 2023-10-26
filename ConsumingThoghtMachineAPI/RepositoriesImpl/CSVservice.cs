using ConsumingThoghtMachineAPI.Repositories;
using CsvHelper;
using System.Globalization;
using System.IO;

namespace ConsumingThoghtMachineAPI.RepositoriesImpl
{
    public class CSVservice : ICSVService
    {
        public void WriteCSV<T>(List<T> records)
        {
            var path = $"{Directory.GetCurrentDirectory()}{@"\Files"}";
            using (var writer = new StreamWriter(path + "\\files.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecord(records);
            }

        }
    }
}
