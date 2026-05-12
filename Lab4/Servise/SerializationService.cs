using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Lab4.DTOs;
using Lab4.Models;

namespace Lab4.Services
{
    internal static class SerializationService
    {
        private const string FileName = "concerts_data.json";

        public static void SaveList(List<Concert> concerts)
        {
            var dtoList = concerts.Select(concert => new ConcertDto
            {
                Organizer = concert.Organizer,
                Date = concert.Date,
                Performances = concert.Performances.Select(p => new PerformanceDto
                {
                    Title = p.Title,
                    WorkType = p.WorkType,
                    Duration = p.Duration,
                    Performer = new PerformerDto
                    {
                        FirstName = p.Performer.FirstName,
                        LastName = p.Performer.LastName
                    }
                }).ToList()
            }).ToList();

            string jsonString = JsonSerializer.Serialize(dtoList, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FileName, jsonString);
        }

        public static List<Concert> LoadList()
        {
            if (!File.Exists(FileName)) return new List<Concert>();

            try
            {
                string jsonString = File.ReadAllText(FileName);
                var dtoList = JsonSerializer.Deserialize<List<ConcertDto>>(jsonString);

                var concertsList = new List<Concert>();

                if (dtoList != null)
                {
                    foreach (var dto in dtoList)
                    {
                        var concert = new Concert { Organizer = dto.Organizer, Date = dto.Date };
                        foreach (var pDto in dto.Performances)
                        {
                            var performance = new Performance
                            {
                                Title = pDto.Title,
                                WorkType = pDto.WorkType,
                                Duration = pDto.Duration,
                                Performer = new Performer
                                {
                                    FirstName = pDto.Performer.FirstName,
                                    LastName = pDto.Performer.LastName
                                }
                            };
                            concert.AddPerformance(performance);
                        }
                        concertsList.Add(concert);
                    }
                }
                return concertsList;
            }
            catch
            {
                return new List<Concert>(); 

            }
        }
    }
}   