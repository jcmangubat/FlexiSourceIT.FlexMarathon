using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Linq.Expressions;

namespace FlexiSourceIT.FlexMarathon.API.Helpers;

public static class FilterParser
{
    public static Expression<Func<T, bool>> Parse<T>(string filter)
    {
        if (string.IsNullOrWhiteSpace(filter))
            throw new ArgumentException("Filter cannot be null or empty.", nameof(filter));

        return BuildExpression<T>(filter);
    }

    public static Expression<Func<T, bool>> BuildExpression<T>(string filter)
    {
        try
        {
            return DynamicExpressionParser.ParseLambda<T, bool>(new ParsingConfig()
            {
                IsCaseSensitive = true,
                UseParameterizedNamesInDynamicQuery = true,
            }, false, filter);
        }
        catch (ParseException ex)
        {
            throw new ArgumentException($"Invalid filter expression: {filter}", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while parsing the filter expression.", ex);
        }
    }
}