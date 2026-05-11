using Lab4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.DTOs
{
    internal class PerformanceDto
    {
        public WorkType WorkType { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }   
        public PerformerDto Performer { get; set; }
    }   
}
