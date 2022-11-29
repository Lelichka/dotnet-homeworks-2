﻿using System.Text.RegularExpressions;

namespace Hw11.Services;
using Hw11.ErrorMessages;
public static class Validator
{
    public static void Validate(string input)
    {
        
        if (string.IsNullOrEmpty(input))
            throw  new Exception(MathErrorMessager.EmptyString);
        if (!CheckCorrectParenthesis(input))
            throw  new Exception(MathErrorMessager.IncorrectBracketsNumber);
        
        foreach (var c in input.Where(c => 
                     !char.IsDigit(c) && !new[] { '+', '-', '*', '/', '(', ')', '.', ' ' }.Contains(c)))
            throw new Exception(MathErrorMessager.UnknownCharacterMessage(c));
        
        var array = new Regex("(?<=[-+*/()])|(?=[-+*/()])").Split(input.Replace(" ", ""))
            .Where(c => c != "").ToArray();
        
        if (!double.TryParse(input[0].ToString(), out _ ) && !new[] { "-", "(" }.Contains(input[0].ToString()))
            throw new Exception(MathErrorMessager.StartingWithOperation);
        
        if (!double.TryParse(input[^1].ToString(), out _ ) && array[^1] != ")")
            throw new Exception(MathErrorMessager.EndingWithOperation);

        var lastElem = "";
        var lastOperation = true;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i].Length > 1 || !("-+*/()".IndexOf(array[i][0]) != -1))
            {
                lastElem = array[i];
                lastOperation = false;
                if (!double.TryParse(array[i], out _))
                    throw new Exception(MathErrorMessager.NotNumberMessage(array[i]));
                continue;
            }

            if (array[i] == "-" && lastOperation)
            {
                lastElem = array[i];
                lastOperation = false;
                continue;
            }
            

            switch (array[i])
            {
                case "(":
                    lastElem = array[i];
                    lastOperation = true;
                    continue;
                case ")":
                {
                    if (lastOperation)
                        throw new Exception(MathErrorMessager.OperationBeforeParenthesisMessage(lastElem));
                    lastElem = array[i];
                    lastOperation = false;
                    continue;
                }
            }

            if (lastOperation)
            {
                if (lastElem == "(")
                    throw new Exception(MathErrorMessager.InvalidOperatorAfterParenthesisMessage(array[i]));
                throw new Exception(MathErrorMessager.TwoOperationInRowMessage(lastElem, array[i]));
            }

            lastElem = array[i];
            lastOperation = true;
        }
    }
    private static bool CheckCorrectParenthesis(string input)
    {
        var open = 0;
        foreach (var c in input)
            switch (c)
            {
                case '(':
                    open++;
                    break;
                case ')' when open == 0:
                    return false;
                case ')':
                    open--;
                    break;
            }

        return open == 0;
    }
    
    
}