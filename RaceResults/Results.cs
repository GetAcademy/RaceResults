using System.Runtime.Intrinsics.X86;

namespace RaceResults
{
    internal class Results
    {
        public readonly List<TimeMeasurement> _timeMeasurements;

        public Results()
        {
            var lines = File.ReadAllLines("timedata.csv");
            _timeMeasurements = new List<TimeMeasurement>();
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var bibNumber = int.Parse(parts[0]);
                var time = TimeOnly.Parse(parts[1]);
                var km = parts[2];
                var timeMeasurement = GetTimeMeasurement(bibNumber);
                if (km == "0") timeMeasurement.TimeAtStart = time;
                else if (km == "5") timeMeasurement.TimeAtStart = time;
                else if (km == "10") timeMeasurement.TimeAtStart = time;
            }
        }

        public void ShowReport()
        {
            var finished = new List<TimeMeasurement>();
            foreach (var timeMeasurement in _timeMeasurements)
            {
                if (timeMeasurement.TimeAt10k != null)
                {
                    finished.Add(timeMeasurement);
                }
            }
            finished.Sort((tmA, tmB) =>
            {
                var elapsedA = tmA.TimeAt10k.Value.ToTimeSpan() - tmA.TimeAtStart.Value.ToTimeSpan();
                var elapsedB = tmB.TimeAt10k.Value.ToTimeSpan() - tmB.TimeAtStart.Value.ToTimeSpan();
                return Convert.ToInt32(elapsedB.TotalMilliseconds - elapsedA.TotalMilliseconds);
            });
            foreach (var timeMeasurement in finished)
            {
                var elapsed = timeMeasurement.TimeAt10k.Value.ToTimeSpan()
                              - timeMeasurement.TimeAtStart.Value.ToTimeSpan();
                Console.WriteLine(elapsed);
            }
        }

        public TimeMeasurement GetTimeMeasurement(int bibNumber)
        {
            foreach (var tm in _timeMeasurements)
            {
                if (tm.BibNumber == bibNumber) return tm;
            }

            var timeMeasurement = new TimeMeasurement { BibNumber = bibNumber };
            _timeMeasurements.Add(timeMeasurement);
            return timeMeasurement;
        }
    }
}
