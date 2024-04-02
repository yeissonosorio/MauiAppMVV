using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiAppMVVM.Models;

namespace MauiAppMVVM.controller
{
    public class controladorproducto
    {
        readonly SQLiteAsyncConnection _connection;

        public controladorproducto()
        {
            SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite |
                                                SQLite.SQLiteOpenFlags.Create |
                                                SQLite.SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBProductos.db3"), extensiones);
            _connection.CreateTableAsync<Productsmodel>().Wait();

        }
        //Insertar
        public async Task<int> StoreSitios(Productsmodel pro)
        {

            if (pro.Id == 0)
            {
                return await _connection.InsertAsync(pro);
            }
            else
            {
                return await _connection.UpdateAsync(pro);
            }
        }
        //Obtener Lista
        public async Task<List<Productsmodel>> GetListPersons()
        {

            return await _connection.Table<Productsmodel>().ToListAsync();
        }

        public async Task<Productsmodel> GePerson(int pid)
        {

            return await _connection.Table<Productsmodel>().Where(i => i.Id == pid).FirstOrDefaultAsync();
        }
    }
}
