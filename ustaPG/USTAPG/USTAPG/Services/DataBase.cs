namespace USTAPG.Services
{
    using SQLite;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using USTAPG.Models;

    public class DataBase
    {
        readonly SQLiteAsyncConnection _database;

        public DataBase(string dbPath)
        {
            //Establishing the conection
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Usuario>().Wait();
        }

        // Show the registers
        public async Task<List<Usuario>> GetPeopleAsync<U>() where U : new()
        {
            return await _database.Table<Usuario>().ToListAsync();
        }

        // Save registers
        public async Task<int> SavePersonAsync(Usuario contact)
        {
            return await _database.InsertAsync(contact);
        }

        // Delete registers
        public async Task<int> DeletePersonAsync(Usuario contact)
        {
            return await _database.DeleteAsync(contact);
        }

        // Save registers
        public async Task<int> UpdatePersonAsync(Usuario contact)
        {
            return await _database.UpdateAsync(contact);
        }

    }
}
