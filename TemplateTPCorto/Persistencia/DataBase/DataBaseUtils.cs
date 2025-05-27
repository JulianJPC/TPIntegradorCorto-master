using Datos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Persistencia.DataBase
{
    public class DataBaseUtils
    {
        private string archivoCsv { get;set ; }
        public DataBaseUtils()
        {
            archivoCsv = Directory.GetCurrentDirectory();
            archivoCsv = Path.GetDirectoryName(archivoCsv);
            archivoCsv = Path.GetDirectoryName(archivoCsv);
            archivoCsv = Path.GetDirectoryName(archivoCsv);
            archivoCsv = Path.Combine(archivoCsv, "Persistencia", "DataBase", "Tablas");
        }
        


        private bool isTheLine(string line, int columnIndex, string columnValue)
        {
            var response = false;
            var lineInParts = line.Split(';').ToList();
            if (lineInParts[columnIndex] == columnValue)
            {
                response = true;
            }
            return response;
        }
        public List<string> BuscarRegistro(string columnValue, int columnToSearch, string nameTable)
        {
            var response = new List<string>();
            var theFile = Path.Combine(archivoCsv, nameTable); // Cambia esta ruta al archivo CSV que deseas leer
            try
            {
                using (StreamReader sr = new StreamReader(theFile))
                {
                    string linea;
                    var firstLine = sr.ReadLine();//salta primera linea

                    while ((linea = sr.ReadLine()) != null)
                    {
                        if(isTheLine(linea, columnToSearch, columnValue))
                        {
                            response.Add(linea);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo leer el archivo:\n" + e.Message);
            }
            return response;
        }
        public List<String> getAllRegistro(string nameTable)
        {
            List<String> response = new List<String>();
            var theFile = Path.Combine(archivoCsv, nameTable); // Cambia esta ruta al archivo CSV que deseas leer
            try
            {
                using (StreamReader sr = new StreamReader(theFile))
                {
                    string linea;
                    var firstLine = sr.ReadLine();//salta primera linea

                    while ((linea = sr.ReadLine()) != null)
                    {
                        var lineInParts = linea.Split(';').ToList();
                        response.Add(linea);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("No se pudo leer el archivo:");
                Console.WriteLine(e.Message);
            }
            return response;
        }
        public void ModificarRegistro(string newRow, String nameTable, int keyColumn, string keyValue)
        {
            var theFile = Path.Combine(archivoCsv, nameTable); // Cambia esta ruta al archivo CSV que deseas leer
            var newText = "";
            try
            {
                
                using (StreamReader sr = new StreamReader(theFile))
                {
                    string linea;
                    var firstLine = sr.ReadLine();//salta primera linea
                    newText += firstLine + "\n";
                    while ((linea = sr.ReadLine()) != null)
                    {
                        var newLine = linea;
                        var lineInParts = linea.Split(';').ToList();
                        if (lineInParts[keyColumn] == keyValue)
                        {
                            newLine = newRow;
                        }
                        newText += newLine + "\n";
                    }
                }
                File.Delete(theFile);
                File.WriteAllText(theFile, newText.ToString());
            }

            catch (Exception e)
            {
                Console.WriteLine("No se pudo leer el archivo:");
                Console.WriteLine(e.Message);
            }
        }
        public void BorrarRegistro(String nameTable, int keyColumn, string keyValue)
        {
            var theFile = Path.Combine(archivoCsv, nameTable); // Cambia esta ruta al archivo CSV que deseas leer
            var newText = "";
            try
            {
                using (StreamReader sr = new StreamReader(theFile))
                {
                    string linea;
                    var firstLine = sr.ReadLine();//salta primera linea
                    newText += firstLine + "\n";
                    while ((linea = sr.ReadLine()) != null)
                    {
                        var newLine = linea;
                        var lineInParts = linea.Split(';').ToList();
                        if (lineInParts[keyColumn] == keyValue)
                        {
                            newLine = "";
                        }
                        newText += newLine + "\n";
                    }
                }
                File.Delete(theFile);
                File.WriteAllText(theFile, newText.ToString());
            }

            catch (Exception e)
            {
                Console.WriteLine("No se pudo leer el archivo:");
                Console.WriteLine(e.Message);
            }
        }

        // Método para borrar un registro
        //public void BorrarRegistro(string id, String nombreArchivo)
        //{
        //    archivoCsv = archivoCsv + nombreArchivo; // Cambia esta ruta al archivo CSV que deseas leer

        //    String rutaArchivo = Path.GetFullPath(archivoCsv); // Normaliza la ruta

        //    try
        //    {
        //        // Verificar si el archivo existe
        //        if (!File.Exists(rutaArchivo))
        //        {
        //            Console.WriteLine("El archivo no existe: " + archivoCsv);
        //            return;
        //        }

        //        // Leer el archivo y obtener las líneas
        //        List<string> listado = BuscarRegistro(nombreArchivo);

        //        // Filtrar las líneas que no coinciden con el ID a borrar (comparar solo la primera columna)
        //        var registrosRestantes = listado.Where(linea =>
        //        {
        //            var campos = linea.Split(';');
        //            return campos[0] != id; // Verifica solo el ID (primera columna)
        //        }).ToList();

        //        // Sobrescribir el archivo con las líneas restantes
        //        File.WriteAllLines(archivoCsv, registrosRestantes);

        //        Console.WriteLine($"Registro con ID {id} borrado correctamente.");
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("Error al intentar borrar el registro:");
        //        Console.WriteLine($"Mensaje: {e.Message}");
        //        Console.WriteLine($"Pila de errores: {e.StackTrace}");
        //    }
        //}

        // Método para agregar un registro
        public void AgregarRegistro(string nameTable, string nuevoRegistro)
        {
            var theFile = Path.Combine(archivoCsv, nameTable);
            try
            {
                // Verificar si el archivo existe
                if (!File.Exists(theFile))
                {
                    Console.WriteLine("El archivo no existe: " + theFile);
                    return;
                }

                // Abrir el archivo y agregar el nuevo registro
                using (StreamWriter sw = new StreamWriter(theFile, append: true))
                {
                    sw.WriteLine(nuevoRegistro); // Agregar la nueva línea
                }

                Console.WriteLine("Registro agregado correctamente.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al intentar agregar el registro:");
                Console.WriteLine($"Mensaje: {e.Message}");
                Console.WriteLine($"Pila de errores: {e.StackTrace}");
            }
        }

    }
}
