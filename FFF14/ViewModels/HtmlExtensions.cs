using System;
using System.Linq.Expressions;
using System.Web.Mvc;

public static class MvcHtmlHelpers
{
	public static MvcHtmlString DescriptionFor<TModel, TValue>( this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression )
	{
		var metadata = ModelMetadata.FromLambdaExpression( expression, self.ViewData );
		var description = metadata.Description;

		return MvcHtmlString.Create( string.Format( "<span class=\"help-block\">{0}</span>", description ) );
	}
}