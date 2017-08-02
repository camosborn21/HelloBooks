using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;

namespace HelloBooks.Utilities
{

	public class FakeDbSet<T> : System.Data.Entity.IDbSet<T> where T : class
	{
		private readonly List<T> list = new List<T>();

		public FakeDbSet()
		{
			list = new List<T>();
		}

		public FakeDbSet(IEnumerable<T> contents)
		{
			this.list = contents.ToList();
		}

		#region IDbSet<T> Members

		public T Add(T entity)
		{
			this.list.Add(entity);
			return entity;
		}

		public T Attach(T entity)
		{
			this.list.Add(entity);
			return entity;
		}

		public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
		{
			throw new NotImplementedException();
		}

		public T Create()
		{
			throw new NotImplementedException();
		}

		public T Find(params object[] keyValues)
		{
			ParameterExpression _ParamExp = Expression.Parameter(typeof(T), "a");

			Expression _BodyExp = null;

			Expression _Prop = null;
			Expression _Cons = null;

			PropertyInfo[] props = typeof(T).GetProperties();
			var typeName = typeof(T).Name.ToLower() + "id";
			var key = props.Where(p => (p.Name.ToLower().Equals("id")) || (p.Name.ToLower().Equals(typeName))).Single();


			_Prop = Expression.Property(_ParamExp, key.Name);
			_Cons = Expression.Constant(keyValues.Single(), key.PropertyType);

			_BodyExp = Expression.Equal(_Prop, _Cons);

			var _Lamba = Expression.Lambda<Func<T, Boolean>>(_BodyExp, new ParameterExpression[] { _ParamExp });

			return this.SingleOrDefault(_Lamba);
		}

		public System.Collections.ObjectModel.ObservableCollection<T> Local
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public T Remove(T entity)
		{
			this.list.Remove(entity);
			return entity;
		}

		#endregion

		#region IEnumerable<T> Members

		public IEnumerator<T> GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		#endregion

		#region IQueryable Members

		public Type ElementType
		{
			get { return this.list.AsQueryable().ElementType; }
		}

		public System.Linq.Expressions.Expression Expression
		{
			get { return this.list.AsQueryable().Expression; }
		}

		public IQueryProvider Provider
		{
			get { return this.list.AsQueryable().Provider; }
		}

		#endregion
	}
}
