﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace HashCode2019
{
    /// <summary>
    /// Provee la recuperación de los ficheros, su lectura en lineas de arrays de integer y el volcado 
    /// del fichero resultado (no implementado).
    /// </summary>
    public class FilesProvider
    {
        private Settings _config;

        /// <summary>
        /// Objeto de configuración de donde recuperar, ruta de la carpeta y nombres de ficheros.
        /// </summary>
        /// <param name="config"></param>
        public FilesProvider(Settings config)
        {
            _config = config;
        }

        public List<FileInfo> GetFiles()
        {
            var pathFiles = Directory.GetFiles(_config.DataDirectory).Where(x => _config.FilesName.Any(f => x.EndsWith(f)));
            var files = pathFiles.Select(p => new FileInfo(p)).ToList();

            return files;
        }

        public List<Photo> GetContentFile(FileInfo file)
        {
            var stream = file.OpenText();
            var photos = new List<Photo>();
            var count = 0;
            int rows = 0;

            while (!stream.EndOfStream)
            {
                count++;

                var line = stream.ReadLine();
                if (line.Split(' ').Count() == 1){
                    rows = Convert.ToInt32(line);
                    continue;
                }

                photos.Add(new Photo { Id = count, Orientation = line.Split(' ')[0], Tags = new List<string>(line.Split(' ').Skip(2)) });
            }

            if (photos.Count != rows)
                throw new ApplicationException("el numero de elementos indicados en el fichero, no coincide con el numero de elementos recuperados");

            return photos;
        }

        /// <summary>
        /// No implementado.
        /// TODO: pendiente de ver que pasar le para guardar.
        /// </summary>
        /// <returns></returns>
        public bool SaveFileOutput()
        {
            throw new NotImplementedException();
        }
    }
}
