using Blazor.Contacts.Wasm.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.Contacts.Wasm.Repositories
{
    public class ContactRepository:IContactRepository
    {
        private readonly  IDbConnection _dbConnection;

        public  ContactRepository  (IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task DeleteContact(int id)
        {
            var sql = @"Delete from Contacts
                        where Id = @id
                       ";

            var result =await _dbConnection.ExecuteAsync(sql, new
            {
                Id = id
            });

          
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            var sql = @"Select Id, FirstName 
                                , LastName
                                , Phone
                                , Address
                        From Contacts
                       ";
            return await _dbConnection.QueryAsync<Contact>(sql, new { });
        }

        public async Task<Contact> GetDetails(int id)
        {
            var sql = @"Select 
                                Id
                                ,FirstName 
                                , LastName
                                , Phone
                                , Address
                         From Contacts
                         where id = @Id ";
            return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(sql, new
            {
                Id = id
            });

        }

        public async Task<bool> InsertContact(Contact contact)
        {
            try
            {
                var sql = @"INSERT INTO Contacts ( FirstName, LastName, Phone, Address) 
                                         Values  ( @FirstName, @LastName, @Phone, @Address) ";

                var result = await _dbConnection.ExecuteAsync(
                    sql, new
                    {
                        contact.FirstName,
                        contact.LastName,
                        contact.Phone,
                        contact.Address
                    }
                    );
                return result > 0;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public async Task<bool> UpdateContact(Contact contact)
        {
            try
            {
                var sql = @"Update Contacts SET FirstName =@FirstName
                                                , LastName=@LastName
                                                , Phone= @Phone
                                                , Address= @Address 
                            where id = @Id ";

                var result = await _dbConnection.ExecuteAsync(
                    sql, new
                    {

                        contact.FirstName,
                        contact.LastName,
                        contact.Phone,
                        contact.Address,
                        contact.Id 
                    }
                    );
                return result > 0;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
