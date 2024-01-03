namespace EngineBay.Core
{
    using System.Globalization;
    using System.Linq.Expressions;

    public class Filter
    {
        public Filter(string filterString)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(filterString);

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

        public string Operator { get; set; }

        public string Value { get; set; }

        public Expression<Func<TModel, bool>> CreateFilterPredicate<TModel>(ParameterExpression parameter, MemberExpression property, ConstantExpression constant)
        {
            ArgumentNullException.ThrowIfNull(parameter);

            ArgumentNullException.ThrowIfNull(property);

            ArgumentNullException.ThrowIfNull(constant);

            if (Nullable.GetUnderlyingType(property.Type) != null && Nullable.GetUnderlyingType(constant.Type) == null)
            {
                constant = Expression.Constant(constant.Value, property.Type);
            }

            var comparison = this.Operator.ToLower(CultureInfo.CurrentCulture) switch
            {
                "eq" => Expression.Equal(property, constant),
                "neq" => Expression.NotEqual(property, constant),
                "gt" => Expression.GreaterThan(property, constant),
                "gte" => Expression.GreaterThanOrEqual(property, constant),
                "lt" => Expression.LessThan(property, constant),
                "lte" => Expression.LessThanOrEqual(property, constant),
                _ => throw new ArgumentException($"Invalid operator string {this.Operator}."),
            };

            return Expression.Lambda<Func<TModel, bool>>(comparison, parameter);
        }
    }
}
