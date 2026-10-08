namespace Paysuit;

public static class Settings
{
    public static string PaySuiteToken =>
        Environment.GetEnvironmentVariable("PAYSUITE_TOKEN") 
        ?? throw new Exception("PAYSUITE_TOKEN is not set.");
}
