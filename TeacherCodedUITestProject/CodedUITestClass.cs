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

        //Testcase 1.3
        [TestMethod]
        public void BackFromFunctionViewCheck()
        {
            this.UIMap.TestEditViewOpenRecorded();

            //Пауза в секунду
            Playback.Wait(1000);

            this.UIMap.TestEditViewBackRecorded();

        }

        //Testcase 2.1
        [TestMethod]
        public void TestEditFormUICheck()
        {
            //Открываем форму всех функции редактирования теста
            this.UIMap.TestEditViewOpenRecorded();

            //Открываем функцию добавления\редактирования вопроса
            this.UIMap.QuestionListOpenRecorded();


            //Пауза в секунду
            Playback.Wait(1000);

            //Проверяем наличие кнопко и их доступность по умолчанию
            this.UIMap.EditBtnDefaultAssert();
            this.UIMap.DeleteQuestionBtnDefaultAssert();
            this.UIMap.AddNewQuestionBtnDefaultAssert();
            this.UIMap.QuestionAddEditView_BackBtnAssert();

            Playback.Wait(1000);
            //Выбираем первый вопрос из списка
            this.UIMap.QuestionListSelectFirstRecorded();

            //Проверяем доступность кнопок после выбора вопроса в списке
            this.UIMap.QuestionListSelected_EditAddBtnAssert();
            this.UIMap.QuestionListSelected_QuestionDeleteBtnAssert();

            Playback.Wait(1000);
        }

        //Testcase 2.2.1
        [TestMethod]
        public void AddNewQuestionViewCheck()
        {
            //Открываем форму всех функции редактирования теста
            this.UIMap.TestEditViewOpenRecorded();

            //Открываем функцию добавления\редактирования вопроса
            this.UIMap.QuestionListOpenRecorded();

            //Открывем форму добавление вопроса
            this.UIMap.QuestionAddNewViewRecorded();

            Playback.Wait(1000);
            //Проверка состояния кнопки "Добавить"
            this.UIMap.AddQuestionView_AddBtnAssert();
            //Проверка состояния кнопки "Удалить"
            this.UIMap.AddQuestionView_DeleteBtnAssert();
            //Проверка состояния кнопки "Готово"
            this.UIMap.AddQuestionView_OkBtnAssert();
            //Проверка состояния кнопки "Назад"
            this.UIMap.AddQuestionView_CancelBtnAssert();

            Playback.Wait(1000);
            //Закрытие формы
            this.UIMap.AddQuestionView_CancelRecorded();
        }

        //Testcase 2.2.2
        [TestMethod]
        public void AddNewQuestionAddWithoutAnswer()
        {
            //Открываем форму всех функции редактирования теста
            this.UIMap.TestEditViewOpenRecorded();

            //Открываем функцию добавления\редактирования вопроса
            this.UIMap.QuestionListOpenRecorded();

            //Открывем форму добавление вопроса
            this.UIMap.QuestionAddNewViewRecorded();

            //Нажимаем на кнопку "Готово" без добавления ответов
            this.UIMap.AddQuestionView_OkWithoutAnswerRecorded();

            Playback.Wait(1000);
            //Закрытие формы
            this.UIMap.AddQuestionView_CancelRecorded();

        }

        //Testcase 2.2.3
        [TestMethod]
        public void AddNewQuestionAddWithoutRightAnswer()
        {
            //Открываем форму всех функции редактирования теста
            this.UIMap.TestEditViewOpenRecorded();

            //Открываем функцию добавления\редактирования вопроса
            this.UIMap.QuestionListOpenRecorded();

            //Открывем форму добавление вопроса
            this.UIMap.QuestionAddNewViewRecorded();

            //Добавляем 3 варианта ответа
            this.UIMap.AddQuestionView_AddAnswerRecorded();

            //Нажимаем на "Готово" без указания любого ответа правильным
            this.UIMap.AddQuestionView_OkWithoutRightAnswerRecorded();

            Playback.Wait(1000);
            //Закрытие формы
            this.UIMap.AddQuestionView_CancelRecorded();

        }

        //Testcase 2.2.4
        [TestMethod]
        public void AddNewQuestionWithRightAnswer()
        {
            //Открываем форму всех функции редактирования теста
            this.UIMap.TestEditViewOpenRecorded();

            //Открываем функцию добавления\редактирования вопроса
            this.UIMap.QuestionListOpenRecorded();

            //Открывем форму добавление вопроса
            this.UIMap.QuestionAddNewViewRecorded();

            Playback.Wait(1000);

            //Вводим текст вопроса
            this.UIMap.AddQuestionView_SetQuestionTextRecorded();

            Playback.Wait(1000);

            //Добавляем 3 варианта ответа
            this.UIMap.AddQuestionView_AddAnswerRecorded();

            Playback.Wait(1000);

            //Помечаем первый вопрос правильным
            this.UIMap.AddQuestionView_CheckFirstAnswerRightRecorded();

            //Нажимаем "Готово"
            this.UIMap.AddQuestionView_OkRecorded();

            //Проверяем что вопрос добавился в список
            this.UIMap.AddQuestionView_QustionItemAddedAssert();

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
