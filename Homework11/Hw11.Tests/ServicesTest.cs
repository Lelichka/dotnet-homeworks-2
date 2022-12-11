using System.Globalization;
using System.Linq.Expressions;
using System.Net.Http.Json;
using Hw11.Dto;
using Hw11.ErrorMessages;
using Hw11.Exceptions;
using Hw11.Services;
using Hw11.Services.ExpressionTree;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hw11.Tests;

public class ServicesTest : IClassFixture<WebApplicationFactory<Program>>
{
	private readonly HttpClient _client;

	public ServicesTest(WebApplicationFactory<Program> fixture)
	{
		_client = fixture.CreateClient();
	}

	[Theory]
	[InlineData("-3 - 4", "-3 4 - ")]
	[InlineData("(-3) - (-4)", "-3 -4 - ")]
	[InlineData("(-2 + 2)", "-2 2 + ")]
	[InlineData("-1 - (-1)", "-1 -1 - ")]
	[InlineData("1 - 1 - 2", "1 1 - 2 - ")]
	public async Task ParseToPosrfixForm(string expression, string result)
	{
		var response = Parser.ConvertToPostfixForm(expression);

		Assert.Equal(result, response);
	}

	[Theory]
	[InlineData(ExpressionType.And,1,1 )]
	[InlineData(ExpressionType.Block,1,1 )]
	async Task ExpressionVisitorCalculateTest(ExpressionType expressionType, double first,double second)
	{
		var response = () => (object)ExpressionTreeVisitor.Calculate(expressionType,first,second);
		var exception = Assert.Throws<InvalidSymbolException>(response);
		Assert.Equal(MathErrorMessager.UnknownCharacter, exception.Message);
	}
	
	[Fact]
	async Task ExpressionVisitorUnknownExpressionTest()
	{
		var expr = Expression.Increment(Expression.Constant(1));
		
		var response = () => ExpressionTreeVisitor.VisitExpression(expr);
		var exception = Assert.ThrowsAsync(typeof(InvalidOperationException),response);
	}
}