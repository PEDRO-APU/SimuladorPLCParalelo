using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TareasPLC
{
    public static class Tarea
    {
        
        public static int [,] obtenerArchivo2()
        {
            int[,] mat1 = new int[7, 3];
            using (StreamReader leer = new StreamReader("archivo2.csv"))
            {

                string linea = null;

                int i = 0;

                while (null != (linea = leer.ReadLine()))
                {

                 
                       
                        string[] valor = linea.Split('-', ';');
                         

                        mat1[i, 0] = Convert.ToInt32(valor[0]);
                        mat1[i, 1] = Convert.ToInt32(valor[1]);
                        mat1[i, 2] = Convert.ToInt32(valor[2]);
                        i++;


                    
                }

            }
            return mat1;
        }

        public static Dictionary<int, int> obtenerListaConcatenada(Dictionary<int, int> lista1A, Dictionary<int, int> lista1B)
        {
           // Dictionary<int, int> lista1A = obtenerArchivo1a();
           
                
          //  Dictionary<int, int> lista1B = obtenerArchivo1b();

            Dictionary<int, int> listaConcatenada = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> parA in lista1A)
            {
                if (!listaConcatenada.ContainsKey(parA.Key))
                {
                    listaConcatenada.Add(parA.Key,parA.Value);
                }
            }
            foreach (KeyValuePair<int, int> parA in lista1B)
            {
                if (!listaConcatenada.ContainsKey(parA.Key))
                {
                    listaConcatenada.Add(parA.Key, parA.Value);
                }
                else
                {
                   listaConcatenada[parA.Key] += parA.Value;
                    //Console.WriteLine("encontro : " + parA.Key + " " + parA.Value);
                }
            }
            return listaConcatenada;
        }
   
        public static int obtenerCociente(int[,] mat, int flag)
        {
                int cociente = 0;
                int coeficiente = 0;
                int[,] matriz = obtenerArchivo2();
          
            
            for(int fila=0; fila < 7; fila++)
            {

                    if(mat[fila, 0] < flag && flag < mat[fila, 1])
                    {
                        coeficiente = mat[fila, 2];
                        break;
                    }               
            }
            try
            {
                cociente = flag / coeficiente;
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Esta intentando dividir: {0}  y  {1}", flag, coeficiente);
            }
            

            return cociente;
        }
        public static Dictionary<int, int> obtenerArchivo1b()
        {
            Dictionary<int, int> listaSecundaria = new Dictionary<int, int>();
            using (StreamReader leer = new StreamReader("archivo1b.csv"))
            {
                string linea = null;
                while (null != (linea = leer.ReadLine()))
                {
                    string[] valor = linea.Split(';', '|');
                    int codigo = Convert.ToInt32(valor[0]);

                    int sum = Convert.ToInt32(valor[1]) + Convert.ToInt32(valor[2]) + Convert.ToInt32(valor[3]);
                    if (listaSecundaria.ContainsKey(codigo))
                    {
                        listaSecundaria[codigo] += sum;
                        // listaSecundaria.Add(codigo, sum);
                    }
                    if (!listaSecundaria.ContainsKey(codigo))
                    {
                        listaSecundaria.Add(codigo, sum);
                    }
                }

            }
            return listaSecundaria;
        } 
        public static Dictionary<int,int> obtenerArchivo1a()
        {
            Dictionary<int, int> lista = new Dictionary<int, int>();            
            using (StreamReader leer = new StreamReader("archivo1a.csv"))
            {
                string linea = null;
                while (null != (linea = leer.ReadLine()))
                {
                    string[] valor = linea.Split(';','|');
                    int codigo = Convert.ToInt32(valor[0]);

                    int sum = Convert.ToInt32(valor[1]) + Convert.ToInt32(valor[2]) + Convert.ToInt32(valor[3]);
                    if (lista.ContainsKey(codigo))
                    {
                        lista[codigo] += sum;
                        // listaSecundaria.Add(codigo, sum);
                    }
                    if (!lista.ContainsKey(codigo))
                    {
                        lista.Add(codigo, sum);
                    }

                }

            }
           
            
           // lista = lista.Concat(listaSecundaria).ToDictionary(x => x.Key, x => x.Value);
            return lista;
        }


    }
}
