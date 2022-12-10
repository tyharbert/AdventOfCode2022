using AdventOfCode2022;

var interfaceType = typeof(IChallengeDay);
var implemenationTypes = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(a => a.GetTypes())
    .Where(t => interfaceType != t && interfaceType.IsAssignableFrom(t))
    .ToList();

implemenationTypes.Select(t => Activator.CreateInstance(t))
    .Cast<IChallengeDay>()
    .OrderBy(cd => cd.DayNumber)
    .Where(cd => cd.DayNumber == 9)
    .ToList()
    .ForEach(cd => cd.PrintResults());
