using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StringCalcLib;

namespace StringCalcTests;
public class StringCalc_Add_Should
{
    private readonly StringCalc _stringCalc;

    public StringCalc_Add_Should()
    {
        _stringCalc = new StringCalc();
    }

    [Theory]
    [InlineData("", 0)]
    [InlineData(null, 0)]
    public void Add_InputIsNullOrEmptyReturnsZero(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("0", 0)]
    [InlineData("1", 1)]
    [InlineData("3", 3)]
    [InlineData("5", 5)]
    public void Add_SingleValueReturnsUnchanged(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("1,1", 2)]
    [InlineData("3,4", 7)]
    [InlineData("9,9", 18)]
    public void Add_CommaDelimitedReturnsSum(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("1\n1", 2)]
    [InlineData("6\n9", 15)]
    [InlineData("4\n7", 11)]
    public void Add_NewlineDelimitedReturnsSum(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("1\n1,3", 5)]
    [InlineData("2,6\n9", 17)]
    [InlineData("4\n7\n1", 12)]
    [InlineData("2,5,3", 10)]
    public void Add_MixedDelimitersReturnsSum(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("-1")]
    [InlineData("2,-2")]
    [InlineData("3,-4")]
    [InlineData("6\n-5,1")]
    public void Add_NegativeCausesThrow(string str)
        => Assert.Throws<ArgumentException>(() => _stringCalc.Add(str));

    [Theory]
    [InlineData("1000", 1000)]
    [InlineData("1001,2", 2)]
    [InlineData("2,1003\n4",6)]
    [InlineData("3\n1337,7", 10)]
    [InlineData("1\n420420,1", 2)]
    public void Add_OverAThousandIgnored(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("//#\n1#3", 4)]
    [InlineData("//?\n1?5,7", 13)]
    [InlineData("//%2\n4%8", 14)]
    public void Add_SingleCharDelimiterIsDefinable(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("[###]\n2###3", 5)]
    [InlineData("[?#%]\n1?#%5,7", 13)]
    [InlineData("[&*$]2\n4&*$8", 14)]
    public void Add_MultiCharDelimiterIsDefinable(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");

    [Theory]
    [InlineData("//#//!//?//*\n2*3?4#!#8", 17)]
    [InlineData("[#!!][?@][),)]\n5?@3),)4#!!7", 19)]
    [InlineData("[#!#]//?[*]1*3?7#!#8", 19)]
    public void Add_MultipleDelimitersAreDefinable(string str, int expectedResult)
        => Assert.True(_stringCalc.Add(str) == expectedResult, $"StringCalc::Add(\"{str}\") should return {expectedResult}");
}