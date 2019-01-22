using Dapper;
using Dapper.Contrib.Extensions;
using fx.Domain.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.Data.Dapper
{
    public class DbContext
    {
        /// <summary>
        /// DB Connetction String
        /// </summary>
        private static readonly string connectionString = "";
        /// <summary>
        /// Get Entity (int key)
        /// </summary> 
        public static async Task<T> GetAsync<T, TKey>(TKey id, IDbTransaction transaction = null, int? commandTimeout = null) where T : AggregateRoot<TKey>
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    return await conn.GetAsync<T>(id, transaction, commandTimeout);
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }        

       
        /// <summary>
        /// Get All List
        /// </summary> 
        public static async Task<IEnumerable<T>> GetAllAsync<T, TKey>() where T : AggregateRoot<TKey>
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    return await conn.GetAllAsync<T>();
                }

                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }

        public static IEnumerable<T> GetAll<T, TKey>() where T : AggregateRoot<TKey>
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    return conn.GetAll<T>();
                }

                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Get List With SQL
        /// </summary> 
        public static async Task<IEnumerable<T>> GetListAsync<T>(string sql) where T : class, new()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    return await conn.QueryAsync<T>(sql, commandType: CommandType.Text);
                }

                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Insert Entity
        /// </summary>
        public static async Task<int> InsertAsync<T, TKey>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : AggregateRoot<TKey>
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    return await conn.InsertAsync<T>(model, transaction, commandTimeout);
                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Update Entity
        /// </summary>
        public static async Task<int> UpdateAsync<T, TKey>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : AggregateRoot<TKey>
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {

                    bool b = await conn.UpdateAsync<T>(model, transaction, commandTimeout);
                    if (b) { return 1; }
                    else { return 0; }

                }
                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        /// <summary>
        /// Delete Entity
        /// </summary>
        public static async Task<T> DeleteAsync<T>(T model, IDbTransaction transaction = null, int? commandTimeout = null) where T : class, new()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    bool b = await conn.DeleteAsync<T>(model, transaction, commandTimeout);
                    if (b) { return model; }
                    else { return null; }

                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
        /// <summary>
        ///Execute SQL Statement
        /// </summary> 
        public static async Task<int> ExecSqlAsync<T>(string sql)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                try
                {
                    return await conn.ExecuteAsync(sql);
                }

                catch (Exception ex)
                {
                    return 0;
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                    {
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
        }
    }
}
