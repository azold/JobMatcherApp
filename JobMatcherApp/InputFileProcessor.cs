namespace JobMatcherApp
{
    public class InputFileProcessor
    {
        public (List<Vehicle>, List<Job>) ProcessDataFromFile(string inputFile)
        {
            if (File.Exists(inputFile))
            {
                string[] allLines = File.ReadAllLines(inputFile);
                int numberOfVehicles = int.Parse(allLines[0]);

                List<Vehicle> vehicles = processVehicles(allLines, numberOfVehicles);

                int numberOJobs = int.Parse(allLines[numberOfVehicles + 1]);

                string[] jobLines = new string[numberOJobs];

                Array.Copy(allLines, numberOfVehicles + 2, jobLines, 0, numberOJobs);

                List<Job> jobs = processJobs(jobLines, numberOJobs);
                return (vehicles, jobs);
            }
            else
            {
                throw new FileNotFoundException("No file was found.");
            }
        }

        private List<Job> processJobs(string[] lines, int numberOJobs)
        {
            List<Job> jobs = new List<Job>();
            for (int i = 0; i < numberOJobs; i++)
            {
                Job job = new Job();
                string[] lineData = lines[i].Trim().Split(' ');

                int jobID = int.Parse(lineData[0]);
                job.ID = jobID;
                job.JobType = lineData[1]; ;

                jobs.Add(job);
            }
            return jobs;
        }

        private List<Vehicle> processVehicles(string[] lines, int numberOfVehicles)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            for (int i = 0; i < numberOfVehicles; i++)
            {
                Vehicle vehicle = new Vehicle();
                string[] lineData = lines[i + 1].Trim().Split(' ');

                int vehicleID = int.Parse(lineData[0]);
                vehicle.ID = vehicleID;

                int jobTypesLength = lineData.Length - 1;
                string[] jobTypes = new string[jobTypesLength];
                Array.Copy(lineData, 1, jobTypes, 0, jobTypesLength);

                vehicle.CompatibleJobTypes = jobTypes;

                vehicles.Add(vehicle);
            }
            return vehicles;
        }
    }
}