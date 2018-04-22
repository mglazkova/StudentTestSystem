using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace TeacherCodedUITestProject
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class CodedUITestClass
    {
        public CodedUITestClass()
        {
        }

        [TestMethod]
        public void CodedUITestMethod1()
        {
            // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        }

        [TestInitialize]
        public void StartTeacherApp()
        {
            ApplicationUnderTest.Launch(ConfigurationManager.AppSettings["AppPath"]);
        }


        [TestCleanup]
        public void CloseApp()
        {
            Keyboard.SendKeys(ApplicationUnderTest.Desktop, "{F4}", ModifierKeys.Alt);
        }

        #region Тестовые сценарии

        //Testcase 1.1
        [TestMethod]
        public void TeacherAppUICheck()
        {
            
        }

        #endregion Тестовые сценарии

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        private TestContext testContextInstance;
    }
}
