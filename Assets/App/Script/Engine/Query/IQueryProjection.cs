public interface IQueryProjection <in T> where T: IQueryData
{
    void Set(T t);
}

public interface IQueryProjection<in T, in T1> where T: IQueryData where T1 : IQueryData
{
    void Set(T t, T1 t1);
}