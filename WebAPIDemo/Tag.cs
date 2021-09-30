using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDemo
{
    public class Tag
    {
        [Key]
        public string Source { get; set; }

        public string Message { get; set; }

        public string Font { get; set; }

        public double Rotation { get; set; }
    }
}
