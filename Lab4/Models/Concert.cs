using System;
using System.Collections.Generic;
using System.Text;
 using System.Linq;

namespace Lab4.Models
{
    public class Concert
    {
        private string _organizer;
        private DateTime _date;
        private List<Performance> _performances;
        public IReadOnlyList<Performance> Performances => _performances;
        public Concert()
        {
            _performances = new List<Performance>();
            _date = DateTime.Now;
        }
        public string Organizer
        {
            get => _organizer;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length <= 3)
                    throw new ArgumentException("Назва організатора має бути не менше 3 символів.");
                _organizer = value;
            }
        }
        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }
        public void AddPerformance(Performance performance)
        {
            if (performance == null) throw new ArgumentNullException(nameof(performance));
            _performances.Add(performance);
        }
        public override string ToString()
        {
            string info = $"Концерт організатора: {_organizer}\nДата: {_date.ToShortDateString()}\nВиступи:\n";
            foreach (var p in _performances)
            {
                info += $"- {p}\n";
            }
            return info;
        }
        public string ToShortString()
        {
            int totalDuration = _performances.Sum(p => p.Duration);
            return $"Організатор: {_organizer}, Загальна тривалість: {totalDuration} хв.";
        }
    }
}
