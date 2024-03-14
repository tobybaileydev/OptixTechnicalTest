namespace OptixTechnicalTest.Integration.Tests.Utilities;

[TestClass]
public class Initializer
{    
    [AssemblyInitialize]
    public static void Initialize(TestContext testContext)
    {
        if (testContext is null)
        {
            throw new ArgumentNullException(nameof(testContext));
        }

        InitializeHelper initializeHelper = new();
        initializeHelper.RebuildDatabase();
        initializeHelper.PopulateDatabase();
    }
}
