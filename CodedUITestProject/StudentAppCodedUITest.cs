using System;
using System.Collections.Generic;
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
    public class StudentAppCodedUITest
    {
        public StudentAppCodedUITest()
        {


        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        private TestContext testContextInstance;

        [TestInitialize]
        public void StartStudentApp()
        {
            this.UIMap.StartStudentAppRecordedMethod();
        }

        [TestCleanup]
        public void CloseStutdApp()
        {
            this.UIMap.CloseStudAppRecordedMethod1();
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

        //Testcase 1
        [TestMethod]

        public void StudAppUICheck()
        {

            this.UIMap.NameTestStudAppAssertMethod2(); //Проверка названия "Тестирование по дисциплине:"
            this.UIMap.BtnOKExistAssertMethod2(); //Наличие и доступность кнопки "Ок"
            this.UIMap.TitleNameStudAppAssertMethod2(); //Проверка названия "Дисциплины"

        }

        //Tastcase 2 и 2.1
        [TestMethod]

        public void RegistrationFormStudAppCheck()
        {

            this.UIMap.TransitToRegistrationFormRecordedMethod1(); //переход на стр. Регистрация
            this.UIMap.NameFormAssertMethod2(); //Проверка названия формы "Регистрация"
            this.UIMap.BtnDoneNoActiveAssertMethod2(); //Инвалидность кнопки Готово по умолчанию
            this.UIMap.BtnDoneNoCompleteDataRecordedMethod1(); //Инвалидность кнопки Готово, если не все поля заполнены (запись)
            //   this.UIMap.BtnDoneNoCompleteDataAssertMethod2();    //Инвалидность кнопки Готово, если не все поля заполнены (атрибутами). хрень вроде
            this.UIMap.BtnDoneFullCompleteRecordedMethod1(); //Валидность кнопки Готово, если все поля заполнены (запись)
            this.UIMap.BtnDoneFullCompleteAssertMethod2();  //Валидность кнопки Готово, если все поля заполнены (атрибуты). походу хрень какая то   

            // про числовые не вышло
        }

        //Testcase 3
        [TestMethod]

        public void InfoTestStudAppCheck()
        {

            this.UIMap.TranzitInfoFormRecordedMethod1(); //Переход к странице 
            this.UIMap.BtnBeginActiveAssertMethod2(); // Наличие и доступность кнопки Начать
            this.UIMap.ExistInfoFieldsAssertMethod2(); // Наличие полей с информацией 





        }



    }


}
