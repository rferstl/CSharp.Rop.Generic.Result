# CSharp.Rop.Generic.Result

This library started as a copy of #vkhorikov/CSharpFunctionalExtensions. 

The idea of "Railway Oriented Programming" by Scott Vlashin 
https://fsharpforfunandprofit.com/rop/ has proven to be very useful in our project.

Some problems:

1. the mixed use of Result<T>, Maybe<T> and Result<Maybe<T>> leads to unsightly code

2. the generic type Result<T,E> with 2 type parameters is no fun in C#.

3. type string for the error object in Result<T> brings some nasty things.

4. working with Result<Maybe<T>> is difficult.

Therefore we have tried to merge the types Result<T> and Maybe<T> to create a three-track railway type.

In C# every function has 3 possible results, a valid object/value, null or an exception.  The return of null is still often used in existing code, e.g. to signal: Not found. 

The generic Result<T> Type presented here is intended to do just that explicitly and provide the necessary operators for further processing that is as "IF" free as possible.
