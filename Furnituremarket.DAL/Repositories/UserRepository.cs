using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Furnituremarket.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> Create(User entity)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("CreateUser",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"nameUser", entity.Name);
                    command.Parameters.AddWithValue($"passwordUser", entity.Password);
                    command.Parameters.AddWithValue($"roleUser", entity.Role);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        return await Task.Run(() =>
                        {
                            return true;
                        });
                    }
                    else
                    {
                        return await Task.Run(() =>
                        {
                            return false;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                ConnectionDataBase.GetInstance.CloseConnection();
            }
        }

        public async Task<User> GetByName(string name)
        {
            User user = new();

            try
            {
                using (MySqlCommand command = new MySqlCommand("GetByNameUser",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"nameUser", name);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                user = new User(
                                        reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetInt32(3));
                            }
                        }
                        reader.Close();
                        return await Task.Run(() =>
                        {
                            return user;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return await Task.Run(() =>
                {
                    return user;
                });
            }
            finally
            {
                ConnectionDataBase.GetInstance.CloseConnection();
            }
        }






        public Task<bool> Delete(User entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<User> GetById(int id)
        {
            throw new System.NotImplementedException();
        }


        public Task<List<Furniture>> Read()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Update(User entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
