using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasPLC
{
    class Program
    {
        private static Stopwatch Stop1;
        private static Stopwatch Stop2;
        public static void Main(string[] args)
        {
            Stop1 = Stopwatch.StartNew();
                Console.WriteLine("Punto 1 - obteniendo archivo 2 ...");
                Task<int[,]> t1 = Task.Factory.StartNew<int[,]>(() =>
                {
                    return Tarea.obtenerArchivo2();
                });
                
                Console.WriteLine("Punto 2 - Obteniendo archivos 1a y 1b ...");
                Task<Dictionary<int, int>> t2 = Task.Factory.StartNew<Dictionary<int, int>>(() =>
                {
                    return Tarea.obtenerArchivo1a();
                });
                Task<Dictionary<int, int>> t3 = Task.Factory.StartNew<Dictionary<int, int>>(() =>
                {
                    return Tarea.obtenerArchivo1b();
                });
            
                Dictionary<int, int> lista = Tarea.obtenerListaConcatenada(t2.Result, t3.Result);
            Stop1.Stop();
            Console.WriteLine("Punto 3 ");
            Stop2 = Stopwatch.StartNew();
                Parallel.ForEach(lista, i =>
                    {
                        int cociente = Tarea.obtenerCociente(t1.Result, i.Value);
                        Console.WriteLine("{0}  :  {1}  :  {2}", i.Key, i.Value, cociente);

                    });
               
            Stop2.Stop();
            Task.WaitAll(new Task[] { t1, t2, t3 });
            Console.WriteLine("Todas las tareas 1,2,3 paralelas demoraron: {0}",Stop1.ElapsedMilliseconds);
            Console.WriteLine("El paralell.for  demoraro: {0}", Stop1.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
