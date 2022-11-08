﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PresMed.Models.ViewModels {
    public class PrescriptionViewModel {
        public int AttendanceId { get; set; }
        public Person Patient { get; set; }
        public Prescription Prescription { get; set; }
        public List<Medicine> Medicines { get; set; }
        public List<Prescription> Prescriptions { get; set; }

        public PrescriptionViewModel() { }
    }
}
