using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobMatcherApp
{
    internal class ResultPrinter
    {
        private string outputFileForPairs = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\data\pairedResult.txt";
        private string outputFileForUnmatched = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + @"\data\unmatchedResult.txt";

        public void PrintResults(Dictionary<Vehicle, Job> matches, List<Job> failedJobs, List<Vehicle> joblessVehicles)
        {
            PrintMatches(matches);
            if (failedJobs.Any() || joblessVehicles.Any()) {
                PrintUnmatchedVehiclesAndJobs(joblessVehicles, failedJobs);
            }
        }

        private void PrintMatches(Dictionary<Vehicle, Job> matches)
        {
            try
            {
                var orderedMatches = matches.OrderBy(m => m.Key.ID);
                StreamWriter streamWriter = new StreamWriter(outputFileForPairs);
                Console.WriteLine("<Vehicle id> <Job id>");

                foreach (var match in orderedMatches)
                {
                    streamWriter.WriteLine($"{match.Key.ID} {match.Value.ID}");
                }

                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void PrintUnmatchedVehiclesAndJobs(List<Vehicle> joblessVehicles, List<Job> failedJobs)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(outputFileForUnmatched);

                if (joblessVehicles.Any())
                {
                    streamWriter.WriteLine("<Vehicle ID>\t<Compatible jobtypes>");
                    foreach (Vehicle vehicle in joblessVehicles)
                    {
                        string compatibleJobs = string.Join(", ", vehicle.CompatibleJobTypes);
                        streamWriter.WriteLine($"{vehicle.ID}\t{compatibleJobs}");
                    }
                    streamWriter.WriteLine();
                }

                if (failedJobs.Any())
                {
                    streamWriter.WriteLine("<Job ID>\t<Job type>");
                    foreach (Job job in failedJobs)
                    {
                        streamWriter.WriteLine($"{job.ID}\t{job.JobType}");
                    }
                }

                streamWriter.Close();
            }catch (Exception e)
            {
                Console.WriteLine("Exception:" + e.Message);
            }
        }
    }
}
