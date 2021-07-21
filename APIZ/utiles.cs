using System;
using System.IO;

namespace APIZ
{
    public class utiles
    {


        public void GenLog(string mensaje)
        {

            StreamWriter log = null;
            try
            {
                
                var carpetaTemporal = "logs";
                string archivoLog = @carpetaTemporal+"/"  + DateTime.Today.Day.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Year.ToString() + ".txt";
                
                if (!System.IO.File.Exists(archivoLog))
                {
                    log = new StreamWriter(archivoLog);
                }

                else
                {
                    log = System.IO.File.AppendText(archivoLog);
                }
                lock (log)
                {
                    log.WriteLine(DateTime.Now);
                    log.WriteLine(mensaje);
                    log.WriteLine();
                    log.Close();
                }
            }

            catch
            {
            }

            finally
            {
                if (log != null)
                {
                    log.Dispose();
                }
            }
        }

        #region GENERAL
        public void borrarCarpetaTemp(string carpeta, string nombreArchivo)
        {
            string ruta = System.Configuration.ConfigurationManager.AppSettings["App_Data_ruta"] + "/temporales/" + carpeta ;
            //DeleteFile(@ruta+ "\\"+archivoTemp);
            DeleteFile(@ruta + "/" + nombreArchivo);
            try { DeleteDirectory(@ruta); }
            catch(Exception e) { throw e; }
            
        }

        public  void CopyFile(string origPath, string destPath, bool overwrite)
        {
            try
            {
                if (System.IO.Path.GetExtension(destPath) == "")
                {
                    destPath = System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(origPath));
                }
                if (!System.IO.Directory.Exists(System.IO.Path.GetDirectoryName(destPath)))
                {
                    CreateEmptyDirectory(System.IO.Path.GetDirectoryName(destPath));
                }
                if (!System.IO.File.Exists(destPath))
                {
                    System.IO.File.Copy(origPath, destPath, true);
                }
                else
                {
                    if (overwrite == true)
                    {
                        DeleteFile(destPath);
                        System.IO.File.Copy(origPath, destPath, true);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Crear un directorio vacio
        /// </summary>
        public  void CreateEmptyDirectory(string fullPath)
        {
            if (!System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.CreateDirectory(fullPath);
            }
        }

        public  void DeleteDirectory(string fullPath)
        {
            if (System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.Delete(fullPath);
            }
        }
        /// <summary>
        /// Borrar un archivo
        /// </summary>
        public  void DeleteFile(string fullPath)
        {
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.FileInfo info = new System.IO.FileInfo(fullPath);
                info.Attributes = System.IO.FileAttributes.Normal;
                System.IO.File.Delete(fullPath);
            }
        }
        #endregion

    }
    
}