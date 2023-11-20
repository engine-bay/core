namespace EngineBay.Core
{
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Filter
    {
        public Filter(string filterString)
        {
            if (filterString is null)
            {
                throw new ArgumentNullException(nameof(filterString));
            }

            var components = filterString.Split(':');
            this.Field = components[0];
            this.Operator = components[1];
            this.Value = string.Join(":", components[2..]);

            if (string.IsNullOrEmpty(this.Field))
            {
                throw new ArgumentException("No Field found");
            }

            if (string.IsNullOrEmpty(this.Operator))
            {
                throw new ArgumentException("No Operator found");
            }

            if (string.IsNullOrEmpty(this.Value))
            {
                throw new ArgumentException("No Value found");
            }
        }

        public string Field { get; set; }

        // public Expression Operation { get; set; }
        public string Operator { get; set; }

        public string Value { get; set; }

        // Attempt 1 - probably works but I didn't like the anotation
        // [RequiresUnreferencedCode("Dynamic Filter")]
        // public Expression<Func<TModel, bool>> CreateFilterPredicate<TModel>()
        // {
        //    var modelParameter = Expression.Parameter(typeof(TModel));
        //    var property = Expression.Property(modelParameter, this.Field);
        //    var format = new DateTimeFormatInfo();
        //    var value = Expression.Constant(Convert.ChangeType(this.Value, property.Type, format));

        // var comparison = this.GetOperatorComparison(property, value);

        // return Expression.Lambda<Func<TModel, bool>>(comparison, modelParameter);
        // }

        // Attempt 2 - works
        public Expression<Func<TModel, bool>> CreateFilterPredicate<TModel>(PropertyInfo property)
        {
            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            var modelParameter = Expression.Parameter(typeof(TModel));
            var expressionProperty = Expression.Property(modelParameter, property);
            var format = new DateTimeFormatInfo();
            var value = Expression.Constant(Convert.ChangeType(this.Value, property.PropertyType, format));

            var comparison = this.GetOperatorComparison(expressionProperty, value);

            return Expression.Lambda<Func<TModel, bool>>(comparison, modelParameter);
        }

        // Attempt 3 - Does not work
        // Error: System.InvalidOperationException: Invoke cannot evaluate LambdaExpression from 'value(EngineBay.DemoModule.QueryTodoItem+<>c__DisplayClass2_1).filter.GetOperatorComparison()'. Ensure that your function/property/member returns LambdaExpression
        public Func<TValue, TValue, bool> GetOperatorComparison<TValue>()
            where TValue : IComparable<TValue>
        {
            return this.Operator.ToLower(CultureInfo.CurrentCulture) switch
            {
                "eq" => (a, b) => a.CompareTo(b) == 0,
                "neq" => (a, b) => a.CompareTo(b) != 0,
                "gt" => (a, b) => a.CompareTo(b) > 0,
                "gte" => (a, b) => a.CompareTo(b) >= 0,
                "lt" => (a, b) => a.CompareTo(b) < 0,
                "lte" => (a, b) => a.CompareTo(b) <= 0,
                _ => throw new ArgumentException($"Invalid operator string {this.Operator}."),
            };
        }

        // Attempt 4 - Does not work
        public Expression<Func<TModel, bool>> CreateFilterPredicate<TModel, TValue>(ParameterExpression parameter, Expression<Func<TModel, TValue>> propertySelector)
            where TValue : IComparable<TValue>
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (propertySelector is null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            var format = new DateTimeFormatInfo();
            var value = Expression.Constant(Convert.ChangeType(this.Value, typeof(TValue), format));

            var comparison = this.GetOperatorComparison(propertySelector.Body, value);

            return Expression.Lambda<Func<TModel, bool>>(comparison, parameter);
        }

        // Attempt 4
        public Expression<Func<TModel, bool>> CreateFilterPredicate<TModel, TValue>(ParameterExpression parameter, Expression<Func<TModel, TValue?>> propertySelector)
            where TValue : struct, IComparable<TValue>
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (propertySelector is null)
            {
                throw new ArgumentNullException(nameof(propertySelector));
            }

            var format = new DateTimeFormatInfo();
            var value = Expression.Constant(Convert.ChangeType(this.Value, typeof(TValue), format));

            var comparison = this.GetOperatorComparison(propertySelector.Body, value);

            return Expression.Lambda<Func<TModel, bool>>(comparison, parameter);
        }

        // Attempt 5 - Works
        // Needed some rework on the type conversion
        public Expression<Func<TModel, bool>> CreateFilterPredicate<TModel>(ParameterExpression parameter, MemberExpression property, ConstantExpression constant)
        {
            if (parameter is null)
            {
                throw new ArgumentNullException(nameof(parameter));
            }

            if (property is null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            if (constant is null)
            {
                throw new ArgumentNullException(nameof(constant));
            }

            if (Nullable.GetUnderlyingType(property.Type) != null && Nullable.GetUnderlyingType(constant.Type) == null)
            {
                constant = Expression.Constant(constant.Value, property.Type);
            }

            var comparison = this.GetOperatorComparison(property, constant);

            return Expression.Lambda<Func<TModel, bool>>(comparison, parameter);
        }

        private BinaryExpression GetOperatorComparison(Expression left, Expression right)
        {
            return this.Operator.ToLower(CultureInfo.CurrentCulture) switch
            {
                "eq" => Expression.Equal(left, right),
                "neq" => Expression.NotEqual(left, right),
                "gt" => Expression.GreaterThan(left, right),
                "gte" => Expression.GreaterThanOrEqual(left, right),
                "lt" => Expression.LessThan(left, right),
                "lte" => Expression.LessThanOrEqual(left, right),
                _ => throw new ArgumentException($"Invalid operator string {this.Operator}."),
            };
        }
    }
}
