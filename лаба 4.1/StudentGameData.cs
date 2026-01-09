using System.Collections.Generic;

namespace LabProject
{
    /// <summary>
    /// Данные об играх студента
    /// </summary>
    public class StudentGameData
    {
        public string StudentName { get; set; }
        public HashSet<string> Games { get; set; }

        public StudentGameData(string name)
        {
            StudentName = name;
            Games = new HashSet<string>();
        }
    }
}