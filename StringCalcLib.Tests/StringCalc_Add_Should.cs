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
    [InlineData("")]
    [InlineData(null)]
    public void Add_InputIsNullOrEmptyReturnsZero(string str)
    {
        int result = _stringCalc.Add(str);
        Assert.True(result == 0, $"StringCalc::Add(\"{str}\") should return 0");
    }
}