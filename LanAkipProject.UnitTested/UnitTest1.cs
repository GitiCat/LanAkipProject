using Akip;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows;

namespace LanAkipProject.UnitTested
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ConvertToTimeResult()
        {
            //arrenge 
            string s_value = "3600";

            //act
            ProgramDesignModel model = new ProgramDesignModel();
            TimeSpan test_time = TimeSpan.FromSeconds( 3600 );
            TimeSpan out_time = model.ConvertToTimeFromStringValue( "aa", "3600" );
            //assert
            bool res = false;
            if(test_time == out_time) {
                res = true;
            } else { res = false; }

            Assert.IsFalse( res );
        }
    }
}
