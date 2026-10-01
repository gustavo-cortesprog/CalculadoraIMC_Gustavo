using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TuProyecto.Pages
{
    public class CalculadoraIMCModel : PageModel
    {
        // Lista donde se almacenan las consultas
        public List<Consulta> Consultas { get; set; } = new List<Consulta>();

        // Variables para recibir los datos del usuario
        [BindProperty]
        public string Nombre { get; set; }

        [BindProperty]
        public double Peso { get; set; }

        [BindProperty]
        public double Estatura { get; set; }

        // Variables para mostrar el resultado
        public double IMC { get; set; }

        public string Clasificacion { get; set; } = "";

        public string Clase { get; set; } = "";

        public string Mensaje { get; set; } = "";

        // Variable para controlar el color del borde
        public string ColorBorde { get; set; } = "#2583bd";


        // Método que se ejecuta cuando se carga la página
        public void OnGet()
        {
        }


        // Función principal para calcular el IMC
        public void CalcularIMC()
        {
            // Verifica que los datos ingresados sean correctos
            if (string.IsNullOrWhiteSpace(Nombre) ||
                Peso <= 0 ||
                Estatura <= 0)
            {
                Mensaje = "Por favor, ingresa los datos correctos.";
                return;
            }


            // Fórmula para calcular el IMC
            IMC = Peso / (Estatura * Estatura);


            // Determina la clasificación y la clase CSS
            if (IMC < 18.5)
            {
                // Clasificación de bajo peso
                Clasificacion = "Bajo peso";

                // Clase CSS
                Clase = "bajo";

                // Color del borde
                ColorBorde = "orange";
            }
            else if (IMC < 25)
            {
                // Clasificación de peso normal
                Clasificacion = "Peso normal";

                // Clase CSS
                Clase = "normal";

                // Color del borde
                ColorBorde = "green";
            }
            else if (IMC < 30)
            {
                // Clasificación de sobrepeso
                Clasificacion = "Sobrepeso";

                // Clase CSS
                Clase = "sobrepeso";

                // Color del borde
                ColorBorde = "red";
            }
            else
            {
                // Clasificación de obesidad
                Clasificacion = "Obesidad";

                // Clase CSS
                Clase = "obesidad";

                // Color del borde
                ColorBorde = "#a00000";
            }


            // Mensaje que se mostrará en pantalla
            Mensaje = $"Hola {Nombre}<br>" +
                      $"Tu IMC es: {IMC:F2}<br>" +
                      $"Clasificación: {Clasificacion}";


            // Agrega la nueva consulta al principio de la lista
            Consultas.Insert(0, new Consulta
            {
                Nombre = Nombre,
                Estatura = Estatura,
                Peso = Peso,
                IMC = IMC,
                Clasificacion = Clasificacion,
                Clase = Clase
            });


            // Mantiene solamente las últimas 10 consultas
            if (Consultas.Count > 10)
            {
                Consultas.RemoveAt(Consultas.Count - 1);
            }
        }


        // Función para eliminar una consulta específica
        public void EliminarConsulta(int indice)
        {
            // Verifica que el índice sea válido
            if (indice >= 0 && indice < Consultas.Count)
            {
                // Elimina la consulta
                Consultas.RemoveAt(indice);
            }
        }


        // Función para limpiar los datos
        public void LimpiarDatos()
        {
            Nombre = "";
            Peso = 0;
            Estatura = 0;

            IMC = 0;
            Clasificacion = "";
            Clase = "";
            Mensaje = "";

            // Regresa el borde a su color original
            ColorBorde = "#2583bd";
        }


        // Función para limpiar toda la tabla
        public void LimpiarTabla()
        {
            // Vacía la lista de consultas
            Consultas.Clear();
        }
    }


    // Clase que representa una consulta
    public class Consulta
    {
        // Guarda el nombre
        public string Nombre { get; set; }

        // Guarda la estatura
        public double Estatura { get; set; }

        // Guarda el peso
        public double Peso { get; set; }

        // Guarda el IMC
        public double IMC { get; set; }

        // Guarda la clasificación
        public string Clasificacion { get; set; }

        // Guarda la clase CSS
        public string Clase { get; set; }
    }
}
