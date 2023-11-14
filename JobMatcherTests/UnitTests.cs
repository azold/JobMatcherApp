using JobMatcherApp;
using System.Collections.Generic;

namespace JobMatcherTests
{
    [TestClass]
    public class UnitTests
    {
        Vehicle vehicle1 = new Vehicle { ID = 1, CompatibleJobTypes = new string[1] { "A" } };
        Vehicle vehicle2 = new Vehicle { ID = 2, CompatibleJobTypes = new string[1] { "B" } };
        Vehicle vehicle3 = new Vehicle { ID = 3, CompatibleJobTypes = new string[4] { "A", "B", "C", "E" } };
        Vehicle vehicle4 = new Vehicle { ID = 4, CompatibleJobTypes = new string[2] { "B", "C" } };
        Vehicle vehicle5 = new Vehicle { ID = 5, CompatibleJobTypes = new string[1] { "D" } };

        Job job1 = new Job { ID = 1, JobType = "A" };
        Job job2 = new Job { ID = 2, JobType = "A" };
        Job job3 = new Job { ID = 3, JobType = "A" };
        Job job4 = new Job { ID = 4, JobType = "A" };
        Job job5 = new Job { ID = 5, JobType = "E" };

        [TestMethod]
        public void TestGetAllSingleCababilityVehicles()
        {
            // Arrange
            List<Vehicle> vehicles = new List<Vehicle>() { };
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            vehicles.Add(vehicle5);

            List<Vehicle> expectedResult = new List<Vehicle>() { };
            expectedResult.Add(vehicle1);
            expectedResult.Add(vehicle2);
            expectedResult.Add(vehicle5);

            List< Job > availableJobs = new List<Job>();

            //  Act
            VehicleToJobMatcher vehicleToJobMatcher = new VehicleToJobMatcher(vehicles, availableJobs);            
            var result = vehicleToJobMatcher.getAllSingleCababilityVehicles(vehicles);

            //Assert
            Assert.AreEqual(expectedResult.Count, result.Count);
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestGetAllSpecificJobCompatibleVehiclesWithCorrectData()
        {
            // Arrange
            List<Vehicle> vehicles = new List<Vehicle>() { };
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            vehicles.Add(vehicle5);

            List<Vehicle> expectedResult = new List<Vehicle>() { };
            expectedResult.Add(vehicle1);
            expectedResult.Add(vehicle3);

            List<Job> availableJobs = new List<Job>();            

            //  Act
            VehicleToJobMatcher vehicleToJobMatcher = new VehicleToJobMatcher(vehicles, availableJobs);
            var result = vehicleToJobMatcher.getAllSpecificJobCompatibleVehicles(vehicles, "A");

            //Assert
            Assert.AreEqual(expectedResult.Count, result.Count);
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestGetAllSpecificJobCompatibleVehiclesWithUnexistingJobType()
        {
            // Arrange
            List<Vehicle> vehicles = new List<Vehicle>() { };
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            vehicles.Add(vehicle5);

            List<Vehicle> expectedResult = new List<Vehicle>() { };

            List<Job> availableJobs = new List<Job>();

            //  Act
            VehicleToJobMatcher vehicleToJobMatcher = new VehicleToJobMatcher(vehicles, availableJobs);
            var result = vehicleToJobMatcher.getAllSpecificJobCompatibleVehicles(vehicles, "T");

            //Assert
            Assert.AreEqual(expectedResult.Count, result.Count);
            CollectionAssert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestCountDemandsForVehicles()
        {
            // Arrange
            List<Vehicle> vehicles = new List<Vehicle>() { };
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            vehicles.Add(vehicle5);

            List<Job> availableJobs = new List<Job>();
            availableJobs.Add(job1);
            availableJobs.Add(job2);
            availableJobs.Add(job3);
            availableJobs.Add(job4);
            availableJobs.Add(job5);

            Dictionary<int, int> expectedResult = new Dictionary<int, int>();
            expectedResult.Add(1, 1);
            expectedResult.Add(3, 2);

            //  Act
            VehicleToJobMatcher vehicleToJobMatcher = new VehicleToJobMatcher(vehicles, availableJobs);
            var jobCompatibleVehicles = vehicleToJobMatcher.getAllSpecificJobCompatibleVehicles(vehicles, "A");
            var result = vehicleToJobMatcher.CountDemandsForVehicles(jobCompatibleVehicles);

            //Assert
            Assert.AreEqual(expectedResult.Count, result.Count);
            CollectionAssert.AreEqual(expectedResult, result);
        }
    }
}