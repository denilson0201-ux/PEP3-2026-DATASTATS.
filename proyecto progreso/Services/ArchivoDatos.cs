using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using DataStats.Models;

namespace DataStats.Services
{
    public static class ArchivoDatos
    {
        // Ruta donde se almacenarán las encuestas.
        private static readonly string RutaArchivo =
            Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Data",
                "Encuestas.csv"
            );

        // Encabezados del archivo CSV.
        private static readonly string[] Encabezados =
        {
            "Proveedor",
            "PagoMensual",
            "Velocidad",
            "Satisfaccion",
            "Estabilidad",
            "TieneProblemas",
            "Recomendacion",
            "AspectoMejora",
            "FechaRegistro"
        };

        // Guarda una encuesta sin borrar las anteriores.
        public static void Guardar(Encuesta encuesta)
        {
            if (encuesta == null)
                throw new ArgumentNullException(nameof(encuesta));

            string? carpeta = Path.GetDirectoryName(RutaArchivo);

            if (string.IsNullOrEmpty(carpeta))
                throw new InvalidOperationException(
                    "No se pudo determinar la carpeta de almacenamiento."
                );

            Directory.CreateDirectory(carpeta);

            bool necesitaEncabezados =
                !File.Exists(RutaArchivo) ||
                new FileInfo(RutaArchivo).Length == 0;

            using (StreamWriter escritor = new StreamWriter(
                RutaArchivo,
                true,
                new UTF8Encoding(false)))
            {
                if (necesitaEncabezados)
                {
                    escritor.WriteLine(
                        string.Join(",", Encabezados)
                    );
                }

                string[] valores =
                {
                    encuesta.Proveedor,
                    encuesta.PagoMensual.ToString(
                        CultureInfo.InvariantCulture),
                    encuesta.Velocidad.ToString(
                        CultureInfo.InvariantCulture),
                    encuesta.Satisfaccion,
                    encuesta.Estabilidad.ToString(
                        CultureInfo.InvariantCulture),
                    encuesta.TieneProblemas.ToString(),
                    encuesta.Recomendacion,
                    encuesta.AspectoMejora,
                    encuesta.FechaRegistro.ToString(
                        "O", CultureInfo.InvariantCulture)
                };

                for (int i = 0; i < valores.Length; i++)
                {
                    valores[i] = EscaparCSV(valores[i]);
                }

                escritor.WriteLine(string.Join(",", valores));
            }
        }

        // Recupera todas las encuestas guardadas.
        public static List<Encuesta> LeerTodas()
        {
            List<Encuesta> encuestas = new List<Encuesta>();

            if (!File.Exists(RutaArchivo))
                return encuestas;

            string contenido = File.ReadAllText(
                RutaArchivo,
                Encoding.UTF8
            );

            List<string[]> filas = AnalizarCSV(contenido);

            if (filas.Count == 0)
                return encuestas;

            // Comprueba que el archivo tenga los encabezados correctos.
            if (filas[0].Length != Encabezados.Length)
            {
                throw new FormatException(
                    "El archivo CSV tiene encabezados incorrectos."
                );
            }

            for (int i = 0; i < Encabezados.Length; i++)
            {
                if (filas[0][i] != Encabezados[i])
                {
                    throw new FormatException(
                        "El formato del archivo CSV no es válido."
                    );
                }
            }

            // Convierte cada fila en un objeto Encuesta.
            for (int i = 1; i < filas.Count; i++)
            {
                string[] datos = filas[i];

                if (datos.Length != Encabezados.Length)
                {
                    throw new FormatException(
                        $"La fila {i + 1} del CSV no tiene " +
                        "la cantidad correcta de columnas."
                    );
                }

                Encuesta encuesta = new Encuesta
                {
                    Proveedor = datos[0],

                    PagoMensual = decimal.Parse(
                        datos[1],
                        CultureInfo.InvariantCulture
                    ),

                    Velocidad = int.Parse(
                        datos[2],
                        CultureInfo.InvariantCulture
                    ),

                    Satisfaccion = datos[3],

                    Estabilidad = int.Parse(
                        datos[4],
                        CultureInfo.InvariantCulture
                    ),

                    TieneProblemas = bool.Parse(datos[5]),

                    Recomendacion = datos[6],

                    AspectoMejora = datos[7],

                    FechaRegistro = DateTime.ParseExact(
                        datos[8],
                        "O",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.RoundtripKind
                    )
                };

                encuestas.Add(encuesta);
            }

            return encuestas;
        }

        // Protege los textos que contienen comas o comillas.
        private static string EscaparCSV(string valor)
        {
            if (valor.Contains('"'))
            {
                valor = valor.Replace("\"", "\"\"");
            }

            if (valor.Contains(',') ||
                valor.Contains('"') ||
                valor.Contains('\n') ||
                valor.Contains('\r'))
            {
                valor = "\"" + valor + "\"";
            }

            return valor;
        }

        // Interpreta las filas y columnas del archivo CSV.
        // También admite comas, comillas y saltos de línea.
        private static List<string[]> AnalizarCSV(string contenido)
        {
            List<string[]> filas = new List<string[]>();
            List<string> campos = new List<string>();
            StringBuilder campo = new StringBuilder();

            bool dentroDeComillas = false;

            for (int i = 0; i < contenido.Length; i++)
            {
                char caracter = contenido[i];

                if (dentroDeComillas)
                {
                    if (caracter == '"')
                    {
                        if (i + 1 < contenido.Length &&
                            contenido[i + 1] == '"')
                        {
                            campo.Append('"');
                            i++;
                        }
                        else
                        {
                            dentroDeComillas = false;
                        }
                    }
                    else
                    {
                        campo.Append(caracter);
                    }
                }
                else
                {
                    if (caracter == '"' && campo.Length == 0)
                    {
                        dentroDeComillas = true;
                    }
                    else if (caracter == ',')
                    {
                        campos.Add(campo.ToString());
                        campo.Clear();
                    }
                    else if (caracter == '\n' ||
                             caracter == '\r')
                    {
                        if (caracter == '\r' &&
                            i + 1 < contenido.Length &&
                            contenido[i + 1] == '\n')
                        {
                            i++;
                        }

                        campos.Add(campo.ToString());
                        campo.Clear();

                        filas.Add(campos.ToArray());
                        campos.Clear();
                    }
                    else
                    {
                        campo.Append(caracter);
                    }
                }
            }

            if (dentroDeComillas)
            {
                throw new FormatException(
                    "El archivo CSV contiene comillas sin cerrar."
                );
            }

            // Recupera la última fila aunque no tenga salto de línea.
            if (campos.Count > 0 || campo.Length > 0)
            {
                campos.Add(campo.ToString());
                filas.Add(campos.ToArray());
            }

            return filas;
        }
    }
}
