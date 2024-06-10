using System.Numerics;

namespace RaceResults
{
    internal class RaceResults
    {
        public readonly List<TimeMeasurement> _timeMeasurements;

        public RaceResults()
        {
            var lines = File.ReadAllLines("timedata.csv");
            _timeMeasurements = new List<TimeMeasurement>();
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                var bibNumber = int.Parse(parts[0]);
                var timeMeasurement = GetTimeMeasurement(bibNumber);


                //new TimeMeasurement
                //{
                //    ,
                //    TimeAtStart = TimeOnly.Parse(parts[1]),

                //}
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
