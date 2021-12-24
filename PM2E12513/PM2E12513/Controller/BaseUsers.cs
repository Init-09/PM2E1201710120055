using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using PM2E12513.Models1;

namespace PM2E12513.Controller
{
    public class BaseUsers
    {
        readonly SQLiteAsyncConnection db;

        //constructor de la clase DataBaseSQLite
        public BaseUsers(String pathdb)
        {
            //crear una conexion a la base de datos
            db = new SQLiteAsyncConnection(pathdb);

            //creacion de la tabla personas dentro de SQLite
            db.CreateTableAsync<Users>().Wait();
        }

        //opaciones CRUD con SQLite
        //READ List Way
        public Task<List<Users>> ObtenerListaUbicaciones()
        {
            return db.Table<Users>().ToListAsync();

        }

        public Task<Users> ObtenerUbicacion(int pcodigo)
        {
            return db.Table<Users>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GrabarUbicacion(Users ubicacion)
        {
            if (ubicacion.codigo != 0)
            {
                return db.UpdateAsync(ubicacion);
            }
            else
            {

                return db.InsertAsync(ubicacion);
            }
        }

        //delete
        public Task<int> EliminarUbicacion(Users ubicacion)
        {
            return db.DeleteAsync(ubicacion);
        }

    }
}