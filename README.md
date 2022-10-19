# CodeLab

## Solution
The endpoint is created at the ```api\Controllers\InsuranceController.cs```.

**Top** method inside the controller is an HttpGet method to return highest combined values of insurances with its respective descendants.
  
- The method takes two integer parameters maxCount and maxDepth and returns an integer array with ActionResult
    - maxCount sets how many of the highest combined values it will return in the array
    - maxDepth sets how many levels of descendants counted in the combined value of a respective parent

- The method works as follows;
    - Brings the list of insurances via the repository
    - Creates an empty array to save all combined values.
    - Starts to iterate over every insurance in the list
    - In every iteration it also calls the recursive method **LoopChildren** from the repository
        - This method loops over its descendants to the utmost bottom possible, until it reachs 'maxDepth' or has no child anymore
        - To track current depth in each level for every insurance, 'Depth' attribute added to the insurance model.
    - By running all loops from bottom siblings to top, it adds values to create cumulative sum 
    - Once it finishes all loops it puts the final sum value to the parent's respective index in the array
    - Once it finishes all of the insurances in the list it sorts the filled array and takes the highest certain amount of values according to 'maxCount'
    - It returns these combined values in an array with HTTP 200 OK.

## Information
Data-models used in the service is of type Insurance.

Each model has these properties:
- InsuranceId is the unique id of the insurance.
- Name is the name of the insurance.
- Value is the value of the insurance.
- ParentId is the parent insurance id to the insurance.
- Parent is the parent insurance to the insurance.
- Children contain all sub-insurances.

```src\api``` project - this is where you add your implementation.

```src\tests``` project - this is where the requirements are implemented as a test to verify the implementation.

## Scenario
We need an endpoint that can return top combined values in insurances with depth restraints.

### Requirements
 - Combine value in parent insurance with sub-insurances.
 - We need to be able to set the maximum amount of returned values. Utilize the maxCount parameter.
 - We need to be able to limit the depth of sub-insurances calulated. Utilize the maxDepth parameter.

## Hints
Seed data for the test can be found in the Seed method of the the ```Tests.cs``` class.
If you want to change it, feel free to do so.
However, know that the current theory is based on initial seed data.
Changing test seed data could potentially break the theory.
