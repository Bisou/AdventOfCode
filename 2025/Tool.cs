public static class Tool {
    public static void LogStart(int day, int part, string dataType) {
        Console.WriteLine($"Day{day:00}-Part{part}-{dataType}");
    }

    public static string[]? ReadAll(int day, string dataType) {
        return File.ReadAllLines(@$".\Inputs\Day{day:00}-{dataType}.txt");   
    }
}