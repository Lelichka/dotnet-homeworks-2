using Hw10.DbModels;
using Hw10.Dto;
using Hw10.Services.MathCalculator;
using Microsoft.Extensions.Caching.Memory;

namespace Hw10.Services.CachedCalculator;

public class MathCachedCalculatorService : IMathCalculatorService
{
	private readonly IMemoryCache _cache;
	private readonly IMathCalculatorService _simpleCalculator;

	public MathCachedCalculatorService(IMemoryCache cache, IMathCalculatorService simpleCalculator)
	{
		_cache = cache;
		_simpleCalculator = simpleCalculator;
	}

	public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
	{
		if (expression != null)
		{
			var alreadyCalculated = _cache.Get<double?>(expression);

			if (alreadyCalculated != null)
			{
				return new CalculationMathExpressionResultDto(alreadyCalculated.Value);
			}
		}
		
		var calculated =  await _simpleCalculator.CalculateMathExpressionAsync(expression);
		if (!calculated.IsSuccess) return calculated;
		
		_cache.Set(expression, calculated.Result);
		return calculated;
	}
};