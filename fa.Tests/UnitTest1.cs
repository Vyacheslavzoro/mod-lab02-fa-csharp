using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fans;

namespace NET
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// ѕровер€ет, что FA1 принимает строку, заканчивающуюс€ на "11".
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            String s = "0111";
            FA1 fa = new FA1();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == true);
        }

        /// <summary>
        /// ѕровер€ет, что FA1 отклон€ет строку, не заканчивающуюс€ на "11".
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            String s = "01011";
            FA1 fa = new FA1();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == false);
        }

        /// <summary>
        /// ѕроверка работы FA1 со сложной строкой Ч ожидаетс€ отклонение.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            String s = "110101011";
            FA1 fa = new FA1();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == false);
        }

        /// <summary>
        /// ѕровер€ет, что FA1 корректно принимает строку, заканчивающуюс€ на "11".
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            String s = "1110111";
            FA1 fa = new FA1();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == true);
        }

        /// <summary>
        /// ѕроверка короткой строки "10" Ч ожидаетс€ прин€тие или отклонение в зависимости от автомата.
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            String s = "10";
            FA1 fa = new FA1();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == true);
        }

        /// <summary>
        /// ѕроверка, что FA2 отклон€ет строку, не содержащую "0001".
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {
            String s = "0101";
            FA2 fa = new FA2();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == false);
        }

        /// <summary>
        /// ѕроверка, что FA2 отклон€ет строку без шаблона "0001".
        /// </summary>
        [TestMethod]
        public void TestMethod7()
        {
            String s = "00110011";
            FA2 fa = new FA2();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == false);
        }

        /// <summary>
        /// ѕроверка прин€ти€ FA2 строки, содержащей "0001".
        /// </summary>
        [TestMethod]
        public void TestMethod8()
        {
            String s = "0001";
            FA2 fa = new FA2();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == true);
        }

        /// <summary>
        /// ѕроверка прин€ти€ FA2 строки с нужным шаблоном в середине.
        /// </summary>
        [TestMethod]
        public void TestMethod9()
        {
            String s = "111000";
            FA2 fa = new FA2();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == true);
        }

        /// <summary>
        /// ѕроверка, что FA3 принимает строку с равным количеством 0 и 1.
        /// </summary>
        [TestMethod]
        public void TestMethod10()
        {
            String s = "00110011";
            FA3 fa = new FA3();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == true);
        }

        /// <summary>
        /// ѕроверка, что FA3 отклон€ет строку с неравным количеством 0 и 1.
        /// </summary>
        [TestMethod]
        public void TestMethod11()
        {
            String s = "0101";
            FA3 fa = new FA3();
            bool? result = fa.Run(s);
            Assert.IsTrue(result == false);
        }
    }
}
