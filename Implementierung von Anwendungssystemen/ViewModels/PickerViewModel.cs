using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace Implementierung_von_Anwendungssystemen.ViewModels
{
    public class PickerViewModel
    {
        public List<University> UniversitiesList { get; set; }

        public PickerViewModel()
        {
            UniversitiesList = GetUniversities().OrderBy(keySelector: t => t.Value).ToList();
        }

        public List<University> GetUniversities()
        {
            var Universities = new List<University>()
            {
                new University(){Key = 1, Value = "Siegen"},
                new University(){Key = 2, Value = "edfawed"},
                new University(){Key = 3, Value = "awdawd"},
            };
            return Universities;
        }
    }
    public class University
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}
