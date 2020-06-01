using BookShop.Domain.Entities;
using BookShop.Domain.Repositories;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BookShop.DapperRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly SqlConnection _sqlConnection;

        public IUnitOfWork UnitOfWork => throw new NotImplementedException();

        public BookRepository(string connectionString)
        {
            _sqlConnection = new SqlConnection(connectionString);
        }
        public async Task<IEnumerable<Book>> GetAsync()
        {
            var result = await _sqlConnection.QueryAsync<Book>
            ("GetAllBooks", commandType:
            CommandType.StoredProcedure);
            return result.AsList();
        }
        public async Task<Book> GetAsync(Guid id)
        {
            return await _sqlConnection.ExecuteScalarAsync<Book>
            ("GetAllBooks", new { Id = id.ToString() }, commandType:
            CommandType.StoredProcedure);
        }
        public Book Add(Book order)
        {
            var result = _sqlConnection.ExecuteScalar<Book>
            ("InsertBook", order, commandType: CommandType.StoredProcedure);
            return result;
        }
        public Book Update(Book Book)
        {
            var result = _sqlConnection.ExecuteScalar<Book>
            ("UpdateBook", Book, commandType:
            CommandType.StoredProcedure);
            return result;
        }
        public Book Delete(Book Book)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBookByAuthorIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Book>> GetBookByGenreIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
