namespace JobMatcherApp
{
    public class VehicleToJobMatcher
    {
        public List<Vehicle> AvailableVehicles { get; set; }
        public List<Job> AvailableJobs { get; set; }
        public List<Job> FailedJobs { get; set; }
        public Dictionary<Vehicle, Job> PairedJobs { get; set; }

        public VehicleToJobMatcher(List<Vehicle> availableVehicles, List<Job> availableJobs)
        {
            AvailableVehicles = availableVehicles;
            AvailableJobs = availableJobs;
            PairedJobs = new Dictionary<Vehicle, Job>();
            FailedJobs = new List<Job>();
        }

        public void MatchVehiclesToJobs()
        {
            MatchSingleCababilityVehiclesToJobs();
            MatchMultiCababilityVehiclesToJobs();
        }

        //Match every single jobCompatibility type vehicle to a job 
        private void MatchSingleCababilityVehiclesToJobs()
        {
            List<Vehicle> singleCapacityVehicles = getAllSingleCababilityVehicles(AvailableVehicles);

            foreach (Vehicle vehicle in singleCapacityVehicles)
            {
                Job job = AvailableJobs.FirstOrDefault(j => j.JobType.Equals(vehicle.CompatibleJobTypes[0]));
                if (job != null)
                {
                    PairedJobs.Add(vehicle, job);
                    AvailableJobs.Remove(job);
                    AvailableVehicles.Remove(vehicle);
                }
            }
        }
        private void MatchMultiCababilityVehiclesToJobs()
        {
            List<Job> tempAvailableJobs = new List<Job>(AvailableJobs);
            foreach (Job job in tempAvailableJobs)
            {
                var jobCompatibleVehicles = getAllSpecificJobCompatibleVehicles(AvailableVehicles, job.JobType);

                //There is no available vehicles for the job
                if (jobCompatibleVehicles.Count == 0)
                {
                    FailedJobs.Add(job);
                    AvailableJobs.Remove(job);
                    continue;
                }

                //There is only one available vehicle for the job
                if (jobCompatibleVehicles.Count == 1)
                {
                    PairedJobs.Add(jobCompatibleVehicles[0], job);
                    AvailableJobs.Remove(job);
                    AvailableVehicles.Remove(jobCompatibleVehicles[0]);
                    continue;
                }

                //Dict<Vehicle Id , Vihicle Demand>
                Dictionary<int, int> vehicleDemandsPairs = CountDemandsForVehicles(jobCompatibleVehicles);

                var orderedVehicleDemandPairs = vehicleDemandsPairs.OrderBy(v => v.Value);
                //Get the vehicle which has the less demands -> more demands = more jobtype could be served so couold be useful later for different types
                Vehicle optimalVehicleForJob = AvailableVehicles.Find(a => a.ID == orderedVehicleDemandPairs.First().Key);

                PairedJobs.Add(optimalVehicleForJob, job);
                AvailableJobs.Remove(job);
                AvailableVehicles.Remove(optimalVehicleForJob);
            }
        }

        // prefer to set the next 3 methods to private, change to public just for the unit tests
        //Check how much OTHER jobtype could be ordered to a vehicle
        public Dictionary<int, int> CountDemandsForVehicles(List<Vehicle> jobCompatibleVehicles)
        {
            Dictionary<int, int> vehicleDemandsPairs = new Dictionary<int, int>();
            foreach (Vehicle vehicle in jobCompatibleVehicles)
            {
                int vehicleDemandNumber = 0;
                foreach (string jobType in vehicle.CompatibleJobTypes)
                {
                    vehicleDemandNumber += AvailableJobs.Any(a => a.JobType.Equals(jobType)) ? 1 : 0;
                }
                vehicleDemandsPairs.Add(vehicle.ID, vehicleDemandNumber);
            }

            return vehicleDemandsPairs;
        }
        
        public List<Vehicle> getAllSpecificJobCompatibleVehicles(List<Vehicle> vehicles, string jobType)
        {
            return vehicles.Where(t => t.CompatibleJobTypes.Contains(jobType)).ToList();
        }
        public List<Vehicle> getAllSingleCababilityVehicles(List<Vehicle> vehicles)
        {
            return vehicles.Where(t => t.CompatibleJobTypes.Length == 1).ToList();
        }
    }
}
