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
            this.UIMap.TestNameAssert();
            this.UIMap.TestEditBtnAssert();
            this.UIMap.TestResultBtnAssert();
            this.UIMap.AboutBtnAssert();
            this.UIMap.StatusBarAssert();

        }

        //Testcase 1.2
        [TestMethod]
        public void TestEditUICheck()
        {

            this.UIMap.TestEditViewOpenRecorded();

            //Пауза в секунду
            Playback.Wait(1000);

            this.UIMap.EditAddBtnAssert();
            this.UIMap.TestParamBtnAssert();
            this.UIMap.TestEditView_BackBtnAssert();

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

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
