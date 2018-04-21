using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;


namespace CodedUITestProject
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class TeacherAppCodedUITest
    {
        public TeacherAppCodedUITest()
        {
        }

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

        [TestInitialize]
        public void StartTeacherApp()
        {
            UIMap.StartTeacherRecordedMethod();
        }


        [TestCleanup]
        public void CloseApp()
        {
            this.UIMap.CloseTeacherAppRecordedMethod();
            }

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


        #region Тестовые сценарии

        #region Визуальная проверка интерфейса


        //Testcase 1.1
        [TestMethod]
        public void TeacherAppUICheck()
        {
            UIMap.TestDisciplineNameCheck();
            UIMap.EditTestButtonExistMethod();
            UIMap.ResultButtonAssertMethod();
            UIMap.AboutAssertMethod();
            UIMap.StatusBarAssertMethod();
        }

        //Testcase 1.2
        [TestMethod]
        public void TestEditUICheck()
        {

            this.UIMap.TestEditViewOpenRecordedMethod();

            this.UIMap.AddEditBtnAssertMethod();
            this.UIMap.TestParamAssertMethod();
            this.UIMap.BackBtnAssertMethod();


            this.UIMap.TestEditBackRecordedMethod();
        }

        //Testcase 1.3
        [TestMethod]
        public void BackFromFunctionViewCheck()
        {
            this.UIMap.TestEditFormShowMethod();
            this.UIMap.BackBtnAddEditAssertMethod();
        }

        #endregion

        #region Возможность редактирования теста
        //Testcase 2
        [TestMethod]
        public void TestEditFormUICheck()
        {

            this.UIMap.TestEditFormShowMethod();

            this.UIMap.EditButtonEnabledAssertMethod();
            this.UIMap.DeleteBtnEnabledAssertMethod();


            this.UIMap.NewBtnAssertMethod();
            this.UIMap.BackBtnAddEditAssertMethod();

        }

        /// <summary>
        /// "Проверка доступности кнопок 'Добавить' и 'Удалить
        /// </summary>


        //Testcase 2.1
        [TestMethod()]
        public void TestEditFormBtnEnabledCheck()
        {

            UIMap.OpneEditWindowRecordedMethod();

            this.UIMap.EditButtonEnabledAssertMethod();
            this.UIMap.DeleteBtnEnabledAssertMethod();

            this.UIMap.QuestionItemSelectRecordedMethod();
            this.UIMap.EditBtnQuestionSelectedEnableAssertMethod();
            this.UIMap.DeleteBtnQuestionSelectedEnableAssertMethod();

            this.UIMap.BackBtnAddEditAssertMethod();

        }

        //Testcase 2.2.1
        [TestMethod()]
        public void AddNewQuestionViewCheck()
        {
            UIMap.OpneEditWindowRecordedMethod();

            this.UIMap.NewQuestionWindowOpenRecordedMethod();

            this.UIMap.QuestionNameTextFieldAssertMethod();
            this.UIMap.AnswerFiledAssertMethod();
            this.UIMap.AddAnswerBtnExistEnableAssertMethod();
            this.UIMap.AnswerDeleteBtnExistEnabledAssertMethod();

            this.UIMap.NewQuestionOkBtnExistAssertMethod();
            this.UIMap.NewQuestionCancelBtnExistAssertMethod();

            this.UIMap.AddQuestionCancelRecordedMethod();
        }

        #endregion Возможность редактирования теста

        #endregion Тестовые сценарии

    }
}
