namespace fx.Domain.core
{
    public interface IAggregateRootFactory<T, TAggregateRootId> where T : AggregateRoot<TAggregateRootId>
    {
        T CreateAggregateRoot(T entity);
        T FindById(TAggregateRootId id);
        /// <summary>
        /// 根据Id删除实体。
        /// </summary>
        /// <param name="isLogicDeleted">是否逻辑删除，false=物理删除，true=逻辑删除</param>
        void DeleteById(bool isLogicDeleted);
        T Update(T entity);
    }
}
