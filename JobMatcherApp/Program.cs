using JobMatcherApp;

internal static class Program
{
    [STAThread]
    static void Main()
    {
       string inputFile = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\data\mediordev.txt";

        InputFileProcessor processor = new InputFileProcessor();
        (List<Vehicle> vehicles, List<Job> jobs) = processor.ProcessDataFromFile(inputFile);

        VehicleToJobMatcher matcher = new VehicleToJobMatcher(vehicles, jobs);
        matcher.MatchVehiclesToJobs();

        ResultPrinter resultPrinter = new ResultPrinter();
        resultPrinter.PrintResults(matcher.PairedJobs, matcher.FailedJobs, matcher.AvailableVehicles);
    }
}