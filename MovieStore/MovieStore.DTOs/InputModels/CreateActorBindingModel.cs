﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.DTOs.InputModels
{
    public class CreateActorBindingModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Photo { get; set; }

        [Required]
        public string IMDBProfile { get; set; }

    }
}
