using System;

namespace fx.Application.Customer.Injectors
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Module)]
    public class TestFodyInjuector:Attribute
    {

    }
}