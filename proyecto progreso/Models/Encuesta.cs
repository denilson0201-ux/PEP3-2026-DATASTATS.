using System;
using System.Collections.Generic;
using System.Text;


namespace DataStats.Models
{
    public class Encuesta
    {
        // Pregunta 1
        public string Proveedor { get; set; } = string.Empty;

        // Pregunta 2
        public decimal PagoMensual { get; set; }

        // Pregunta 3
        public int Velocidad { get; set; }

        // Pregunta 4
        public string Satisfaccion { get; set; } = string.Empty;

        // Pregunta 5
        public int Estabilidad { get; set; }

        // Pregunta 6
        public bool TieneProblemas { get; set; }

        // Pregunta 7
        public string Recomendacion { get; set; } = string.Empty;

        // Pregunta 8
        public string AspectoMejora { get; set; } = string.Empty;

        // Fecha en que se registra la encuesta
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
