using System.Linq.Expressions;
using System.Reflection;

namespace Web_api.Extensions
{
    public static class QueryableExtensions
    {
        // 1. Từ khóa 'this' giúp biến OrderByDynamic thành một phương thức mở rộng của IQueryable<T>
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> source, string? propertyName, string? direction)
        {
            // Nếu không truyền tên cột, giữ nguyên query gốc
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return source;
            }

            var type = typeof(T);

            // 2. Tìm Property trong Class của Entity.
            // Sử dụng BindingFlags.IgnoreCase để bỏ qua phân biệt chữ hoa/thường 
            // (ví dụ: client gửi lên "name" vẫn sẽ map được vào thuộc tính "Name" của C#)
            var property = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // Nếu nhập sai tên cột hoặc cột không tồn tại
            if (property == null)
            {
                return source;
            }

            // 3. Tạo biểu thức Lambda dạng: p => p.Name
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // 4. Kiểm tra hướng sắp xếp gửi từ client lên là giảm dần hay tăng dần
            var isDesc = "desc".Equals(direction, StringComparison.OrdinalIgnoreCase);
            var methodName = isDesc ? "OrderByDescending" : "OrderBy";

            // 5. Thực hiện gọi động phương thức OrderBy hoặc OrderByDescending trong Linq
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(orderByExpression)
            );

            return source.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
