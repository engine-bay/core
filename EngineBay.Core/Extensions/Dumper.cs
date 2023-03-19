namespace EngineBay.Core
{
    using Newtonsoft.Json;

    public static class Dumper
    {
        public static void Dump(this object obj)
        {
            Console.WriteLine("OBJECT DUMP:");
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            var formattedJsonString = JsonConvert.SerializeObject(obj, Formatting.Indented, settings);

            Console.WriteLine(formattedJsonString);
        }
    }
}
