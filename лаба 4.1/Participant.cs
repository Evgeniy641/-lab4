using System;
using System.Linq;

namespace LabProject
{
    /// <summary>
    /// Участник олимпиады
    /// </summary>
    public class Participant
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int[] Scores { get; set; }
        public int TotalScore => Scores.Sum(); // Сумма баллов

        public Participant(string lastName, string firstName, int[] scores)
        {
            LastName = lastName;
            FirstName = firstName;
            Scores = scores ?? throw new ArgumentNullException(nameof(scores));
        }

        public override string ToString() => $"{LastName} {FirstName} {TotalScore}";

        public string GetDetailedInfo() => $"{LastName} {FirstName} [{string.Join(" + ", Scores)} = {TotalScore}]";
    }
}