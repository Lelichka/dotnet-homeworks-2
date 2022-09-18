.class public abstract sealed auto ansi beforefieldinit Hw2.Parser
    extends [System.Runtime]System.Object
{
    .method public hidebysig static void
    ParseCalcArguments(
      string[] args,
      [out] float64& val1,
      [out] valuetype Hw2.CalculatorOperation& operation,
      [out] float64& val2) cil managed
    {
        ldarg.0      // args
        call         bool Hw2.Parser::IsArgLengthSupported(string[])
        brtrue.s     IL_0001
        
        ldstr        "You have to pass 3 arguments"
        newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
        throw
        
        IL_0001: ldarg.0      // args
                 ldc.i4.0
                 ldelem.ref
                 ldarg.1      // val1
                 call         bool [System.Runtime]System.Double::TryParse(string, float64&)
                 brtrue.s    IL_0002
                 
                 ldstr        "The first argument could not be converted to the double type"
                 newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
                 throw
        
        IL_0002: ldarg.0      // args
                 ldc.i4.2
                 ldelem.ref
                 ldarg.3      // val2
                 call         bool [System.Runtime]System.Double::TryParse(string, float64&)
                 brtrue.s     IL_0003
                 
                 ldstr        "The second argument could not be converted to the double type"
                 newobj       instance void [System.Runtime]System.ArgumentException::.ctor(string)
                 throw   
 
        IL_0003: ldarg.2    //operation      
                 ldarg.0    //args
                 ldc.i4.1
                 ldelem.ref      
                 call valuetype [Hw2]Hw2.CalculatorOperation [Hw2]Hw2.Parser::ParseOperation(string)
                 stind.i4         
                 ret    
    } 
        
    .method private static bool IsArgLengthSupported (string[] args) cil managed
    {
        ldarg.0         
        ldlen           
        ldc.i4.3        
        ceq             
        ret
    }
        
    .method private hidebysig static valuetype Hw2.CalculatorOperation
    ParseOperation(
      string arg) cil managed
    {
        IL_0001: ldarg.0
                 ldstr       "+"
                 call        bool [System.Runtime]System.String::op_Equality(string, string)
                 brfalse.s   IL_0002     
                 ldc.i4.0
                 ret
        
        IL_0002: ldarg.0
                 ldstr       "-"
                 call        bool [System.Runtime]System.String::op_Equality(string, string)
                 brfalse.s   IL_0003     
                 ldc.i4.1
                 ret
        
        IL_0003: ldarg.0
                 ldstr       "*"
                 call        bool [System.Runtime]System.String::op_Equality(string, string)
                 brfalse.s   IL_0004    
                 ldc.i4.2
                 ret
        
        IL_0004: ldarg.0
                 ldstr       "/"
                 call        bool [System.Runtime]System.String::op_Equality(string, string)
                 brfalse.s   IL_0005  
                 ldc.i4.3
                 ret
        
        IL_0005:  ldstr      "Unknown operation"
                  newobj     instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
                  throw
    } 
} 
