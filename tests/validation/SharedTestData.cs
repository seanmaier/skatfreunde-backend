using static skat_back.utilities.constants.TestingConstants;

namespace skat_back.Tests.validation;

public class SharedTestData
{
    public class InvalidGuidTestData : TheoryData<string>
    {
        public InvalidGuidTestData()
        {
            Add(null!); // Null
            Add(""); // Empty
            Add("not-a-guid");
            Add("123e4567-e89b-12d3");
            Add("123e4567-e89b-12d3-a456-xyzxyzxyzx");
            Add(TestUserId);
        }
    }
}