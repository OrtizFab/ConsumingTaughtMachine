namespace ConsumingThoghtMachineAPI.Repositories
{
    public interface ICSVService
    {
        void WriteCSV<T>(List<T> records);
    }
}
