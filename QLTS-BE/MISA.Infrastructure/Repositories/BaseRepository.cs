using Dapper;
using MISA.Core.Entities;
using MISA.Core.Interfaces.Repositories;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure.Repositories
{
    /// <summary>
    /// BaseRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// CreatedBy: hadm (11/11/2021)
    /// ModifiedBy: null
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable
    {
        #region Declare
        protected string _tableName;
        protected string _connectString;
        protected IDbConnection _dbConnection;
        #endregion

        #region Property

        #endregion

        #region Constructor
        public BaseRepository()
        {
            _tableName = typeof(T).Name;
            _connectString = "" +
            "Host = 47.241.69.179;" +
            "Port = 3306;" +
            "Database =  MISA.WEB09.DMHA;" +
            "User Id = dev;" +
            "Password = manhmisa";
            //_connectString = "" +
            //"Host = localhost;" +
            //"Port = 3306;" +
            //"Database =  MISA.WEB09.DMHA;" +
            //"User Id = root;" +
            //"Character Set=utf8";
            _dbConnection = new MySqlConnection(_connectString);
        }
        #endregion

        #region Method
        /// <summary>
        /// Xóa một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public virtual int Delete(Guid entityId)
        {
            string sqlCommand = $"Proc_Delete{_tableName}ById";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@${_tableName}Id", entityId.ToString());
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                int rowEffects = _dbConnection.Execute(sqlCommand, param: dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return rowEffects;
            }
        }

        /// <summary>
        /// Lấy tất cả bản ghi
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public virtual IEnumerable<T> GetAll()
        {
            var sqlCommand = $"Proc_Get{_tableName}s";

            return _dbConnection.Query<T>(sqlCommand, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy một bản ghi theo Id
        /// </summary>
        /// <param name="entityId">Id của entity</param>
        /// <returns>Entity</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public virtual T GetById(Guid entityId)
        {
            string sqlCommand = $"Proc_Get{_tableName}ById";
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@${_tableName}Id", entityId.ToString());

            return _dbConnection.QueryFirstOrDefault<T>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy một bản ghi theo giá trị cột
        /// </summary>
        /// <param name="column">cột</param>
        /// <param name="value">giá trị cột</param>
        /// <param name="id">entity Id</param>
        /// <returns>entity</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public T GetByProperty(string column, string value, string id)
        {
            string sqlCommand = $"Proc_Get{_tableName}By{column}";

            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add($"@${column}", value);
            dynamicParameters.Add($"@${_tableName}Id", id);


            return _dbConnection.QueryFirstOrDefault<T>(sqlCommand, param: dynamicParameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public virtual int Insert(T entity)
        {
            string sqlCommand = $"Proc_Insert{_tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if(prop.IsDefined(typeof(Obj), true))
                {
                    continue;
                }
                var value = prop.GetValue(entity) == "" ? null : prop.GetValue(entity);
                dynamicParameters.Add($"@${prop.Name}", value);
            }

            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                int rowAffects = _dbConnection.Execute(sqlCommand, param: dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return rowAffects;
            }
        }

        /// <summary>
        /// Sửa một bản ghi theo Id
        /// </summary>
        /// <param name="entity">Id của entity</param>
        /// <param name="entityId">Entity</param>
        /// <returns>Số dòng ảnh hưởng trong DB</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public virtual int Update(T entity, Guid entityId)
        {
            string sqlCommand = $"Proc_Update{_tableName}";
            DynamicParameters dynamicParameters = new DynamicParameters();

            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (prop.IsDefined(typeof(Obj), true))
                {
                    continue;
                }
                dynamicParameters.Add($"@${prop.Name}", prop.GetValue(entity));
            }
            dynamicParameters.Add($"@${_tableName}Id", entityId.ToString());

            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                int rowAffects = _dbConnection.Execute(sqlCommand, param: dynamicParameters, transaction, commandType: CommandType.StoredProcedure);
                transaction.Commit();
                return rowAffects;
            }
        }

        /// <summary>
        /// Xóa nhiều
        /// </summary>
        /// <param name="listId">danh sách id cần xóa</param>
        /// <returns>sô dòng bị xóa</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public async Task<int> DeleteMulti(string[] listId)
        {
            int affectedRow = 0;
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                DynamicParameters dynamicParameters = new DynamicParameters();
                string sql = $"DELETE FROM {_tableName} WHERE {_tableName}Id IN @ListId;";

                dynamicParameters.Add($"@ListId", listId);

                affectedRow = await _dbConnection.ExecuteAsync(sql, param: dynamicParameters, transaction: transaction);

                if (affectedRow < listId.Length)
                {
                    transaction.Rollback();
                }
                else
                {
                    transaction.Commit();
                }
            }
            return affectedRow;
        }



        // <summary>
        /// Lấy mã mới nhất
        /// </summary>
        /// <returns>Mã TS mới nhất</returns>
        /// CreatedBy: hadm (27/8/2021)
        /// ModifiedBy: null
        public string GetLastCode()
        {
            string sqlCommand = $"Proc_GetLastest{_tableName}Code";
            return _dbConnection.QueryFirstOrDefault<string>(sqlCommand, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Lấy ra Id gần đây nhất
        /// </summary>
        /// <returns>last id</returns>
        /// CreatedBy: hadm (11/11/2021)
        /// ModifiedBy: null
        public Guid GetLastId()
        {
            string sqlCommand = $"Proc_GetLastest{_tableName}Id";
            return _dbConnection.QueryFirstOrDefault<Guid>(sqlCommand, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Đóng connection
        /// </summary>
        /// CreatedBy: hadm (28/8/2021)
        /// ModifiedBy: null
        public void Dispose()
        {
            if (_dbConnection.State == ConnectionState.Open)
            {
                _dbConnection.Close();
            }
        }
        #endregion
    }
}
