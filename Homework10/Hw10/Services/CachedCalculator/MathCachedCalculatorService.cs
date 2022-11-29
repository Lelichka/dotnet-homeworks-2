using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly ApplicationContext _dbContext;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(ApplicationContext dbContext, IMathCalculatorService simpleCalculator)
	{
		_dbContext = dbContext;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		var cache = _dbContext.SolvingExpressions.FirstOrDefault(SolvExpr => SolvExpr.Expression == expression);
		if (cache != null)
			return new CalculationMathExpressionResultDto(cache.Result);
		var calculated =  await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (!calculated.IsSuccess) return calculated;
		_dbContext.SolvingExpressions.Add(new SolvingExpression(expression!, calculated.Result));
		await _dbContext.SaveChangesAsync();
		return calculated;
	}
};