using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using PM2E12513.Models;

namespace PM2E12513.Controller
{
    public class BaseSQLite
    {
        readonly SQLiteAsyncConnection db;

        public BaseSQLite(String pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);

            db.CreateTableAsync<Ubicaciones>().Wait();
        }

        public Task<List<Ubicaciones>> ObtenerListaUbicaciones()
        {
            return db.Table<Ubicaciones>().ToListAsync();

        }
        public Task<Ubicaciones> ObtenerUbicacion(int pcodigo)
        {
            return db.Table<Ubicaciones>()
                .Where(i => i.codigo == pcodigo)
                .FirstOrDefaultAsync();
        }

        public Task<int> GrabarUbicacion(Ubicaciones ubicacion)
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

        public Task<int> EliminarUbicacion(Ubicaciones ubicacion)
        {
            return db.DeleteAsync(ubicacion);
        }
    }
}
