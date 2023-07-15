using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Model;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;

namespace Furnituremarket.DAL.Repositories
{
    public class FurnitureRepository : IFurnitureRepository
    {
        public async Task<bool> Create(Furniture entity)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("CreateFurniture",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"nameFurniture", entity.Name);
                    command.Parameters.AddWithValue($"descriptionFurniture", entity.Description);
                    command.Parameters.AddWithValue($"colorFurniture", entity.Color);
                    command.Parameters.AddWithValue($"materialFurniture", entity.Material);
                    command.Parameters.AddWithValue($"priceFurniture", entity.Price);
                    command.Parameters.AddWithValue($"dateCreateFurniture",
                        entity.DateCreate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue($"imageFurniture", entity.Image);
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

        public async Task<List<Furniture>> Read()
        {
            List<Furniture> listFurniture = new List<Furniture>();

            try
            {
                using (MySqlCommand command = new MySqlCommand("GetFurnitureFull",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listFurniture.Add(
                                    new Furniture(
                                        reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetString(4),
                                        reader.GetDecimal(5),
                                        reader.GetDateTime(6),
                                        reader.GetString(7)));
                            }
                        }
                        reader.Close();
                        return await Task.Run(() =>
                        {
                            return listFurniture;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return await Task.Run(() =>
                {
                    return listFurniture;
                });
            }
            finally
            {
                ConnectionDataBase.GetInstance.CloseConnection();
            }
        }

        public async Task<bool> Update(Furniture entity)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("UpdateFurniture",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"nameFurniture", entity.Name);
                    command.Parameters.AddWithValue($"descriptionFurniture", entity.Description);
                    command.Parameters.AddWithValue($"colorFurniture", entity.Color);
                    command.Parameters.AddWithValue($"materialFurniture", entity.Material);
                    command.Parameters.AddWithValue($"priceFurniture", entity.Price);
                    command.Parameters.AddWithValue($"dateCreateFurniture",
                        entity.DateCreate.ToString("yyyy-MM-dd"));
                    command.Parameters.AddWithValue($"imageFurniture", entity.Image);
                    command.Parameters.AddWithValue($"idFurniture", entity.Id);
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

        public async Task<bool> Delete(Furniture entity)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand("DeleteFurnitureId",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"idFurniture", entity.Id);
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
                return await Task.Run(() =>
                {
                    return false;
                });
            }
            finally
            {
                ConnectionDataBase.GetInstance.CloseConnection();
            }
        }

        public async Task<List<Furniture>> GetByQuery(string query)
        {
            List<Furniture> listFurniture = new List<Furniture>();

            try
            {
                using (MySqlCommand command = new MySqlCommand("GetFurnitureQuery",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"queryFurniture", query);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                listFurniture.Add(new Furniture(
                                        reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetString(4),
                                        reader.GetDecimal(5),
                                        reader.GetDateTime(6),
                                        reader.GetString(7)));
                            }
                        }
                        reader.Close();
                        return await Task.Run(() =>
                        {
                            return listFurniture;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return await Task.Run(() =>
                {
                    return listFurniture;
                });
            }
            finally
            {
                ConnectionDataBase.GetInstance.CloseConnection();
            }
        }

        public async Task<Furniture> GetById(int id)
        {
            Furniture furniture = new();

            try
            {
                using (MySqlCommand command = new MySqlCommand("GetFurnitureId",
                ConnectionDataBase.GetInstance.GetConnection()))
                {
                    ConnectionDataBase.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"idFurniture", id);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                furniture = new Furniture(
                                        reader.GetInt32(0),
                                        reader.GetString(1),
                                        reader.GetString(2),
                                        reader.GetString(3),
                                        reader.GetString(4),
                                        reader.GetDecimal(5),
                                        reader.GetDateTime(6),
                                        reader.GetString(7));
                            }
                        }
                        reader.Close();
                        return await Task.Run(() =>
                        {
                            return furniture;
                        });
                    }
                }
            }
            catch (Exception)
            {
                return await Task.Run(() =>
                {
                    return furniture;
                });
            }
            finally
            {
                ConnectionDataBase.GetInstance.CloseConnection();
            }
        }

    }
}
