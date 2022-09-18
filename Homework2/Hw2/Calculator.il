.class public abstract sealed auto ansi beforefieldinit Hw2.Calculator
{
    .method public hidebysig static float64 Calculate(float64 a,valuetype Hw2.CalculatorOperation s, float64 b) cil managed
    {    
        ldarg.0      //val1
        ldarg.2      //val2
    
        ldarg.1      // operation
        switch       (IL_0001, IL_0002, IL_0003, IL_0004,IL_0005)        
    
        IL_0001: add
                 ret
    
        IL_0002: sub
                 ret      
    
        IL_0003: mul
                 ret
    
        IL_0004: div
                 ret
    
        IL_0005: newobj       instance void [System.Runtime]System.ArgumentOutOfRangeException::.ctor()
                 throw
    }
}


