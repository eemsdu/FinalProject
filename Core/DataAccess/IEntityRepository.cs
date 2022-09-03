using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    //generic constraint
    //class referans tip 
    //IEntity :IEntity olabilir veya IEntity implemente eden bir nesne olabilir 
    //new ():newlenebilir olmalı 
    public interface IEntityRepository<T> where T:class,IEntity,new()
    {
        List<T> GetAll(Expression<Func<T,bool>>filter=null);
        //filter=null filtre vermeyebilirsin demektir ,filtre vermiyorsa tüm datayı getir anlamına gelir.
        T Get(Expression<Func<T, bool>> filter );
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
      //ürünleri kategoriye göre filtrele 
    }
}
