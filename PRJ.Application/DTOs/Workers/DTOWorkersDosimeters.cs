using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRJ.Domain;
using ent = PRJ.Domain.Entities;

namespace PRJ.Application.DTOs.Workers
{
    public class DTOWorkersDosimeters
    {
        public string Id { get; set; }
        public int LineNum { get; set; }
        public string DosimeterType { get; set; }
        public string DosimeterID { get; set; }
        public bool ActiveFlag { get; set; }
        public Nullable<DateTime> StartWearDate { get; set; }
        public Nullable<DateTime> EndWearDate { get; set; }


    }
}
