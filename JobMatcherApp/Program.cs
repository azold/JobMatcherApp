using JobMatcherApp;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        InputFileProcessor processor = new InputFileProcessor();
        (List<Vehicle> vehicles, List<Job> jobs) = processor.ProcessDataFromFile();

        VehicleToJobMatcher matcher = new VehicleToJobMatcher(vehicles, jobs);
        matcher.MatchVehiclesToJobs();

        ResultPrinter resultPrinter = new ResultPrinter();
        resultPrinter.PrintResults(matcher.PairedJobs, matcher.FaildJobs, matcher.AvailableVehicles);
    }
}