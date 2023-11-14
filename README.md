# JobMatcherApp

## The application is created to solve a logistic problem:

A common dilemma what a trucking company faces is matching vehicles and orders. Which truck should carry which job in order to make the operation as cheap as possible. This is a very common yet very complex challenge. The task is to solve a subset of this problem.

Given two lists, the first one contains the available trucks and the compatible job types the truck can carry. The other one contains the current jobs.

Match the vehicles with jobs, where should consider the job type constraints during the pairing. The types of jobs are marked with letters. Each truck has a list of letters that indicates the compatible job types.
The goal is to find a truck for each job. If can’t find a truck for all the jobs, a partial solution is also acceptable.

The format of the input file is the following:

`<Number of vehicles>`

`<Vehicle id> <Compatible job types list>`

`...`

`<Vehicle id> <Compatible job types list>`

`<Number of current jobs>`

`<Job id> <Job type>`

`...`

`<Job id> <Job type>`

The input file is available in the Data folder of the app's project.

The expected output format:
`<Vehicle id> <Job id>...`

## The solution
First, pair up every vehicle that has only one compatible job type with the jobs having the same job type.

After there will be a set of pairs, and a descended list of vehicles and jobs (with the unmatched ones).
The next thing to do is to find a job for every vehicle.

The logic is to check if there is any other job the vehicle (in the remaining list) could paired with. If there is, then have to select the first vehicle that could serve the least jobs while still compatible with the first job from the descended list. 

Also, have to collect all the jobs that were unable to be paired up with a vehicle to a different file. If a vehicle remains available the code also prints that to the same file.
