using Lab4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4.DTOs
{
    internal class ConcertDto
    {
         public string Organizer { get; set; }
        public DateTime Date { get; set; }
        public List<PerformanceDto> Performances { get; set; } = new List<PerformanceDto>();
    }
}
