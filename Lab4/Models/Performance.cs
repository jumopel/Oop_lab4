using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Lab4.Models
{
     public class Performance
    {
        private Performer _performer;
        private WorkType _workType;
        private string _title;
        private int _duration;
        public Performer Performer { get => _performer; set => _performer = value ?? throw new ArgumentNullException(); }
        public WorkType WorkType { get => _workType; set => _workType = value; }
        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException("Назва не може бути порожньою") : value;
        }
        public int Duration
        {
            get => _duration;
            set => _duration = value <= 0 ? throw new ArgumentException("Тривалість має бути більшою за 0") : value;
        }
        public override string ToString()
        {
            return $"{_title} ({_workType}) - {_performer.FirstName} {_performer.LastName}, {_duration} хв.";
        }
    }
}

