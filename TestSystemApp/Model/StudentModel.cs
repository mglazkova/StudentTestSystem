using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemApp.Model
{
    public class StudentModel:BaseModel
    {
        public int Id { get; set; }

        #region LastNameProperty
        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }
        #endregion

        #region FirstNameProperty
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }
        #endregion

        #region GroupNumberProperty
        private string _groupNumber;
        public string GroupNumber
        {
            get { return _groupNumber; }
            set
            {
                if (_groupNumber != value)
                {
                    _groupNumber = value;
                    RaisePropertyChanged("GroupNumber");
                }
            }
        }
        #endregion 
    }
}
