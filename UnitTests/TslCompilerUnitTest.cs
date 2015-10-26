﻿using System.Reflection;
using Computator.NET.Compilation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class NativeCompilerUnitTest
    {
        private readonly NativeCompiler nativeCompiler = new NativeCompiler();

        [TestMethod]
        public void Test1()
        {
            var assembly = nativeCompiler.Compile(@"using System;

namespace Testing
{
	public static class TestCase
	{
		public static int TestFunction()
		{
			return Math.Abs(-2)*2*2;
		}
	}
}");
            Assert.AreEqual(8,
                assembly.GetType("Testing.TestCase")
                    .GetMethod("TestFunction", BindingFlags.Public | BindingFlags.Static)
                    .Invoke(null, null));
        }
    }


    [TestClass]
    public class TslCompilerUnitTest
    {
        private readonly TslCompiler tslCompiler = new TslCompiler();

        [TestMethod]
        public void Test1()
        {
            tslCompiler.Variables.Clear();
            tslCompiler.Variables.Add("x");

            Assert.AreEqual("pow(x,2)+cos(pow(x,2*x+1.1+cos(x)))+2/3.0",
                tslCompiler.TransformToCSharp("x²+cos(x²˙ˣ⁺¹ॱ¹⁺ᶜᵒˢ⁽ˣ⁾)+2/3"), "Fail!!!");
        }

        [TestMethod]
        public void Test6()
        {
            tslCompiler.Variables.Clear();
            tslCompiler.Variables.Add("x");

            Assert.AreEqual("pow(2,103213.323232)", tslCompiler.TransformToCSharp("2¹⁰³²¹³ॱ³²³²³²"), "Fail!!!");
        }

        [TestMethod]
        public void Test2()
        {
            tslCompiler.Variables.Clear();
            //tslCompiler.Variables.Add("x");

            Assert.AreEqual("var f = TypeDeducer.Func((real x, real y, complex z) => 100/(1.0*((2+2))))",
                tslCompiler.TransformToCSharp("var f(real x, real y, complex z)=100/(2+2)"), "Fail!!!");
        }

        [TestMethod]
        public void Test3()
        {
            tslCompiler.Variables.Clear();
            tslCompiler.Variables.AddRange(new[] {"x", "y", "z"});

            Assert.AreEqual("z*x*y+pow(y,x*z*y+11*x+cos(x/y))",
                tslCompiler.TransformToCSharp("z·x·y+yˣ˙ᶻ˙ʸ⁺¹¹˙ˣ⁺ᶜᵒˢ⁽ˣ˸ʸ⁾"), "Fail!!!");
        }

        [TestMethod]
        public void Test4()
        {
            tslCompiler.Variables.Clear();
            //tslCompiler.Variables.Add("x");

            Assert.AreEqual("var sumOfValues = TypeDeducer.Func((string str, integer k) => k+k+k+str.Length)",
                tslCompiler.TransformToCSharp("function sumOfValues(string str, integer k)=k+k+k+str.Length"), "Fail!!!");
        }

        [TestMethod]
        public void Test5()
        {
            tslCompiler.Variables.Clear();
            tslCompiler.Variables.Add("x");

            Assert.AreEqual("(pow(10,2)*x)/(1.0*((10-6*pow(x,2)+pow((25-pow(x,2)),2)+10*(25-pow(x,2)))))",
                tslCompiler.TransformToCSharp("(10²·x)/(10-6·x²+(25-x²)²+10·(25-x²))"), "Fail!!!");
        }
    }
}