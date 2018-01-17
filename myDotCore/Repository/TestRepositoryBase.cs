using IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>

    public class TestRepositoryBase<T> : IRepository<T> where T : class
    {
        //定义数据访问上下文对象
        protected readonly TestDbContext _dbContext;

        /// <summary>
        /// 通过构造函数注入得到数据上下文对象实例
        /// </summary>
        /// <param name="dbContext"></param>
        public TestRepositoryBase(TestDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllList()
        {
            return _dbContext.Set<T>().ToList();
        }



        /// <summary>
        /// 返回IQueryable集合，延时加载数据（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<IQueryable<T>> LoadAllAsync(Expression<Func<T, bool>> predicate)
        {
            return predicate != null ? await Task.Run(() => _dbContext.Set<T>().Where(predicate).AsNoTracking<T>()) : await Task.Run(() => _dbContext.Set<T>().AsQueryable<T>().AsNoTracking<T>());
        }


        /// <summary>
        /// 根据lambda表达式条件获取实体集合
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public List<T> GetAllList(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate).ToList();
        }



        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate">lambda表达式条件</param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        /// <returns></returns>
        public bool Insert(T entity, bool autoSave = true)
        {
            _dbContext.Set<T>().Add(entity);
            if (autoSave)
                return Save();
            else return false;
        }


        public bool Update(T entity, bool autoSave = true)
        {
            _dbContext.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            if (autoSave)
                return Save();
            else return false;

        }



        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="autoSave">是否立即执行保存</param>
        public bool Delete(T entity, bool autoSave = true)
        {
            _dbContext.Set<T>().Remove(entity);
            if (autoSave)
                return Save();
            else return false;
        }



        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        public bool Delete(Expression<Func<T, bool>> where, bool autoSave = true)
        {
            _dbContext.Set<T>().Where(where).ToList().ForEach(it => _dbContext.Set<T>().Remove(it));
            if (autoSave)
                return Save();
            else return false;
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="startPage">页码</param>
        /// <param name="pageSize">单页数据数</param>
        /// <param name="rowCount">行数</param>
        /// <param name="where">条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public IQueryable<T> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<T, bool>> where = null, Expression<Func<T, object>> order = null)
        {
            var result = from p in _dbContext.Set<T>()
                         select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderByDescending(order);
            //else
            //    result = result.OrderBy(m => m.Id);
            rowCount = result.Count();
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }


        #region 多模型 操作

        /// <summary>
        /// 增加多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public bool SaveList(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _dbContext.Set<T>().Add(item);
            });

            if (IsCommit)
                return _dbContext.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 增加多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> SaveListAsync(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _dbContext.Set<T>().Add(item);
            });

            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 增加多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public bool SaveList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;
            var tmp = _dbContext.ChangeTracker.Entries<T>().ToList();
            foreach (var x in tmp)
            {
                var properties = typeof(T).GetTypeInfo().GetProperties();
                foreach (var y in properties)
                {
                    var entry = x.Property(y.Name);
                    entry.CurrentValue = entry.OriginalValue;
                    entry.IsModified = false;
                    y.SetValue(x.Entity, entry.OriginalValue);
                }
                x.State = EntityState.Unchanged;
            }
            T.ToList().ForEach(item =>
            {
                _dbContext.Set<T1>().Add(item);
            });
            if (IsCommit)
                return _dbContext.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 增加多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> SaveListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);
            var tmp = _dbContext.ChangeTracker.Entries<T>().ToList();
            foreach (var x in tmp)
            {
                var properties = typeof(T).GetTypeInfo().GetProperties();
                foreach (var y in properties)
                {
                    var entry = x.Property(y.Name);
                    entry.CurrentValue = entry.OriginalValue;
                    entry.IsModified = false;
                    y.SetValue(x.Entity, entry.OriginalValue);
                }
                x.State = EntityState.Unchanged;
            }
            T.ToList().ForEach(item =>
            {
                _dbContext.Set<T1>().Add(item);
            });
            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 更新多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public bool UpdateList(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Entry<T>(item).State = EntityState.Modified;
            });

            if (IsCommit)
                return _dbContext.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 更新多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> UpdateListAsync(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Entry<T>(item).State = EntityState.Modified;
            });

            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 更新多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public virtual bool UpdateList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;

            T.ToList().ForEach(item =>
            {
                _dbContext.Set<T1>().Attach(item);
                _dbContext.Entry<T1>(item).State = EntityState.Modified;
            });

            if (IsCommit)
                return _dbContext.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 更新多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> UpdateListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);

            T.ToList().ForEach(item =>
            {
                _dbContext.Set<T1>().Attach(item);
                _dbContext.Entry<T1>(item).State = EntityState.Modified;
            });

            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 删除多条记录，同一模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public bool DeleteList(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return false;

            T1.ToList().ForEach(item =>
            {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Set<T>().Remove(item);
            });

            if (IsCommit)
                return _dbContext.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 删除多条记录，同一模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> DeleteListAsync(List<T> T1, bool IsCommit = true)
        {
            if (T1 == null || T1.Count == 0) return await Task.Run(() => false);

            T1.ToList().ForEach(item =>
            {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Set<T>().Remove(item);
            });

            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        /// <summary>
        /// 删除多条记录，独立模型
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public bool DeleteList<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return false;

            T.ToList().ForEach(item =>
            {
                _dbContext.Set<T1>().Attach(item);
                _dbContext.Set<T1>().Remove(item);
            });

            if (IsCommit)
                return _dbContext.SaveChanges() > 0;
            else
                return false;
        }
        /// <summary>
        /// 删除多条记录，独立模型（异步方式）
        /// </summary>
        /// <param name="T1">实体模型集合</param>
        /// <param name="IsCommit">是否提交（默认提交）</param>
        /// <returns></returns>
        public async Task<bool> DeleteListAsync<T1>(List<T1> T, bool IsCommit = true) where T1 : class
        {
            if (T == null || T.Count == 0) return await Task.Run(() => false);

            T.ToList().ForEach(item =>
            {
                _dbContext.Set<T1>().Attach(item);
                _dbContext.Set<T1>().Remove(item);
            });

            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }


        /// <summary>
        /// 通过Lamda表达式，删除一条或多条记录（异步方式）
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="IsCommit"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(Expression<Func<T, bool>> predicate, bool IsCommit = true)
        {
            IQueryable<T> entry = (predicate == null) ? _dbContext.Set<T>().AsQueryable() : _dbContext.Set<T>().Where(predicate);
            List<T> list = entry.ToList();

            if (list != null && list.Count == 0) return await Task.Run(() => false);
            list.ForEach(item => {
                _dbContext.Set<T>().Attach(item);
                _dbContext.Set<T>().Remove(item);
            });

            if (IsCommit)
                return await Task.Run(() => _dbContext.SaveChanges() > 0);
            else
                return await Task.Run(() => false);
        }

        #endregion



        #region 验证是否存在

        /// <summary>
        /// 验证当前条件是否存在相同项
        /// </summary>
        public bool IsExist(Expression<Func<T, bool>> predicate)
        {
            var entry = _dbContext.Set<T>().Where(predicate);
            return (entry.Any());
        }
        /// <summary>
        /// 验证当前条件是否存在相同项（异步方式）
        /// </summary>
        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> predicate)
        {
            var entry = _dbContext.Set<T>().Where(predicate);
            return await Task.Run(() => entry.Any());
        }
        #endregion


        /// <summary>
        /// 事务性保存
        /// </summary>
        public bool Save()
        {
            var result = false;
            result = _dbContext.SaveChanges() > 0 ? true : false;
            return result;
        }




    }
}
